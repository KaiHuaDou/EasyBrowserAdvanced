using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Xml.Serialization;
namespace 极简浏览器.Api
{
    public static class Configer
    {
        private static FileStream genGetStream(string fileName)
        {
            FileStream stream = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            return stream;
        }
        private static FileStream genSetStream(string fileName)
        {
            FileStream stream = new FileStream(fileName, FileMode.Create, FileAccess.ReadWrite);
            return stream;
        }
        public static void AddConfig(Config data, string fileName)
        {
            ObservableCollection<Config> savedData = GetConfig(fileName);
            savedData.Add(data);
            SaveConfig(savedData, fileName);
        }
        public static void RemoveConfig(Config data, string fileName)
        {
            ObservableCollection<Config> savedData = GetConfig(fileName);
            savedData.Remove(data);
            SaveConfig(savedData, fileName);
        }
        public static void SaveConfig(ObservableCollection<Config> data,  string fileName)
        {
            FileStream fs = genSetStream(fileName);
            XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<Config>));
            serializer.Serialize(fs, data);
            fs.Close();
        }
        public static ObservableCollection<Config> GetConfig(string fileName)
        {
            FileStream fs = genGetStream(fileName);
            XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<Config>));
            ObservableCollection<Config> config = new ObservableCollection<Config>();
            try
            {
                config = serializer.Deserialize(fs) as ObservableCollection<Config>;
                fs.Close();
            }
            catch (InvalidOperationException)
            {
                fs.Close();
                InitConfig(fileName);
            }
            return config;
        }
        public static void InitConfig(string fileName)
        {
            SaveConfig(new ObservableCollection<Config>(), fileName);
        }
    }
    public class Config
    {
        public Config( ) { }
        public Config(bool isChecked, string siteName, string siteAddress, string visitTime)
        {
            IsChecked = isChecked;
            SiteName = siteName;
            SiteAddress = siteAddress;
            VisitTime = visitTime;
        }
        public bool IsChecked { get; set; }
        public string SiteName { get; set; }
        public string SiteAddress { get; set; }
        public string VisitTime { get; set; }
    }
}