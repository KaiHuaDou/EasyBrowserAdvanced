using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using CefSharp;
using 极简浏览器.Api;

namespace 极简浏览器;

/// <summary>
/// 下载文件的模块
/// </summary>
public partial class Download : Window, IDisposable
{
    private readonly string filePath;
    private static DownloadItem task = new( );
    private HttpClient httpClient;
    private HttpResponseMessage response;

    private Stream webStream;
    private FileStream fileStream;
    private readonly byte[] buf = new byte[short.MaxValue];
    private ulong totalSize, size, lastSize, lastTime;
    private ulong NowTime => (ulong) stopwatch.Elapsed.TotalSeconds;
    private bool finished;

    private Stopwatch stopwatch = new( );
    private delegate void UpdateValue(ulong value);
    private CancellationTokenSource cancelSource = new( );

    public Download(DownloadItem item, string path)
    {
        InitializeComponent( );
        task = item;
        filePath = path;
        FileName.Content = task.SuggestedFileName;
        httpClient = new HttpClient( );
        DownloadInit( );
    }

    public async void DownloadInit( )
    {
        try
        {
            response = await httpClient.GetAsync(task.Url, HttpCompletionOption.ResponseHeadersRead, cancelSource.Token);
            response.EnsureSuccessStatusCode( );
            totalSize = (ulong) response.Content.Headers.ContentLength.GetValueOrDefault( );
            fileStream = new FileStream(filePath, FileMode.Create, FileAccess.ReadWrite);
            stopwatch.Start( );
            await Task.Run(DownloadFile, cancelSource.Token);
        }
        catch (HttpRequestException e)
        {
            MessageBox.Show("无法下载\n" + e.Message);
            Close( );
        }
    }

    public async Task DownloadFile( )
    {
        webStream = await response.Content.ReadAsStreamAsync(cancelSource.Token);
        for (int i = 0; (i = await webStream.ReadAsync(buf, cancelSource.Token)) > 0;)
        {
            try
            {
                size += (ulong) i;
                Dispatcher.Invoke(new UpdateValue(UpdateUI), size);
                await fileStream.WriteAsync(buf.AsMemory(0, i), cancelSource.Token);
            }
            catch (IOException ex)
            {
                Logger.Write(ex, LogType.Warn);
            }
        }
        finished = true;
        fileStream.Close( );
        webStream.Close( );
        Dispatcher.Invoke(( ) =>
        {
            OpenButton.IsEnabled = true;
            CancelButton.Content = "关闭";
        });
    }

    private void UpdateUI(ulong nowSize)
    {
        double percent = nowSize * 100 / totalSize;
        Progress.Value = percent;
        Percent.Content = Math.Round(percent, 2) + "%";

        ulong timePassed = NowTime - lastTime;
        ulong sizePassed = nowSize - lastSize;
        if (timePassed == 0 || sizePassed == 0) return;
        double speed = sizePassed / timePassed;
        ulong timeRemain = (ulong) ((totalSize - nowSize) / speed);

        Speed.Content = Math.Round(speed / 1024.0) + "KB/s";
        string hourPassed = NowTime / 3600 > 0 ? $"{NowTime / 3600}h" : "";
        string minPassed = NowTime % 3600 / 60 > 0 ? $"{NowTime % 3600 / 60}m" : "";
        string secPassed = NowTime % 60 > 0 ? $"{NowTime % 60}s" : "";
        string hourRemain = timeRemain / 3600 > 0 ? $"{timeRemain / 3600}h" : "";
        string minRemain = timeRemain % 3600 / 60 > 0 ? $"{timeRemain % 3600 / 60}m" : "";
        string secRemain = timeRemain % 60 > 0 ? $"{timeRemain % 60}s" : "";
        RunTime.Content = $"{hourPassed}{minPassed}{secPassed}/{hourRemain}{minRemain}{secRemain}";

        lastTime = NowTime;
        lastSize = nowSize;
    }

    private void OpenClick(object o, RoutedEventArgs e)
        => Process.Start(filePath);

    private void OpenFolderClick(object o, RoutedEventArgs e)
    {
        try { Process.Start(Directory.GetParent(filePath).FullName); }
        catch (FileNotFoundException) { }
    }

    private void CancelTask( )
    {
        cancelSource.Cancel( );
        File.Delete(filePath);
    }

    private void PauseClick(object o, RoutedEventArgs e)
    {
        if (PauseResumeButton.Content.ToString( ) == "暂停")
        {
            PauseResumeButton.Content = "继续";
            cancelSource.Cancel( );
        }
        else
        {
            PauseResumeButton.Content = "暂停";
            cancelSource = new CancellationTokenSource( );
        }
    }

    private void CancelClick(object o, RoutedEventArgs e)
    {
        if (finished)
        {
            Close( );
        }
        else
        {
            CancelTask( );
            CancelButton.IsEnabled = false;
        }
    }

    private void WindowClosing(object o, CancelEventArgs e)
    {
        if (!finished)
            CancelTask( );
    }

    private void WindowSizeChanged(object o, SizeChangedEventArgs e)
        => FromURL.Content = Utils.ZipStr(task.Url, (int) (ActualWidth / 15));

    public void Dispose( )
    {
        fileStream.Dispose( );
        webStream.Dispose( );
        response.Dispose( );
        cancelSource.Dispose( );
        httpClient.Dispose( );
        GC.SuppressFinalize(this);
    }
}
