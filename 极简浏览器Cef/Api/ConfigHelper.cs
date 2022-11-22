using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Xml.Serialization;
namespace 极简浏览器.Api
{
    public static class ConfigHelper
    {
        private static FileStream GenStream(string fileName)
        {
            FileStream stream = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            return stream;
        }
        public static void ClearConfig(string fileName)
        {
            FileStream fs = GenStream(fileName);
            XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<ConfigData>));
            serializer.Serialize(fs, new ObservableCollection<ConfigData>());
            fs.Close();
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
            FileStream fs = GenStream(fileName);
            XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<ConfigData>));
            serializer.Serialize(fs, data);
            fs.Close();
        }
        public static ObservableCollection<ConfigData> GetConfig(string fileName)
        {
            FileStream fs = GenStream(fileName);
            XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<ConfigData>));
            ObservableCollection<ConfigData> config = new ObservableCollection<ConfigData>();
            try
            {
                config = serializer.Deserialize(fs) as ObservableCollection<ConfigData>;
                fs.Close();
            }
            catch (Exception)
            {
                fs.Close();
                InitConfig(fileName);
                MessageBox.Show("读取历史记录错误，已重置文件");
            }
            return config;
        }
        public static void InitConfig(string fileName)
        {
            SaveConfig(new ObservableCollection<ConfigData>(), fileName);
        }
    }
    public class ConfigDatas: ObservableCollection<ConfigData>
    {
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