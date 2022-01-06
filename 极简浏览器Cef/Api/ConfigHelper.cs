using System.Collections.ObjectModel;
using System.IO;
using System.Xml.Serialization;
namespace 极简浏览器.Api
{
    public static class ConfigHelper
    {
        private static FileStream GenStream(string fileName)
        {
            return new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.ReadWrite);
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
            return serializer.Deserialize(fs) as ObservableCollection<ConfigData>;
        }
        public static void InitConfig(string fileName)
        {
            FileStream fs = GenStream(fileName);
            XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<ConfigData>));
            serializer.Serialize(fs, new ObservableCollection<ConfigData>());
            fs.Close();
        }
    }
    public class ConfigDatas: ObservableCollection<ConfigData>
    {
    }
    public class ConfigData
    {
        public bool IsChecked
        {
            get; set;
        }
        public string SiteName
        {
            get; set;
        }
        public string SiteAdddress
        {
            get; set;
        }
        public string VisitTime
        {
            get; set;
        }
    }
}