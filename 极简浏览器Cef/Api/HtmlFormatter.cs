using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace 极简浏览器.Api
{
    public static class Formatter
    {
        public static string Format(string text)
        {
            text = HtmlFormatter.FormatHtml(text);
            text = CssFormater.FormatCss(text);
            return text;
        }
    }
    public class HtmlFormatter
    {
        public static string FormatHtml(string inputString)
        {
            StringBuilder outputString = new StringBuilder( );
            string[] arrayString = ToFormatArray(inputString);
            arrayString = FormatImp(arrayString);

            foreach (string ss in arrayString)
            {
                outputString.Append(ss + "\r\n");
            }

            return outputString.ToString( );

        }

        private static string[] ToFormatArray(string inputString)
        {
            StringBuilder workString = new StringBuilder(inputString);
            StringBuilder outputString = new StringBuilder( );
            //去掉换行
            workString.Replace("\r\n", "");
            //添加必须的换行
            workString.Replace("<", "\n<");
            workString.Replace(">", ">\n");

            char[] cc = new char[1] { '\n' };
            string[] arrayString = workString.ToString( ).Split(cc);
            //去掉空行
            foreach (string s in arrayString)
            {
                // 去掉前前后空白字符
                string text = s.Trim( );
                if (text != "")
                {
                    outputString.Append(text + "\n");
                }
            }

            // 不包含空行的字符串数组
            arrayString = outputString.ToString( ).Split(cc);

            return arrayString;
        }

        private static string[] FormatImp(string[] arrayString)
        {
            int indent = 4;
            int count = 0;
            LastTagType lastTag = LastTagType.None;

            // 找第一个HTML开始标签，<html>
            for (int i = 0; i < arrayString.Length; i++)
            {
                if (Regex.IsMatch(arrayString[i], "<[^\\/].+>")) //匹配HTML开始标签 <html> <head> <title> <font>
                {
                    if (LastTagType.BeginTag == lastTag)
                    {
                        count++;
                    }
                    arrayString[i] = CreateBlank(indent * count) + arrayString[i];
                    lastTag = LastTagType.BeginTag;
                }
                else if (Regex.IsMatch(arrayString[i], "<\\/.+>")) //匹配HTML开始标签 </html> </body> </font>
                {
                    if ((lastTag == LastTagType.Text) || (lastTag == LastTagType.EndTag))
                    {
                        count--;
                    }
                    arrayString[i] = CreateBlank(indent * count) + arrayString[i];
                    lastTag = LastTagType.EndTag;
                }
                else
                {
                    if (lastTag == LastTagType.BeginTag)
                    {
                        count++;
                    }
                    arrayString[i] = CreateBlank(indent * count) + arrayString[i];
                    lastTag = LastTagType.Text;
                }
            }

            return arrayString;
        }

        private static string CreateBlank(int length)
        {
            if (length <= 0)
            {
                return "";
            }

            StringBuilder sb = new StringBuilder( );
            for (int i = 0; i < length; i++)
            {
                sb.Append(" ");
            }
            return sb.ToString( );
        }

        private enum LastTagType
        {
            BeginTag,
            Text,
            EndTag,
            None
        }
    }
    public class CssFormater
    {
        public static string FormatCss(string InputString)
        {
            string OutputString = "";
            InputString = Regex.Replace(InputString, ";", ";\n");//遇分号换行
            InputString = Regex.Replace(InputString, "}", "\n } \n");//遇"}"换行
            InputString = Regex.Replace(InputString, "{", "\n { \n");//遇"{"换行          
            char[] cc = new char[1] { '\n', };
            string[] ArryInput = new string[5000];
            ArryInput = InputString.Split(cc);
            //去掉空行
            foreach (string ss in ArryInput)
            {
                string ss1 = Regex.Replace(ss, "\\s*", "");
                if (ss1 != "")
                {
                    OutputString += ss.Trim( ) + "\n";
                }
            }
            ArryInput = OutputString.Split(cc);
            OutputString = "";
            ArryInput = FormatKUOHAO(ArryInput);//处理{}匹配            
            foreach (string ss in ArryInput)
            {
                OutputString += ss + "\r\n";
            }

            OutputString = Regex.Replace(OutputString, @"(?<Function>(?<=\r\n)\s*(int|long|void|char|bool|string|(unsigned\s*int)|(short\s*int)|unsigned|short|(long\s*int)|)\s*\*?\s*[A-Za-z_]*\w*\s*\(.*?\)\s*(?=\r\n|{))", "\r\n${Function}");
            OutputString = Regex.Replace(OutputString, "\r\n\r\n(?<A>(\\s)*?(if|for|while|do)\\()", "\r\n${A}");
            return OutputString;
        }
        /// <summary>
        /// 匹配括号所用的数据结构
        /// </summary>
        struct Place
        {
            public string data;
            public int place;
        }

        public static string[] FormatKUOHAO(string[] Intput)
        {
            Place[] MyPlace = new Place[5000];
            int Top = -1;
            int i = 0;  //遍利数组Input  
            int j = 0;
            int k = 0;
            try
            {
                //找第一个”{“
                for (j = 0; j < Intput.Length; j++)
                {
                    if (Intput[j] == "{")
                    {
                        Top++;
                        MyPlace[Top].data = "{";
                        MyPlace[Top].place = j;
                        break;
                    }
                }
                i = j + 1;
                while (Top >= 0 || i < Intput.Length)
                {
                    if (Intput[i] == "{")
                    {
                        Top++;
                        MyPlace[Top].data = "{";
                        MyPlace[Top].place = i;
                        i++;
                    }
                    else if (Intput[i] == "}")
                    {
                        int P1 = MyPlace[Top].place;
                        int P2 = i;
                        for (k = P1 + 1; k < P2; k++)
                        {
                            Intput[k] = "    " + Intput[k];
                        }
                        //Output[i] += "\n";
                        Top--;
                        i++;
                    }
                    else
                    {
                        i++;
                    }
                }
            }
            catch (Exception e)
            {
                Logger.Log(e, LogType.Other, false);
            }
            return Intput;
        }
    }
}