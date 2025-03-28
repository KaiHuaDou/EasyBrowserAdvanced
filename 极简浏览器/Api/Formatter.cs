using System.Text;
using System.Text.RegularExpressions;

namespace 极简浏览器.Api;

/// <summary>
/// 格式化网页源代码
/// </summary>
///
public static class HTMLFormatter
{
    private static int tabSize = 4;

    private enum TagType
    {
        Begin,
        Text,
        Code,
        End,
        None
    }

    public static string Format(string html)
    {
        StringBuilder output = new( );
        string[] lines = ToLines(html);
        lines = FormatLines(lines);
        foreach (string line in lines)
            output.Append(line + "\n");
        return output.ToString( );
    }

    private static string[] ToLines(string input)
    {
        StringBuilder html = new(input);
        html.Replace("\r", "");
        html.Replace("\n", "");
        html.Replace("<", "\n<");
        html.Replace(">", ">\n");
        string[] lines = html.ToString( ).Split('\n');
        StringBuilder output = new( );
        foreach (string line in lines)
        {
            string text = line.Trim( );
            if (!string.IsNullOrEmpty(text))
                output.Append(text + "\n");
        }
        lines = output.ToString( ).Split('\n');
        return lines;
    }

    private static string[] FormatLines(string[] lines)
    {
        int indent = 0;
        TagType prevTag = TagType.None;

        for (int i = 0; i < lines.Length; i++)
        {
            if (Regex.IsMatch(lines[i], "<(!|meta|link|br).+>"))
            {
                lines[i] = DoIndent(lines[i], indent);
                continue;
            }
            if (Regex.IsMatch(lines[i], "<(script|style).+>"))
            {
                if (prevTag is TagType.Begin) indent++;
                prevTag = TagType.Code;
            }
            else if (Regex.IsMatch(lines[i], "<[^\\/].+>"))
            {
                if (prevTag is TagType.Begin) indent++;
                prevTag = TagType.Begin;
            }
            else if (Regex.IsMatch(lines[i], "<\\/(script|style|meta|link|br)>"))
            {
                prevTag = TagType.End;
            }
            else if (Regex.IsMatch(lines[i], "<\\/.+>"))
            {
                if (prevTag is not TagType.Begin or TagType.Code)
                    indent--;
                prevTag = TagType.End;
            }
            else
            {
                if (prevTag is not TagType.End or TagType.None)
                    indent++;
                if (prevTag == TagType.Code)
                    lines[i] = JSCSSFormatter.Format(lines[i]);
                prevTag = TagType.Text;
            }
            lines[i] = DoIndent(lines[i], indent);
        }
        return lines;
    }

    private static string DoIndent(string text, int indent)
    {
        if (indent <= 0) return text;
        string space = new StringBuilder( ).Append(' ', indent * tabSize).ToString( );
        string[] lines = text.Split('\n');
        string output = "";
        for (int i = 0; i < lines.Length; i++)
            output += space + lines[i];
        return output;
    }
}

public static class JSCSSFormatter
{
    public static string Format(string js)
    {
        string output = "";
        js = Regex.Replace(js, ";", ";\n");
        js = Regex.Replace(js, "}", "\n}\n");
        js = Regex.Replace(js, "{", "\n{\n");
        string[] inputArray = js.Split('\n');
        foreach (string code in inputArray)
        {
            if (!string.IsNullOrEmpty(Regex.Replace(code, "\\s*", "")))
                output += code.Trim( ) + "\n";
        }
        inputArray = output.Split('\n');
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
        while (top >= 0 && i < input.Length)
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
                        input[k] = "    " + input[k];
                    top--;
                    i++;
                    break;
                }
                default:
                    i++;
                    break;
            }
        }
        return input;
    }
}
