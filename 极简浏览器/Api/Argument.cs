namespace 极简浏览器.Api;

public class Argument(bool topmost = false, bool isPrivate = false, bool singleWindow = false)
{
    public bool Topmost { get; set; } = topmost;
    public bool IsPrivate { get; set; } = isPrivate;
    public bool SingleWindow { get; set; } = singleWindow;
}
