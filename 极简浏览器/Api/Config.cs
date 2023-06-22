using System.ComponentModel;

namespace 极简浏览器.Api;

public class Config
{
    public const string mainPageDefault = "about:blank";
    public const string searchEngineDefault = "https://cn.bing.com/search?q=";
    public const string UaCheated = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Easy/3.4.7.2 Chrome/87.0.4280.141 Safari/537.36";

    private string mainPage = mainPageDefault;
    private string searchEngine = searchEngineDefault;

    [DefaultValue(mainPageDefault)]
    public string MainPage
    {
        get => mainPage;
        set => mainPage = string.IsNullOrWhiteSpace(value) ? mainPage : value;
    }

    [DefaultValue(searchEngineDefault)]
    public string SearchEngine
    {
        get => searchEngine;
        set => searchEngine = string.IsNullOrWhiteSpace(value) ? searchEngine : value;
    }

    [DefaultValue(false)]
    public bool CheatUA { get; set; }
}
