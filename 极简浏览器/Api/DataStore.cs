﻿using System;
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

    private string XmlFile;
    private bool InitEmpty;
    private FileStream XmlStream;

    public DataStore(string xmlFile, bool initEmpty = true)
    {
        XmlFile = new FileInfo(xmlFile).FullName;
        InitEmpty = initEmpty;
        XmlStream = new(XmlFile, FileMode.OpenOrCreate);
        Read( );
    }

    public void Read( )
    {
        XmlReader reader = XmlReader.Create(XmlStream);
        try
        {
            Content = reader.ReadContentAs(typeof(List<T>), null) as List<T>;
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
        XmlStream.SetLength(0);
        XmlSerializer serializer = new(typeof(List<T>));
        serializer.Serialize(XmlStream, Content);
    }

    public void Dispose( )
    {
        XmlStream.Dispose( );
        GC.SuppressFinalize(this);
    }
}
