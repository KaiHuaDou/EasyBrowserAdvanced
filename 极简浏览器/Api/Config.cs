using System.Collections.Generic;
using System.ComponentModel;
using 极简浏览器.Resources;

namespace 极简浏览器.Api;

public class Config
{
    // 静态对象
    public const string mainPageDefault = "about:blank";
    public const string searchEngineDefault = "https://cn.bing.com/search?q=";
    public const string UANormal = "Mozilla/5.0 (Windows NT 6.2; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Easy/3.4.7.2 Chrome/87.0.4280.141 Safari/537.36";
    public const string UACheated = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Easy/3.4.7.2 Chrome/114.0.0.0 Safari/537.36";
    public static readonly Dictionary<string, string> UITheme = new( )
    {
        ["Classic"] = Text.UITheme_Classic,
        ["Luna.NormalColor"] = Text.UITheme_Luna_NormalColor,
        ["Luna.Homestead"] = Text.UITheme_Luna_Homestead,
        ["Luna.Metallic"] = Text.UITheme_Luna_Metallic,
        ["Royale.NormalColor"] = Text.UITheme_Royale_NormalColor,
        ["Aero.NormalColor"] = Text.UITheme_Aero_NormalColor,
        ["Aero2.NormalColor"] = Text.UITheme_Aero2_NormalColor
    };

    // 实例对象
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
