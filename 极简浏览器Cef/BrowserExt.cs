﻿using System;
using System.Windows;
using CefSharp;
using 极简浏览器.Api;

namespace 极简浏览器
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window, IDisposable
    {
        private void About_Click(object sender, RoutedEventArgs e)
        { new About().Show(); }

        private void AddNewPageButton_Click(object sender, RoutedEventArgs e)
        { Instance.New(); }

        private void GoBackButton_Click(object sender, RoutedEventArgs e)
        { Browser.GoBack(); }

        private void GoForwardButton_Click(object sender, RoutedEventArgs e)
        { Browser.GoForward(); }

        private void Help_Click(object sender, RoutedEventArgs e)
        { new Help().Show(); }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        { Browser.Refresh(); }

        private void SetBookMark_Click(object sender, RoutedEventArgs e)
        {
            Configer.AddConfig(
                new Config(false, Browser.Core.Title,
                Browser.Core.Address, StdApi.LocalTime),
                FilePath.BookMark);
        }

        private void Setting_Click(object sender, RoutedEventArgs e)
        { new Setting().Show(); }

        private void ViewSource_Click(object sender, RoutedEventArgs e)
        { StdApi.ViewSource(); }

        private void ViewHistory_Click(object sender, RoutedEventArgs e)
        { new History().Show(); }

        private void privateBox_Click(object sender, RoutedEventArgs e)
        { App.Program.argus.IsPrivate = (bool)privateBox.IsChecked; }

        private void Topmost_Checked(object sender, RoutedEventArgs e)
        {
            this.Topmost = !this.Topmost;
            App.Program.argus.IsTopmost = this.Topmost;
        }

        private void DevToolsButton_Click(object sender, RoutedEventArgs e)
        { Browser.Core.ShowDevTools(); }
    }
}
