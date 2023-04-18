using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Xml.Serialization;

namespace 极简浏览器.Api;

public static class DataMgr<T>
{
    private static XmlSerializer serializer = new(typeof(ObservableCollection<T>));

    private static FileStream GenStream(string fileName, FileMode mode)
    {
        FileStream stream = new(fileName, mode, FileAccess.ReadWrite);
        return stream;
    }

    public static void Init(string fileName)
        => Save(new ObservableCollection<T>( ), fileName);

    public static void Add(T data, string fileName)
    {
        ObservableCollection<T> savedData = Get(fileName);
        savedData.Add(data);
        Save(savedData, fileName);
    }
    public static void Remove(T data, string fileName)
    {
        ObservableCollection<T> savedData = Get(fileName);
        savedData.Remove(data);
        Save(savedData, fileName);
    }
    public static void Save(ObservableCollection<T> data, string fileName)
    {
        FileStream fs = GenStream(fileName, FileMode.Create);
        serializer.Serialize(fs, data);
        fs.Close( );
    }
    public static ObservableCollection<T> Get(string fileName)
    {
        FileStream fs = GenStream(fileName, FileMode.OpenOrCreate);
        ObservableCollection<T> config = new( );
        try
        {
            config = serializer.Deserialize(fs) as ObservableCollection<T>;
            fs.Close( );
        }
        catch (InvalidOperationException)
        {
            fs.Close( );
            Init(fileName);
        }
        return config;
    }
}

[Serializable]
public class Config
{
    public Config( ) { }
    public bool Check { get; set; }
    public string Title { get; set; }
    public string Url { get; set; }
    public string Time { get; set; }
}
