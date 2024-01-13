using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace 极简浏览器.Api;

public class Record
{
    public bool Check { get; set; }
    public string Title { get; set; }
    public string Url { get; set; }
    public string Time { get; set; }
}

public class DataStore<T> : IDisposable where T : new()
{
    public List<T> Content { get; private set; }

    private string File;
    private bool InitEmpty;
    private FileStream Stream;
    private XmlReader Reader;

    public DataStore(string xmlFile, bool initEmpty = true)
    {
        File = new FileInfo(xmlFile).FullName;
        InitEmpty = initEmpty;
        Stream = new(File, FileMode.OpenOrCreate);
        Reader = XmlReader.Create(Stream);
        Read( );
    }

    public void Read( )
    {
        XmlSerializer serializer = new(typeof(List<T>));
        try
        {
            Content = serializer.Deserialize(Reader) as List<T>;
            if (Content.Count == 0 && !InitEmpty)
                Content.Add(new T( ));
        }
        catch
        {
            Content = new List<T>( );
            if (!InitEmpty)
                Content.Add(new T( ));
        }
    }

    public void Save( )
    {
        Stream.SetLength(0);
        XmlSerializer serializer = new(typeof(List<T>));
        serializer.Serialize(Stream, Content);
    }

    public void Dispose( )
    {
        Reader.Dispose( );
        Stream.Dispose( );
        GC.SuppressFinalize(this);
    }
}
