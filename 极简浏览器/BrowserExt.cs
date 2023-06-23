﻿using System.Windows;
using 极简浏览器.Api;

namespace 极简浏览器;

/// <summary>
/// 事件处理程序
/// </summary>
public partial class MainWindow : Window
{
    private void AboutClick(object o, RoutedEventArgs e)
        => new About( ).Show( );

    private void NewPageClick(object o, RoutedEventArgs e)
        => Instance.New( );

    private void GoBackClick(object o, RoutedEventArgs e)
        => Instance.GoBack(Id);

    private void GoForwardClick(object o, RoutedEventArgs e)
        => Instance.GoForward(Id);

    private void HelpClick(object o, RoutedEventArgs e)
        => new Help( ).Show( );

    private void RefreshClick(object o, RoutedEventArgs e)
        => Instance.Refresh(Id);

    private void SetBookMarkClick(object o, RoutedEventArgs e)
    {
        App.Bookmark.Content.Add(
            new Record
            {
                Check = false,
                Title = Instance.Title(Id),
                Url = Instance.Address(Id),
                Time = Utils.LocalTime
            }
        );
    }

    private void SettingClick(object o, RoutedEventArgs e)
        => new Setting( ).Show( );

    private void ViewSourceClick(object o, RoutedEventArgs e)
        => Instance.ViewSource(Id);

    private void HistoryClick(object o, RoutedEventArgs e)
        => new History( ).Show( );

    private void DevToolClick(object o, RoutedEventArgs e)
        => Instance.ShowDevTools(Id);

    private void TopmostChecked(object o, RoutedEventArgs e)
        => Topmost = !Topmost;
}
