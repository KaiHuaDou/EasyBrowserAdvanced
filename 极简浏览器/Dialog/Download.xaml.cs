using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Threading;
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
    private WebRequest request;
    private WebResponse response;

    private Thread thread;
    private Stream webStream;
    private FileStream fileStream;
    private readonly byte[] buf = new byte[short.MaxValue];
    private ulong totalSize, size, lastSize, lastTime;
    private ulong NowTime => (ulong) stopwatch.Elapsed.TotalSeconds;
    private bool finished;

    private Stopwatch stopwatch = new( );
    private delegate void UpdateValue(ulong value);
    private CancellationTokenSource cancelToken = new( );

    public Download(DownloadItem item, string path)
    {
        InitializeComponent( );
        task = item;
        filePath = path;
        FileName.Content = task.SuggestedFileName;
        DownloadInit( );
    }

    public void DownloadInit( )
    {
        try
        {
            request = WebRequest.Create(task.Url);
            response = request.GetResponse( );
            totalSize = (ulong) response.ContentLength;
            fileStream = new FileStream(filePath, FileMode.Create, FileAccess.ReadWrite);
            stopwatch.Start( );
            thread = new Thread(DownloadFile);
            thread.Start( );
        }
        catch (WebException e)
        {
            MessageBox.Show("无法下载\n" + e.Status.ToString( ));
            Close( );
        }
    }

    public void DownloadFile( )
    {
        webStream = response.GetResponseStream( );
        for (int i = 0; (i = webStream.Read(buf, 0, buf.Length)) > 0;)
        {
            try
            {
                size += (ulong) i;
                Dispatcher.Invoke(new UpdateValue(UpdateUI), size);
                fileStream.Write(buf, 0, i);
            }
            catch (IOException ex)
            {
                Logger.Write(ex, LogType.Warn);
            }
            while (cancelToken.Token.IsCancellationRequested)
            {
                Thread.Sleep(100);
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
        string hourPassed = timePassed / 3600 > 0 ? $"{timePassed / 3600}h" : "";
        string minPassed = timePassed % 3600 / 60 > 0 ? $"{timePassed % 3600 / 60}m" : "";
        string secPassed = timePassed % 60 > 0 ? $"{timePassed % 60}s" : "";
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
        thread.Abort( );
        File.Delete(filePath);
    }

    private void PauseClick(object o, RoutedEventArgs e)
    {
        if (PauseResumeButton.Content.ToString( ) == "暂停")
        {
            PauseResumeButton.Content = "继续";
            cancelToken.Cancel( );
        }
        else
        {
            PauseResumeButton.Content = "暂停";
            cancelToken = new CancellationTokenSource( );
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
        if (!finished) CancelTask( );
    }

    private void WindowSizeChanged(object o, SizeChangedEventArgs e)
        => FromURL.Content = Utils.ZipStr(task.Url, (int) (ActualWidth / 15));

    public void Dispose( )
    {
        fileStream.Dispose( );
        webStream.Dispose( );
        response.Dispose( );
        GC.SuppressFinalize(this);
    }
}
