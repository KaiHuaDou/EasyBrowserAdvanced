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
    private static DownloadItem task = new( );
    private readonly string filePath;
    private WebRequest request;
    private WebResponse response;
    private byte[] buf = new byte[short.MaxValue];
    private Thread thread;
    private Stream webStream;
    private FileStream fileStream;
    private long totalSize, size, lastSize;
    private double totalSec;
    private delegate void UpdateValue(int value);
    private System.Timers.Timer timer = new(500);

    public Download(DownloadItem item, string path)
    {
        InitializeComponent( );
        task = item;
        filePath = path;
        FileName.Content = task.SuggestedFileName;
        timer.Elapsed += Timer_Elapsed;
        DownloadInit( );
    }

    private void Timer_Elapsed(object o, ElapsedEventArgs e)
    {
        Dispatcher.Invoke(( ) =>
        {
            Speed.Content = Convert.ToUInt32((size - lastSize) / 1024) + "KB/s";
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
            Progress.Maximum = totalSize;
            thread = new Thread(new ThreadStart(HttpDownloadFile));
            fileStream = new FileStream(filePath, FileMode.Create, FileAccess.ReadWrite);
            timer.Enabled = true;
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
                Logger.Log(ex, LogType.Debug);
            }
        }
        timer.Enabled = false;
        fileStream.Close( );
        webStream.Close( );
        Dispatcher.Invoke(( ) => OpenButton.IsEnabled = true);
    }

    private void UpdateUI(int value)
    {
        Progress.Value = value;
        double percent = value / Progress.Maximum * 100.0;
        Percent.Content = decimal.Round((decimal) percent, 2) + "%";
    }

    private void OpenClick(object o, RoutedEventArgs e)
        => Process.Start(filePath);

    private void WindowClosing(object o, CancelEventArgs e)
        => thread.Abort( );

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
