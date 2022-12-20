using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
namespace 极简浏览器.Api
{
    public static class Configer<T>
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
        public static void Add(T data, string fileName)
        {
            HashSet<T> savedData = Get(fileName);
            savedData.Add(data);
            Save(savedData, fileName);
        }
        public static void Remove(T data, string fileName)
        {
            HashSet<T> savedData = Get(fileName);
            savedData.Remove(data);
            Save(savedData, fileName);
        }
        public static void Save(HashSet<T> data,  string fileName)
        {
            FileStream fs = genSetStream(fileName);
            XmlSerializer serializer = new XmlSerializer(typeof(HashSet<T>));
            serializer.Serialize(fs, data);
            fs.Close();
        }
        public static HashSet<T> Get(string fileName)
        {
            FileStream fs = genGetStream(fileName);
            XmlSerializer serializer = new XmlSerializer(typeof(HashSet<T>));
            HashSet<T> config = new HashSet<T>();
            try
            {
                config = serializer.Deserialize(fs) as HashSet<T>;
                fs.Close();
            }
            catch (InvalidOperationException)
            {
                fs.Close();
                Init(fileName);
            }
            return config;
        }
        public static void Init(string fileName)
        {
            Save(new HashSet<T>(), fileName);
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