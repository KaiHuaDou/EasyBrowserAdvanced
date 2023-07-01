using System.Text;
using System.Text.RegularExpressions;

namespace 极简浏览器.Api;

/// <summary>
/// 格式化网页源代码
/// </summary>
/// 
public static class Formatter
{
    public static string Format(string code)
    {
        code = HTMLFormatter.Format(code);
        code = JSCSSFormatter.Format(code);
        return code;
    }
}

public static class HTMLFormatter
{
    public static string Format(string html)
    {
        StringBuilder output = new( );
        string[] arrayString = ToFormatArray(html);
        arrayString = FormatImp(arrayString);
        foreach (string ss in arrayString)
            output.Append(ss + "\n");
        return output.ToString( );
    }

    private static string[] ToFormatArray(string inputHtml)
    {
        StringBuilder html = new(inputHtml);
        StringBuilder output = new( );
        html.Replace("\r\n", "");
        html.Replace("<", "\n<");
        html.Replace(">", ">\n");

        string[] tagArray = html.ToString( ).Split('\n');
        foreach (string s in tagArray)
        {
            string text = s.Trim( );
            if (!string.IsNullOrEmpty(text))
                output.Append(text + "\n");
        }
        tagArray = output.ToString( ).Split('\n');
        return tagArray;
    }

    private static string[] FormatImp(string[] tagArray)
    {
        int indent = 4;
        int count = 0;
        TagType lastTag = TagType.None;

        for (int i = 0; i < tagArray.Length; i++)
        {
            if (Regex.IsMatch(tagArray[i], "<[^\\/].+>"))
            {
                if (TagType.Begin == lastTag)
                    count++;
                tagArray[i] = CreateBlank(indent * count) + tagArray[i];
                lastTag = TagType.Begin;
            }
            else if (Regex.IsMatch(tagArray[i], "<\\/.+>"))
            {
                if (lastTag is TagType.Text or TagType.End)
                    count--;
                tagArray[i] = CreateBlank(indent * count) + tagArray[i];
                lastTag = TagType.End;
            }
            else
            {
                if (lastTag == TagType.Begin)
                    count++;
                tagArray[i] = CreateBlank(indent * count) + tagArray[i];
                lastTag = TagType.Text;
            }
        }
        return tagArray;
    }

    private static string CreateBlank(int len)
    {
        if (len <= 0)
            return "";
        StringBuilder output = new( );
        for (int i = 0; i < len; i++)
            output.Append(' ');
        return output.ToString( );
    }

    private enum TagType
    {
        Begin,
        Text,
        End,
        None
    }
}

public static class JSCSSFormatter
{
    public static string Format(string js)
    {
        string output = "";
        js = Regex.Replace(js, ";", ";\n");
        js = Regex.Replace(js, "}", "\n } \n");
        js = Regex.Replace(js, "{", "\n { \n");
        char[] cc = new char[1] { '\n', };
        string[] inputArray = js.Split(cc);
        foreach (string code in inputArray)
        {
            if (!string.IsNullOrEmpty(Regex.Replace(code, "\\s*", "")))
                output += code.Trim( ) + "\n";
        }
        inputArray = output.Split(cc);
        output = "";
        inputArray = FormatBracket(inputArray);
        foreach (string code in inputArray)
            output += code + "\n";
        output = Regex.Replace(output, @"(?<Function>(?<=\r\n)\s*(int|long|void|char|bool|string|(unsigned\s*int)|(short\s*int)|unsigned|short|(long\s*int)|)\s*\*?\s*[A-Za-z_]*\w*\s*\(.*?\)\s*(?=\r\n|{))", "\r\n${Function}");
        output = Regex.Replace(output, "\n\n(?<A>(\\s)*?(if|for|while|do)\\()", "\r\n${A}");
        return output;
    }

    private struct Place
    {
        public string data;
        public int place;
    }

    private static string[] FormatBracket(string[] input)
    {
        Place[] place = new Place[input.Length];
        int top = -1;
        try
        {
            int j;
            for (j = 0; j < input.Length; j++)
            {
                if (input[j] == "{")
                {
                    top++;
                    place[top].data = "{";
                    place[top].place = j;
                    break;
                }
            }
            int i = j + 1;
            while (top >= 0 || i < input.Length)
            {
                switch (input[i])
                {
                    case "{":
                        top++;
                        place[top].data = "{";
                        place[top].place = i;
                        i++;
                        break;
                    case "}":
                    {
                        int place1 = place[top].place;
                        int place2 = i;
                        int k;
                        for (k = place1 + 1; k < place2; k++)
                        {
                            input[k] = "    " + input[k];
                        }
                        top--;
                        i++;
                        break;
                    }
                    default:
                        i++;
                        break;
                }
            }
        }
        catch { throw; }
        return input;
    }
}
