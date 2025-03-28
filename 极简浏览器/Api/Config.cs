using System.ComponentModel;

namespace 极简浏览器.Api;

public enum UIThemes
{
    AeroNormalColor = 0, Aero2NormalColor,
    LunaNormalColor, LunaHomestead, LunaMetallic, RoyaleNormalColor,
    Classic
}

public class Config
{
    public const string VERSION = "v3.4.7.2 Release";

    // 静态对象
    public const string mainPageDefault = "about:blank";
    public const string searchEngineDefault = "https://cn.bing.com/search?q=";
    public const string UACheated = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Easy/3.4.7.2 Chrome/123.0.0.0 Safari/537.36\n";

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

    [DefaultValue(false)]
    public bool DisableGPU { get; set; }

    [DefaultValue((int) UIThemes.AeroNormalColor)]
    public int UITheme { get; set; }
}
