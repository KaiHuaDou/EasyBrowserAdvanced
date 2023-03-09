using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
namespace 极简浏览器.Api
{
    public static class DataMgr<T>
    {
        private static readonly XmlSerializer serializer = new XmlSerializer(typeof(HashSet<T>));
        private static FileStream GenStream(string fileName, FileMode mode)
        {
            FileStream stream = new FileStream(fileName, mode, FileAccess.ReadWrite);
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
        public static void Save(HashSet<T> data, string fileName)
        {
            FileStream fs = GenStream(fileName, FileMode.Create);
            serializer.Serialize(fs, data);
            fs.Close( );
        }
        public static HashSet<T> Get(string fileName)
        {
            FileStream fs = GenStream(fileName, FileMode.OpenOrCreate);
            HashSet<T> config = new HashSet<T>( );
            try
            {
                config = serializer.Deserialize(fs) as HashSet<T>;
                fs.Close( );
            }
            catch (InvalidOperationException)
            {
                fs.Close( );
                Init(fileName);
            }
            return config;
        }
        public static void Init(string fileName)
        {
            Save(new HashSet<T>( ), fileName);
        }
    }
    public class Config
    {
        public Config( ) { }
        public Config(bool c, string n, string a, string t)
        {
            Check = c;
            Title = n;
            Url = a;
            Time = t;
        }
        public bool Check { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public string Time { get; set; }
    }
}
