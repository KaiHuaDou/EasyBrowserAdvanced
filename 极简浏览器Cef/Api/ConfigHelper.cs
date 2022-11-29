using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Xml.Serialization;
namespace 极简浏览器.Api
{
    public static class ConfigHelper
    {
        private static FileStream GenStreamGet(string fileName)
        {
            FileStream stream = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            return stream;
        }
        private static FileStream GenStreamSet(string fileName)
        {
            FileStream stream = new FileStream(fileName, FileMode.Create, FileAccess.ReadWrite);
            return stream;
        }
        public static void AddConfig(ConfigData data, string fileName)
        {
            ObservableCollection<ConfigData> savedData = GetConfig(fileName);
            savedData.Add(data);
            SaveConfig(savedData, fileName);
        }
        public static void RemoveConfig(ConfigData data, string fileName)
        {
            ObservableCollection<ConfigData> savedData = GetConfig(fileName);
            savedData.Remove(data);
            SaveConfig(savedData, fileName);
        }
        public static void SaveConfig(ObservableCollection<ConfigData> data,  string fileName)
        {
            FileStream fs = GenStreamSet(fileName);
            XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<ConfigData>));
            serializer.Serialize(fs, data);
            fs.Close();
        }
        public static ObservableCollection<ConfigData> GetConfig(string fileName)
        {
            FileStream fs = GenStreamGet(fileName);
            XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<ConfigData>));
            ObservableCollection<ConfigData> config = new ObservableCollection<ConfigData>();
            try
            {
                config = serializer.Deserialize(fs) as ObservableCollection<ConfigData>;
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
            SaveConfig(new ObservableCollection<ConfigData>(), fileName);
        }
    }
    public class ConfigData
    {
        public ConfigData( ) { }
        public ConfigData(bool isChecked, string siteName, string siteAddress, string visitTime)
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