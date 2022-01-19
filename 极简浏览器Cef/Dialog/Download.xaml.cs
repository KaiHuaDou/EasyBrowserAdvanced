using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Threading;
using System.Windows;
using System.Windows.Threading;
using CefSharp;

namespace 极简浏览器
{
    /// <summary>
    /// Download.xaml 的交互逻辑
    /// </summary>
    public partial class Download : Window, IDisposable
    {
        private static DownloadItem cur = new DownloadItem();
        private string FilePath;
        private WebRequest httpRequest;
        private WebResponse httpResponse;
        private byte[] buffer = new byte[short.MaxValue];
        private Thread downloadThread;
        private Stream ns;
        private FileStream fs;
        private long length;
        private long downlength=0;
        private long lastlength = 0;
        private delegate void updateData(string value);
        private int totalseconds = 0;
        private updateData UIDel;
        private System.Timers.Timer timer = new System.Timers.Timer(1000);
        public Download(DownloadItem item, string path)
        {
            InitializeComponent();
            cur = item;
            FilePath = path;
            FileName.Content = cur.SuggestedFileName;
            FromURL.Content = "从" + cur.Url.Substring(0, 20) + "…下载";
            timer.Elapsed += Timer_Elapsed;
            DownloadInit();
        }

        void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                Speed.Content = ((downlength - lastlength) / 1024) + "KB/s";
                lastlength = downlength;
                totalseconds++;
            });
        }

        public void DownloadInit()
        {
            try
            {
                httpRequest = WebRequest.Create(cur.Url);
                httpResponse = httpRequest.GetResponse();
                length = httpResponse.ContentLength;
                Progress.Maximum = length;
                downloadThread = new Thread(new ThreadStart(HttpDownloadFile));
                fs = new FileStream(FilePath, FileMode.OpenOrCreate, FileAccess.Write);
                downloadThread.Start();
                timer.Enabled = true;
            }
            catch(WebException e)
            {
                MessageBox.Show("无法下载，原因：:" + e.Status.ToString());
                this.Close();
            }
        }

        public void HttpDownloadFile()
        {
            ns = httpResponse.GetResponseStream();
            int i;
            UIDel = new updateData(updateUI);
            try
            {
                while ((i = ns.Read(buffer, 0, buffer.Length)) > 0)
                {
                    downlength += i;
                    string value = downlength.ToString();
                    Dispatcher.Invoke(UIDel, value);
                    fs.Write(buffer, 0, i);
                }
                Dispatcher.Invoke(() =>
                {
                    Speed.Content = (length / (1024 * totalseconds)) + "KB/s";
                });
            }
            catch (DivideByZeroException) { }
            catch (IOException) { }
            timer.Enabled = false;
            fs.Close();
            ns.Close();
            Dispatcher.Invoke(() =>
            {
                OpenButton.IsEnabled = true;
            });
        }
        void updateUI(string value)
        {
            try
            {
                Progress.Value = int.Parse(value);
                double percent = Progress.Value / Progress.Maximum * 100.0;
                Percent.Content = decimal.Round((decimal)percent, 1) + "%";
            }
            catch (Exception) { }
        }

        private void OpenButton_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(FilePath);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            downloadThread.Abort();
        }

        public void Dispose()
        {
            fs.Close();
            fs.Dispose();
            timer.Close();
            timer.Dispose();
        }
    }
}
