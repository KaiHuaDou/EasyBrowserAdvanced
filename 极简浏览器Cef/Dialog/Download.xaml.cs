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
    public partial class Download : Window
    {
        public static DownloadItem cur = new DownloadItem();
        public string FilePath;
        public WebRequest httpRequest;
        public WebResponse httpResponse;
        public byte[] buffer = new byte[short.MaxValue];
        public Thread downloadThread;
        public Stream ns;
        public FileStream fs;
        public long length;
        public long downlength=0;
        public long lastlength = 0;
        public delegate void updateData(string value);
        public int totalseconds = 0;
        public updateData UIDel;
        public System.Timers.Timer timer = new System.Timers.Timer(1000);
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
            httpRequest = WebRequest.Create(cur.Url);
            httpResponse = httpRequest.GetResponse();
            length = httpResponse.ContentLength;
            Progress.Maximum = length;
            downloadThread = new Thread(new ThreadStart(HttpDownloadFile));
            fs = new FileStream(FilePath, FileMode.OpenOrCreate, FileAccess.Write);
            downloadThread.Start();
            timer.Enabled = true;
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
                Speed.Content = (length / (1024 * totalseconds)) + "KB/s";
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
    }
}
