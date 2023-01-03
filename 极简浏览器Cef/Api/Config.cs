using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
namespace 极简浏览器.Api
{
    public static class Configer<T>
    {
        private static XmlSerializer serializer = new XmlSerializer(typeof(HashSet<T>));
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
            serializer.Serialize(fs, data);
            fs.Close();
        }
        public static HashSet<T> Get(string fileName)
        {
            FileStream fs = genGetStream(fileName);
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
        public Config(bool c, string n, string a, string t)
        {
            IsChecked = c;
            SiteName = n;
            SiteAddress = a;
            VisitTime = t;
        }
        public bool IsChecked { get; set; }
        public string SiteName { get; set; }
        public string SiteAddress { get; set; }
        public string VisitTime { get; set; }
    }
}