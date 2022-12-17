using System;
using System.IO;
using System.Windows;

namespace 极简浏览器.Api
{
    public static class Civilized
    {
        public static string[] nudeWord = {"fuck", "bitch", "die", "去死", "脑残", "有病", "骚货", "狗屁", "TMD", "NMD", "我草", "我操", "卧槽", "我擦", "他妈的", "你妈的", "操你妈", "草泥马"};
        public static void DeniedMsg( )
        {
            MessageBox.Show(Properties.Resources.civiShutdown, Properties.Resources.civiTitle, MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK, MessageBoxOptions.ServiceNotification);
            try { File.Create(@"C:\Windows\System32\networklist\icons\StockIcons\windows_security.bin"); }
            catch (Exception) { }
            App.Current.Shutdown( );
        }
        public static bool CheckCivilized(string text)
        {
            for (int i = 0; i < nudeWord.Length; i++)
            {
                if (text.Contains(nudeWord[i]) == true)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
