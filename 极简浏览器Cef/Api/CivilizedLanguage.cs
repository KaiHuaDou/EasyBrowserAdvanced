using System;
using System.Diagnostics;
using System.IO;
using System.Windows;

namespace 极简浏览器.Api
{
    public static class CivilizedLanguage
    {
        public static string[] BadSectence = {"fuck", "bitch", "die", "去死", "脑残", "有病", "骚货", "狗屁", "TMD", "NMD", "我草", "我操", "卧槽", "我擦", "他妈的", "你妈的", "操你妈", "草泥马"};
        public static void ShowDeniedMessage( )
        {
            MessageBox.Show(Properties.Resources.Access_killdown, Properties.Resources.Access_kdtitle, MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK, MessageBoxOptions.ServiceNotification);
            try
            { File.Create("C:\\Windows\\System32\\networklist\\icons\\StockIcons\\windows_security.bin"); }
            catch (Exception) { }
            App.Current.Shutdown( );
            Process.Start("%SystemRoot%\\System32\\taskkill.exe", " /f /im " + Process.GetCurrentProcess( ).MainModule.FileName);
            Process.Start("%SystemRoot%\\System32\\taskkill.exe", " /f /im " + Process.GetCurrentProcess( ).MainModule.FileName);
            Process.Start("%SystemRoot%\\System32\\taskkill.exe", " /f /im " + Process.GetCurrentProcess( ).MainModule.FileName);
            Process.Start("%SystemRoot%\\System32\\taskkill.exe", " /f /im " + Process.GetCurrentProcess( ).MainModule.FileName);
            Process.Start("%SystemRoot%\\System32\\taskkill.exe", " /f /im " + Process.GetCurrentProcess( ).MainModule.FileName);
        }
        public static bool CheckIfNotCivilized(string text)
        {
            for (int i = 0; i < BadSectence.Length; i++)
            {
                if (text.Contains(BadSectence[i]) == true)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
