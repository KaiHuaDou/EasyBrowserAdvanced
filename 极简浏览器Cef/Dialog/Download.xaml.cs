﻿using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Threading;
using System.Windows;
using System.Windows.Threading;
using CefSharp;
using 极简浏览器.Api;

namespace 极简浏览器
{
    /// <summary>
    /// Download.xaml 的交互逻辑
    /// </summary>
    public partial class Download : Window
    {
        private static DownloadItem basicInfo = new DownloadItem();
        private string dlPath;
        private WebRequest httpReq;
        private WebResponse httpRes;
        private byte[] buf = new byte[short.MaxValue];
        private Thread dlThread;
        private Stream dlStream;
        private FileStream dlFS;
        private long length = 0;
        private long dlLen = 0;
        private long lastLen = 0;
        private double totalSec = 0;
        private delegate void updateData(string value);
        private updateData doFront;
        private System.Timers.Timer timer = new System.Timers.Timer(500);
        public Download(DownloadItem item, string savePath)
        {
            InitializeComponent();
            basicInfo = item;
            dlPath = savePath;
            FileName.Content = basicInfo.SuggestedFileName;
            timer.Elapsed += Timer_Elapsed;
            DownloadInit();
        }

        void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                Speed.Content = Convert.ToUInt32((dlLen - lastLen) / 1024) + "KB/s";
                lastLen = dlLen;
                totalSec += 0.5;
            });
        }

        public void DownloadInit()
        {
            try
            {
                httpReq = WebRequest.Create(basicInfo.Url);
                httpRes = httpReq.GetResponse();
                length = httpRes.ContentLength;
                Progress.Maximum = length;
                dlThread = new Thread(new ThreadStart(HttpDownloadFile));
                dlFS = new FileStream(dlPath, FileMode.Create, FileAccess.ReadWrite);
                timer.Enabled = true;
                dlThread.Start();
            }
            catch (WebException e)
            {
                MessageBox.Show("无法下载，原因：:" + e.Status.ToString());
                this.Close();
            }
        }

        public void HttpDownloadFile()
        {
            dlStream = httpRes.GetResponseStream();
            int i;
            doFront = new updateData(updateUI);
            while ((i = dlStream.Read(buf, 0, buf.Length)) > 0)
            {
                try
                {
                    dlLen += i;
                    Dispatcher.Invoke(doFront, dlLen.ToString());
                    dlFS.Write(buf, 0, i);
                }
                catch (IOException ex)
                {
                    Logger.Log(ex, LogType.Debug);
                }
            }
            timer.Enabled = false;
            dlFS.Close();
            dlStream.Close();
            Dispatcher.Invoke(() => { OpenButton.IsEnabled = true; });
        }
        void updateUI(string value)
        {
            try
            {
                Progress.Value = int.Parse(value);
                double percent = Progress.Value / Progress.Maximum * 100.0;
                Percent.Content = decimal.Round((decimal)percent, 2) + "%";
            }
            catch (Exception) { }
        }

        private void OpenButton_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(dlPath);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            dlThread.Abort();
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            FromURL.Content = "下载地址：" + basicInfo.Url.Substring(0, (int)(this.ActualWidth / 25));
        }
    }
}