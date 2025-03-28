using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Threading;
using System.Timers;
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
    private long totalSize, size, lastSize;
    private double totalSec;
    private bool finished;

    private System.Timers.Timer timer = new(500);
    private delegate void UpdateValue(long value);

    public Download(DownloadItem item, string path)
    {
        InitializeComponent( );
        task = item;
        filePath = path;
        FileName.Content = task.SuggestedFileName;
        timer.Elapsed += TimerElapsed;
        DownloadInit( );
    }

    private void TimerElapsed(object o, ElapsedEventArgs e)
    {
        Dispatcher.Invoke(( ) =>
        {
            Speed.Content = (size - lastSize) / 1024 + "KB/s";
            lastSize = size;
            totalSec += 0.5;
        });
    }

    public void DownloadInit( )
    {
        try
        {
            request = WebRequest.Create(task.Url);
            response = request.GetResponse( );
            totalSize = response.ContentLength;
            fileStream = new FileStream(filePath, FileMode.Create, FileAccess.ReadWrite);
            timer.Enabled = true;
            thread = new Thread(HttpDownloadFile);
            thread.Start( );
        }
        catch (WebException e)
        {
            MessageBox.Show("无法下载\n" + e.Status.ToString( ));
            Close( );
        }
    }

    public void HttpDownloadFile( )
    {
        webStream = response.GetResponseStream( );
        for (int i = 0; (i = webStream.Read(buf, 0, buf.Length)) > 0;)
        {
            try
            {
                size += i;
                Dispatcher.Invoke(new UpdateValue(UpdateUI), size);
                fileStream.Write(buf, 0, i);
            }
            catch (IOException ex)
            {
                Logger.Write(ex, LogType.Warn);
            }
        }
        timer.Enabled = false;
        finished = true;
        fileStream.Close( );
        webStream.Close( );
        Dispatcher.Invoke(( ) =>
        {
            OpenButton.IsEnabled = true;
            CancelButton.Content = "关闭";
        });
    }

    private void UpdateUI(long value)
    {
        double percent = value * 100 / totalSize;
        Progress.Value = percent;
        Percent.Content = decimal.Round((decimal) percent, 2) + "%";
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
        timer.Dispose( );
        fileStream.Dispose( );
        webStream.Dispose( );
        response.Dispose( );
        GC.SuppressFinalize(this);
    }
}
