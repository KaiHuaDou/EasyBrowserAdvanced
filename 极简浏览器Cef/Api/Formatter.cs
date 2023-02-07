using System;
using System.Collections;
using System.IO;
using System.Text;
using System.Xml;
namespace 极简浏览器
{
    public static class Formatter
    {
        public static string FormartHtml(string str, bool bLineAndIndent)
        {
            XmlDocument document1 = XFormatHtml(str);
            if (bLineAndIndent)
            {
                StringBuilder builder1 = new StringBuilder( );
                XmlTextWriter writer1 = new XmlTextWriter(new StringWriter(builder1));
                writer1.IndentChar = ' ';
                writer1.Indentation = 4;
                writer1.Formatting = Formatting.Indented;
                document1.DocumentElement.WriteContentTo(writer1);
                return builder1.ToString( );
            }
            return document1.DocumentElement.InnerXml;
        }
        private static XmlDocument XFormatHtml(string str)
        {
            XmlDocument document1 = new XmlDocument( );
            document1.LoadXml("<xml/>");
            HtmlFormatter.ParseHtml(document1.DocumentElement, str);
            return document1;
        }
    }
    internal class HtmlFormatter
    {
        private static Hashtable hashtable;
        private const string elementS = ",LINK,META,BASE,BGSOUND,BR,HR,INPUT,PARAM,IMG,AREA,";
        private const string elementD = ",IFRAME,A,P,DIV,OBJECT,ADDRESS,BIG,BLOCKQUOTE,B,CAPTION,CENTER,CITE,CODE,\r\n\t\t\t\t\t\t\t\t\t,DD,DFN,DIR,DL,DT,EM,FONT,FORM,H1,H2,H3,H4,H5,H6,HEAD,HTML,I,LI,MAP,MENU,OL,OPTION,\r\n\t\t\t\t\t\t\t\t\t,PRE,SELECT,SMALL,STRIKE,STRONG,SUB,SUP,TABLE,TD,TEXTAREA,TH,TITLE,TR,TT,U,";
        private XmlDocument document;
        private string gStr1;
        private string gStr2;
        private int gNum1;
        private int gNum2;
        private static char[] filter = new char[2] { '&', ';' };
        private static char[] outFilter = new char[2] { '&', ';' };
        private string a( )
        {
            StringBuilder builder1 = new StringBuilder( );
            int num1 = this.gNum2;
            builder1.Append(this.e( ));
            this.gNum2 = num1;
            if (builder1.Length == 0)
            {
                return null;
            }
            return builder1.ToString( );
        }
        private string a(char ch)
        {
            if (this.gNum2 >= this.gNum1)
            {
                return "";
            }
            int num1 = this.gStr1.IndexOf(ch, this.gNum2);
            if (num1 == -1)
            {
                return this.e( );
            }
            int num2 = this.gStr1.IndexOf("<", this.gNum2, num1 - this.gNum2);
            if (num2 != -1)
            {
                num1 = num2;
            }
            string text1 = this.gStr1.Substring(this.gNum2, num1 - this.gNum2);
            this.gNum2 = num1 + 1;
            return text1;
        }
        private string a(out string str)
        {
            int num1 = this.gNum2;
            str = this.d( );
            string text1 = this.b( );
            this.gNum2 = num1;
            return text1;
        }
        private string a(params char[] chars)
        {
            if (this.gNum2 >= this.gNum1)
            {
                return "";
            }
            int num1 = this.gStr1.IndexOfAny(chars, this.gNum2);
            if (num1 == -1)
            {
                return this.e( );
            }
            int num2 = this.gStr1.IndexOf("<", this.gNum2, num1 - this.gNum2);
            if (num2 != -1)
            {
                num1 = num2;
            }
            string text1 = this.gStr1.Substring(this.gNum2, num1 - this.gNum2);
            this.gNum2 = num1 + 1;
            return text1;
        }
        private void a(XmlElement arg)
        {
            string text1 = this.b( );
            if (text1 != null)
            {
                this.gNum2 += text1.Length;
                arg.AppendChild(this.document.CreateProcessingInstruction(text1, this.a('>')));
            }
            else
            {
                arg.AppendChild(this.document.CreateTextNode("<?"));
            }
        }
        private void a(XmlElement xml, string str1, string str2)
        {
            str1 = str1.ToLower( );
            if (str1.IndexOf(":") == -1)
                xml.SetAttribute(str1, str2);
            else
            {
                outFilter = new char[1] { ':' };
                string[] textArray1 = str1.Split(outFilter, 2);
                string text1 = textArray1[0];
                string text2 = textArray1[1];
                if (string.IsNullOrEmpty(text1))
                    xml.SetAttribute(text2, str2);
                else if (string.IsNullOrEmpty(text2))
                    xml.SetAttribute(text1, str2);
                else
                    xml.SetAttribute(text2, str2);
            }
        }
        private string b( )
        {
            int num1 = this.gNum1;
            if (this.gNum2 >= num1)
            {
                return null;
            }
            char ch1 = this.gStr1[this.gNum2];
            if (((ch1 < 'a') || (ch1 > 'z')) && ((ch1 < 'A') || (ch1 > 'Z')))
            {
                return null;
            }
            int num2 = this.gNum2 + 1;
            while (num2 < num1)
            {
                ch1 = this.gStr1[num2];
                if ((((ch1 < 'a') || (ch1 > 'z')) && ((ch1 < 'A') || (ch1 > 'Z'))) && (((ch1 < '0') || (ch1 > '9')) && (((ch1 != '_') && (ch1 != '-')) && ((ch1 != '.') && (ch1 != ':')))))
                {
                    break;
                }
                num2++;
            }
            return this.gStr1.Substring(this.gNum2, num2 - this.gNum2);
        }
        private string b(char ch)
        {
            if (this.gNum2 >= this.gNum1)
            {
                return "";
            }
            int num1 = this.gStr1.IndexOf(ch, this.gNum2);
            if (num1 == -1)
            {
                return this.f( );
            }
            string text1 = this.gStr1.Substring(this.gNum2, num1 - this.gNum2);
            this.gNum2 = num1 + 1;
            return text1;
        }
        private static string b(string arg)
        {
            if (string.IsNullOrEmpty(arg))
            {
                return null;
            }
            if (string.IsNullOrEmpty(arg))
            {
                return "";
            }
            StringBuilder builder1 = new StringBuilder( );
            int num1 = arg.Length;
            int num2 = 0;
            for (int num3 = arg.IndexOf("&"); num3 != -1; num3 = arg.IndexOf("&", num2))
            {
                int num4 = arg.IndexOfAny(filter, num3 + 1);
                if (num4 == -1)
                {
                    break;
                }
                if (arg[num4] == '&')
                {
                    builder1.Append(arg, num2, num4 - num2);
                    num3 = num4;
                    continue;
                }
                if (num2 != num3)
                {
                    builder1.Append(arg, num2, num3 - num2);
                }
                builder1.Append(HtmlFormatter.praseStr(arg.Substring(num3, (num4 + 1) - num3)));
                num2 = num4 + 1;
                if (num2 >= num1)
                {
                    break;
                }
            }
            if (num2 < num1)
            {
                builder1.Append(arg, num2, num1 - num2);
            }
            return builder1.ToString( );
        }
        private void b(XmlElement xml)
        {
            if (string.Compare(this.gStr1, this.gNum2, "[CDATA[", 0, 7, true) == 0)
            {
                this.gNum2 += 7;
                xml.AppendChild(this.document.CreateCDataSection(this.c("]]>")));
            }
            else if (string.Compare(this.gStr1, this.gNum2, "--", 0, "--".Length, true) == 0)
            {
                this.gNum2 += 2;
                string text1 = this.c("-->");
                if (text1.IndexOf("--") != -1)
                {
                    text1 = text1.Replace("--", "- -");
                }
                if (text1.StartsWith("-") || text1.EndsWith("-"))
                {
                    text1 = " " + text1 + " ";
                }
                xml.AppendChild(this.document.CreateComment(text1));
            }
            else
            {
                string text2 = this.b( );
                if (text2 != null)
                {
                    this.gNum2 += text2.Length;
                    try
                    {
                        xml.AppendChild(this.document.CreateDocumentType(text2, null, null, this.a('>')));
                    }
                    catch
                    {
                    }
                }
                else
                {
                    xml.AppendChild(this.document.CreateTextNode("<!"));
                }
            }
        }
        private string c( )
        {
            int num1 = this.gNum1;
            int num2 = this.gNum2;
            while (num2 < num1)
            {
                char ch1 = this.gStr1[num2];
                if (!char.IsWhiteSpace(ch1))
                {
                    break;
                }
                num2++;
            }
            if (num2 == this.gNum2)
            {
                return null;
            }
            return this.gStr1.Substring(this.gNum2, num2 - this.gNum2);
        }
        private string c(string str)
        {
            if (this.gNum2 >= this.gNum1)
            {
                return "";
            }
            int num1 = this.gStr1.IndexOf(str, this.gNum2);
            if (num1 == -1)
            {
                return this.e( );
            }
            int num2 = this.gStr1.IndexOf("<", this.gNum2, num1 - this.gNum2);
            if (num2 != -1)
            {
                num1 = num2;
            }
            string text1 = this.gStr1.Substring(this.gNum2, num1 - this.gNum2);
            this.gNum2 = num1 + str.Length;
            return text1;
        }
        private void c(XmlElement xml)
        {
            while (this.gNum2 < this.gNum1)
            {
                this.d( );
                string text1 = this.b( );
                if (text1 == null)
                {
                    return;
                }
                this.gNum2 += text1.Length;
                this.d( );
                if (this.gNum2 >= this.gNum1)
                {
                    this.a(xml, text1, text1);
                    return;
                }
                if (this.gStr1[this.gNum2] == '=')
                {
                    string text2;
                    this.gNum2++;
                    this.d( );
                    if (this.gNum2 >= this.gNum1)
                    {
                        this.a(xml, text1, text1);
                        return;
                    }
                    char ch1 = this.gStr1[this.gNum2];
                    if ((ch1 == '"') || (ch1 == '\''))
                    {
                        this.gNum2++;
                        outFilter = new char[2] { '>', ch1 };
                        text2 = this.a(outFilter);
                        if ((this.gNum2 < this.gNum1) && (this.gStr1[this.gNum2] == ch1))
                        {
                            this.gNum2++;
                        }
                    }
                    else if (ch1 == '>')
                    {
                        text2 = null;
                    }
                    else
                    {
                        text2 = this.g( );
                    }
                    if (text2 == null)
                    {
                        text2 = "";
                    }
                    else
                    {
                        text2 = b(text2);
                    }
                    this.a(xml, text1, text2);
                }
            }
        }
        private string d( )
        {
            string text1 = this.c( );
            if (text1 == null)
            {
                return null;
            }
            this.gNum2 += text1.Length;
            return text1;
        }
        private void d(string str)
        {
            if (((this.gNum2 + 1) <= this.gNum1) && ((this.gStr1[this.gNum2] == '<') && (this.gStr1[this.gNum2 + 1] == '/')))
            {
                int num1 = this.gNum2;
                this.gNum2 += 2;
                string text1 = this.b( );
                if (text1 != null)
                {
                    this.gNum2 += text1.Length;
                    this.d( );
                    if ((this.gNum2 < this.gNum1) && (this.gStr1[this.gNum2] == '>'))
                    {
                        this.gNum2++;
                    }
                    if (string.Compare(text1, str, true) == 0)
                    {
                        return;
                    }
                }
                this.gNum2 = num1;
            }
        }
        private void d(XmlElement xml)
        {
            int num1 = this.gNum1;
            if (this.gNum2 < num1)
            {
                char ch1 = this.gStr1[this.gNum2];
                if (ch1 == '!')
                {
                    this.gNum2++;
                    this.b(xml);
                    return;
                }
                if (ch1 == '?')
                {
                    this.gNum2++;
                    this.a(xml);
                    return;
                }
                string text1 = this.b( );
                if (text1 != null)
                {
                    this.gNum2 += text1.Length;
                    XmlElement element1 = this.e(text1);
                    string text2 = element1.LocalName.ToUpper( );
                    xml.AppendChild(element1);
                    this.c(element1);
                    if (this.gNum2 < num1)
                    {
                        ch1 = this.gStr1[this.gNum2];
                        if (ch1 == '/')
                        {
                            this.gNum2++;
                            if ((this.gNum2 < num1) && (this.gStr1[this.gNum2] == '>'))
                            {
                                this.gNum2++;
                            }
                            return;
                        }
                        if (ch1 == '>')
                        {
                            this.gNum2++;
                        }
                        if (this.gNum2 >= num1)
                        {
                            return;
                        }
                        if (",LINK,META,BASE,BGSOUND,BR,HR,INPUT,PARAM,IMG,AREA,".IndexOf("," + text2 + ",") != -1)
                        {
                            return;
                        }
                        if (text2 == "SCRIPT")
                        {
                            string text3;
                            int num2 = this.gStr2.IndexOf("</script", this.gNum2);
                            if (num2 == -1)
                            {
                                text3 = this.f( );
                            }
                            else if (num2 == this.gNum2)
                            {
                                text3 = "";
                            }
                            else
                            {
                                text3 = this.gStr1.Substring(this.gNum2, num2 - this.gNum2);
                                this.gNum2 = num2;
                            }
                            text3 = text3.Replace("/*<![CDATA[*/", "").Replace("/*]]>*/", "").Trim( ).Replace("]]>", "]]&gt;");
                            element1.AppendChild(this.document.CreateTextNode("/*"));
                            element1.AppendChild(this.document.CreateCDataSection("*/\r\n" + text3 + "\r\n/*"));
                            element1.AppendChild(this.document.CreateTextNode("*/"));
                        }
                        else
                        {
                            this.e(element1);
                            if ((",IFRAME,A,P,DIV,OBJECT,ADDRESS,BIG,BLOCKQUOTE,B,CAPTION,CENTER,CITE,CODE,\r\n\t\t\t\t\t\t\t\t\t,DD,DFN,DIR,DL,DT,EM,FONT,FORM,H1,H2,H3,H4,H5,H6,HEAD,HTML,I,LI,MAP,MENU,OL,OPTION,\r\n\t\t\t\t\t\t\t\t\t,PRE,SELECT,SMALL,STRIKE,STRONG,SUB,SUP,TABLE,TD,TEXTAREA,TH,TITLE,TR,TT,U,".IndexOf("," + text2 + ",") != -1) && (element1.ChildNodes.Count == 0))
                            {
                                element1.AppendChild(this.document.CreateTextNode(""));
                            }
                        }
                        this.d(text1);
                    }
                    return;
                }
            }
            xml.AppendChild(this.document.CreateTextNode("<"));
        }
        private string e( )
        {
            if (this.gNum2 >= this.gNum1)
            {
                return "";
            }
            if (this.gStr1[this.gNum2] == '<')
            {
                return "";
            }
            int num1 = this.gStr1.IndexOf('<', this.gNum2);
            if (num1 != -1)
            {
                string text1 = this.gStr1.Substring(this.gNum2, num1 - this.gNum2);
                this.gNum2 = num1;
                return text1;
            }
            string text2 = this.gStr1.Substring(this.gNum2);
            this.gNum2 = this.gNum1;
            return text2;
        }
        private XmlElement e(string str)
        {
            XmlElement element1;
            str = str.ToLower( );
            try
            {
                if (str.IndexOf(":") == -1)
                {
                    return this.document.CreateElement(str);
                }
                char[] chArray1 = new char[1] { ':' };
                string[] textArray1 = str.Split(chArray1, 2);
                string text1 = textArray1[0];
                string text2 = textArray1[1];
                if (text2.IndexOf(":") != -1)
                {
                    text2 = text2.Replace(":", "-");
                }
                if (string.IsNullOrEmpty(text2))
                {
                    this.document.CreateElement(text1);
                }
                if (string.IsNullOrEmpty(text1))
                {
                    return this.document.CreateElement(text2);
                }
                element1 = this.document.CreateElement(text1, text2, "http://unknownprefix/" + text1);
            }
            catch
            {
                element1 = this.document.CreateElement("error");
            }
            return element1;
        }
        private void e(XmlElement xml)
        {
            int num1 = this.gNum1;
            while (this.gNum2 < num1)
            {
                char ch1 = this.gStr1[this.gNum2];
                if (ch1 == '<')
                {
                    if (((this.gNum2 + 1) < num1) && (this.gStr1[this.gNum2 + 1] == '/'))
                    {
                        return;
                    }
                    this.gNum2++;
                    this.d(xml);
                    continue;
                }
                string text1 = this.d( );
                string text2 = this.a( );
                if (text2 == null)
                {
                    if (text1 == null)
                    {
                        this.gNum2++;
                    }
                    continue;
                }
                this.gNum2 += text2.Length;
                xml.AppendChild(this.document.CreateTextNode(text1 + b(text2)));
            }
        }
        private string f( )
        {
            if (this.gNum2 < this.gNum1)
            {
                string text1 = this.gStr1.Substring(this.gNum2);
                this.gNum2 = this.gNum1;
                return text1;
            }
            return "";
        }
        private string g( )
        {
            int num1 = this.gNum2;
            while (this.gNum2 < this.gNum1)
            {
                char ch1 = this.gStr1[this.gNum2];
                if ((ch1 == '>') || char.IsWhiteSpace(ch1))
                {
                    break;
                }
                this.gNum2++;
            }
            if (num1 == this.gNum2)
            {
                return null;
            }
            return this.gStr1.Substring(num1, this.gNum2 - num1);
        }
        private void h( )
        {
            if (((this.gNum2 + 1) <= this.gNum1) && ((this.gStr1[this.gNum2] == '<') && (this.gStr1[this.gNum2 + 1] == '/')))
            {
                this.gNum2 += 2;
                string text1 = this.b( );
                if (text1 != null)
                {
                    this.gNum2 += text1.Length;
                    this.d( );
                    if ((this.gNum2 < this.gNum1) && (this.gStr1[this.gNum2] == '>'))
                    {
                        this.gNum2++;
                    }
                }
            }
        }
        public static void ParseHtml(XmlElement xml, string str)
        {
            if (xml == null)
            {
                throw new ArgumentNullException("element");
            }
            if (str == null)
            {
                throw new ArgumentNullException("xmlstring");
            }
            HtmlFormatter parser1 = new HtmlFormatter( );
            parser1.document = xml.OwnerDocument;
            parser1.gStr1 = str;
            parser1.gStr2 = str.ToLower( );
            parser1.gNum1 = str.Length;
            parser1.gNum2 = 0;
            while (true)
            {
                parser1.e(xml);
                if (parser1.gNum2 >= parser1.gNum1)
                {
                    return;
                }
                parser1.h( );
            }
        }
        private static string praseStr(string str)
        {
            string result;
            object obj1;
            if (hashtable == null)
            {
                Hashtable t = new Hashtable(0x404, 0.5f);
                string[] before = { "&quot;", "&amp;", "&lt;", "&gt;", "&nbsp;", "&iexcl;", "&cent;", "&pound;", "&curren;", "&yen;", "&brvbar;", "&brkbar;", "&sect;", "&uml;", "&die;", "&copy;", "&ordf;", "&laquo;", "&not", "&shy;", "&reg;", "&macr;", "&hibar;", "&deg;", "&plusmn;", "&sup2;", "&sup3;", "&acute;", "&micro;", "&para;", "&middot;", "&cedil;", "&sup1;", "&ordm;", "&raquo;", "&frac14;", "&frac12;", "&frac34;", "&iquest;", "&Agrave;", "&Aacute;", "&Acirc;", "&Atilde;", "&Auml;", "&Aring;", "&AElig;", "&Ccedil;", "&Egrave;", "&Eacute;", "&Ecirc;", "&Euml;", "&Igrave;", "&Iacute;", "&Icirc;", "&Iuml;", "&ETH;", "&Ntilde;", "&Ograve;", "&Oacute;", "&Ocirc;", "&Otilde;", "&Ouml;", "&times;", "&Oslash;", "&Ugrave;", "&Uacute;", "&Ucirc;", "&Uuml;", "&Yacute;", "&THORN;", "&szlig;", "&agrave;", "&aacute;", "&acirc;", "&atilde;", "&auml;", "&aring;", "&aelig;", "&ccedil;", "&egrave;", "&eacute;", "&ecirc;", "&euml;", "&igrave;", "&iacute;", "&icirc;", "&iuml;", "&eth;", "&ntilde;", "&ograve;", "&oacute;", "&ocirc;", "&otilde;", "&ouml;", "&divide;", "&oslash;", "&ugrave;", "&uacute;", "&ucirc;", "&uuml;", "&yacute;", "&thorn;", "&yuml;", "&fnof;", "&Alpha;", "&Beta;", "&Gamma;", "&Delta;", "&Epsilon;", "&Zeta;", "&Eta;", "&Theta;", "&Iota;", "&Kappa;", "&Lambda;", "&Mu;", "&Nu;", "&Xi;", "&Omicron;", "&Pi;", "&Rho;", "&Sigma;", "&Tau;", "&Upsilon;", "&Phi;", "&Chi;", "&Psi;", "&Omega;", "&alpha;", "&beta;", "&gamma;", "&delta;", "&epsilon;", "&zeta;",
                    "&eta;", "&theta;", "&iota;", "&kappa;", "&lambda;", "&mu;", "&nu;", "&xi;", "&omicron;", "&pi;", "&rho;", "&sigmaf;", "&sigma;", "&tau;", "&upsilon;", "&phi;", "&chi;", "&psi;", "&omega;", "&thetasym;", "&upsih;", "&piv;", "&bull;", "&hellip;", "&prime;", "&Prime;", "&oline;", "&frasl;", "&weierp;", "&image;", "&real;", "&trade;", "&alefsym;", "&larr;", "&uarr;", "&rarr;", "&darr;", "&harr;", "&crarr;", "&lArr;", "&uArr;", "&rArr;", "&dArr;", "&hArr;", "&forall;", "&part;", "&exist;", "&empty;", "&nabla;", "&isin;", "&notin;", "&ni;", "&prod;", "&sum;", "&minus;", "&lowast;", "&radic;", "&prop;", "&infin;", "&ang;", "&and;", "&or;", "&cap;", "&cup;", "&int;", "&there4;", "&sim;", "&cong;", "&asymp;", "&ne;", "&equiv;", "&le;", "&ge;", "&sub;", "&sup;", "&nsub;", "&sube;", "&supe;", "&oplus;", "&otimes;", "&perp;", "&sdot;", "&lceil;", "&rceil;", "&lfloor;", "&rfloor;", "&lang;", "&rang;", "&loz;", "&spades;", "&clubs;", "&hearts;", "&diams;", "&quot", "&amp", "&lt", "&gt", "&OElig", "&oelig", "&Scaron", "&scaron", "&Yuml", "&circ", "&tilde", "&ensp", "&emsp", "&thinsp", "&zwnj", "&zwj", "&lrm", "&rlm", "&ndash", "&mdash", "&lsquo", "&rsquo", "&sbquo", "&ldquo", "&rdquo", "&bdquo", "&dagger", "&Dagger", "&permil", "&lsaquo", "&rsaquo", "&#0;", "&#1;", "&#2;", "&#3;", "&#4;", "&#5;", "&#6;", "&#7;", "&#8;", "&#9;", "&#10;", "&#11;", "&#12;", "&#13;", "&#14;", "&#15;", "&#16;", "&#17;", "&#18;", "&#19;", "&#20;", "&#21;", "&#22;", "&#23;",
                    "&#24;", "&#25;", "&#26;", "&#27;", "&#28;", "&#29;", "&#30;", "&#31;", "&#32;", "&#33;", "&#34;", "&#35;", "&#36;", "&#37;", "&#38;", "&#39;", "&#40;", "&#41;", "&#42;", "&#43;", "&#44;", "&#45;", "&#46;", "&#47;", "&#48;", "&#49;", "&#50;", "&#51;", "&#52;", "&#53;", "&#54;", "&#55;", "&#56;", "&#57;", "&#58;", "&#59;", "&#60;", "&#61;", "&#62;", "&#63;", "&#64;", "&#65;", "&#66;", "&#67;", "&#68;", "&#69;", "&#70;", "&#71;", "&#72;", "&#73;", "&#74;", "&#75;", "&#76;", "&#77;", "&#78;","&#79;", "&#80;", "&#81;", "&#82;", "&#83;", "&#84;", "&#85;", "&#86;", "&#87;", "&#88;", "&#89;", "&#90;", "&#91;", "&#92;", "&#93;", "&#94;", "&#95;", "&#96;", "&#97;", "&#98;", "&#99;", "&#100;", "&#101;", "&#102;", "&#103;", "&#104;", "&#105;", "&#106;", "&#107;", "&#108;", "&#109;", "&#110;", "&#111;", "&#112;", "&#113;", "&#114;", "&#115;", "&#116;", "&#117;", "&#118;", "&#119;", "&#120;", "&#121;", "&#122;", "&#123;", "&#124;", "&#125;", "&#126;", "&#127;", "&#128;", "&#129;", "&#130;", "&#131;", "&#132;", "&#133;", "&#134;", "&#135;", "&#136;", "&#137;", "&#138;", "&#139;", "&#140;", "&#141;", "&#142;", "&#143;", "&#144;", "&#145;", "&#146;", "&#147;", "&#148;", "&#149;", "&#150;", "&#151;", "&#152;", "&#153;", "&#154;", "&#155;", "&#156;", "&#157;", "&#158;", "&#159;", "&#160;", "&#161;", "&#162;", "&#163;", "&#164;", "&#165;", "&#166;", "&#167;", "&#168;", "&#169;", "&#170;", "&#171;", "&#172;", "&#173;", "&#174;", "&#175;", "&#176;", "&#177;",
                    "&#178;", "&#179;", "&#180;", "&#181;", "&#182;", "&#183;", "&#184;", "&#185;", "&#186;", "&#187;", "&#188;", "&#189;", "&#190;", "&#191;", "&#192;", "&#193;", "&#194;", "&#195;", "&#196;", "&#197;", "&#198;", "&#199;", "&#200;", "&#201;", "&#202;", "&#203;", "&#204;", "&#205;", "&#206;", "&#207;", "&#208;", "&#209;", "&#210;", "&#211;", "&#212;", "&#213;", "&#214;", "&#215;", "&#216;", "&#217;", "&#218;", "&#219;", "&#220;", "&#221;", "&#222;", "&#223;", "&#224;", "&#225;", "&#226;", "&#227;", "&#228;", "&#229;", "&#230;", "&#231;", "&#232;", "&#233;", "&#234;", "&#235;", "&#236;", "&#237;", "&#238;", "&#239;", "&#240;", "&#241;", "&#242;", "&#243;", "&#244;", "&#245;", "&#246;", "&#247;", "&#248;", "&#249;", "&#250;", "&#251;", "&#252;", "&#253;", "&#254;", "&#255;" };
                int[] after = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 0x10, 0x11, 0x12, 0x13, 20, 0x15, 0x16, 0x17, 0x18, 0x19, 0x1a, 0x1b, 0x1c, 0x1d, 30, 0x1f, 0x20, 0x21, 0x22, 0x23, 0x24, 0x25, 0x26, 0x27, 40, 0x29, 0x2a, 0x2b, 0x2c, 0x2d, 0x2e, 0x2f, 0x30, 0x31, 50, 0x33, 0x34, 0x35, 0x36, 0x37, 0x38, 0x39, 0x3a, 0x3b, 60, 0x3d, 0x3e, 0x3f, 0x40, 0x41, 0x42, 0x43, 0x44, 0x45, 70, 0x47, 0x48, 0x49, 0x4a, 0x4b, 0x4c, 0x4d, 0x4e, 0x4f, 80, 0x51, 0x52, 0x53, 0x54, 0x55, 0x56, 0x57, 0x58, 0x59, 90, 0x5b, 0x5c, 0x5d, 0x5e, 0x5f, 0x60, 0x61, 0x62, 0x63, 100, 0x65, 0x66, 0x67, 0x68, 0x69, 0x6a, 0x6b, 0x6c, 0x6d, 110, 0x6f, 0x70, 0x71, 0x72, 0x73, 0x74, 0x75, 0x76, 0x77, 120, 0x79, 0x7a, 0x7b, 0x7c, 0x7d, 0x7e, 0x7f, 0x80, 0x81, 130, 0x83, 0x84, 0x85, 0x86, 0x87, 0x88, 0x89, 0x8a, 0x8b, 140, 0x8d, 0x8e, 0x8f, 0x90, 0x91, 0x92, 0x93, 0x94, 0x95, 150, 0x97, 0x98, 0x99, 0x9a, 0x9b, 0x9c, 0x9d, 0x9e, 0x9f, 160, 0xa1, 0xa2, 0xa3, 0xa4, 0xa5, 0xa6, 0xa7, 0xa8, 0xa9, 170, 0xab, 0xac, 0xad, 0xae, 0xaf, 0xb0, 0xb1, 0xb2, 0xb3, 180, 0xb5, 0xb6, 0xb7, 0xb8, 0xb9, 0xba, 0xbb, 0xbc, 0xbd, 190, 0xbf, 0xc0, 0xc1, 0xc2, 0xc3, 0xc4, 0xc5, 0xc6, 0xc7, 200, 0xc9, 0xca, 0xcb, 0xcc, 0xcd, 0xce, 0xcf, 0xd0, 0xd1, 210, 0xd3, 0xd4, 0xd5, 0xd6, 0xd7, 0xd8, 0xd9, 0xda, 0xdb, 220, 0xdd, 0xde, 0xdf, 0xe0, 0xe1, 0xe2, 0xe3, 0xe4, 0xe5, 230, 0xe7, 0xe8, 0xe9, 0xea, 0xeb, 0xec, 0xed, 0xee, 0xef, 240, 0xf1, 0xf2, 0xf3, 0xf4, 0xf5, 0xf6, 0xf7, 0xf8, 0xf9, 250, 0xfb, 0xfc, 0xfd,
                    0xfe, 0xff, 0x100, 0x101, 0x102, 0x103, 260, 0x105, 0x106, 0x107, 0x108, 0x109, 0x10a, 0x10b, 0x10c, 0x10d, 270, 0x10f, 0x110, 0x111, 0x112, 0x113, 0x114, 0x115, 0x116, 0x117, 280, 0x119, 0x11a, 0x11b, 0x11c, 0x11d, 0x11e, 0x11f, 0x120, 0x121, 290,0x123, 0x124, 0x125, 0x126, 0x127, 0x128, 0x129, 0x12a, 0x12b, 300, 0x12d, 0x12e, 0x12f, 0x130, 0x131, 0x132, 0x133, 0x134, 0x135, 310, 0x137, 0x138, 0x139, 0x13a, 0x13b, 0x13c, 0x13d, 0x13e, 0x13f, 320, 0x141, 0x142, 0x143, 0x144, 0x145, 0x146, 0x147, 0x148, 0x149, 330, 0x14b, 0x14c, 0x14d, 0x14e, 0x14f, 0x150, 0x151, 0x152, 0x153, 340, 0x155, 0x156, 0x157, 0x158, 0x159, 0x15a, 0x15b, 0x15c, 0x15d, 350, 0x15f, 0x160, 0x161, 0x162, 0x163, 0x164, 0x165, 0x166, 0x167, 360, 0x169, 0x16a, 0x16b, 0x16c, 0x16d, 0x16e, 0x16f, 0x170, 0x171, 370, 0x173, 0x174, 0x175, 0x176, 0x177, 0x178, 0x179, 0x17a, 0x17b, 380, 0x17d, 0x17e, 0x17f, 0x180, 0x181, 0x182, 0x183, 0x184, 0x185, 390, 0x187, 0x188, 0x189, 0x18a, 0x18b, 0x18c, 0x18d, 0x18e, 0x18f, 400, 0x191, 0x192, 0x193, 0x194, 0x195, 0x196, 0x197, 0x198, 0x199, 410, 0x19b, 0x19c, 0x19d, 0x19e, 0x19f, 0x1a0, 0x1a1, 0x1a2, 0x1a3, 420, 0x1a5, 0x1a6, 0x1a7, 0x1a8, 0x1a9, 0x1aa, 0x1ab, 0x1ac, 0x1ad, 430, 0x1af, 0x1b0, 0x1b1, 0x1b2, 0x1b3, 0x1b4, 0x1b5, 0x1b6, 0x1b7, 440, 0x1b9, 0x1ba, 0x1bb, 0x1bc, 0x1bd, 0x1be, 0x1bf, 0x1c0, 0x1c1, 450, 0x1c3, 0x1c4, 0x1c5, 0x1c6, 0x1c7, 0x1c8, 0x1c9, 0x1ca, 0x1cb, 460, 0x1cd, 0x1ce, 0x1cf, 0x1d0, 0x1d1, 0x1d2, 0x1d3, 0x1d4, 0x1d5,
                    470, 0x1d7, 0x1d8, 0x1d9, 0x1da, 0x1db, 0x1dc, 0x1dd, 0x1de, 0x1df, 480, 0x1e1, 0x1e2, 0x1e3, 0x1e4, 0x1e5, 0x1e6, 0x1e7, 0x1e8, 0x1e9, 490, 0x1eb, 0x1ec, 0x1ed, 0x1ee, 0x1ef, 0x1f0, 0x1f1, 0x1f2, 0x1f3, 500, 0x1f5, 0x1f6, 0x1f7, 0x1f8, 0x1f9, 0x1fa, 0x1fb, 0x1fc, 0x1fd, 510, 0x1ff, 0x200, 0x201 };
                for (int i = 0; i < before.Length; i++)
                {
                    t.Add(before[i], after[i]);
                }
                hashtable = t;
            }
            if (((obj1 = str.ToLower( )) != null) && ((obj1 = hashtable[obj1]) != null))
            {
                switch ((int) obj1)
                {
                    case 0:
                        return "\"";
                    case 1:
                        return "&";
                    case 2:
                        return "<";
                    case 3:
                        return ">";
                    case 4:
                        return "\x00a0";
                    case 5:
                        return "\x00a1";
                    case 6:
                        return "\x00a2";
                    case 7:
                        return "\x00a3";
                    case 8:
                        return "\x00a4";
                    case 9:
                        return "\x00a5";
                    case 10:
                        return "\x00a6";
                    case 11:
                        return "\x00a6";
                    case 12:
                        return "\x00a7";
                    case 13:
                        return "\x00a8";
                    case 14:
                        return "\x00a8";
                    case 15:
                        return "\x00a9";
                    case 0x10:
                        return "\x00aa";
                    case 0x11:
                        return "\x00ab";
                    case 0x12:
                        return "\x00ac";
                    case 0x13:
                        return "\x00ad";
                    case 20:
                        return "\x00ae";
                    case 0x15:
                        return "\x00af";
                    case 0x16:
                        return "\x00af";
                    case 0x17:
                        return "\x00b0";
                    case 0x18:
                        return "\x00b1";
                    case 0x19:
                        return "\x00b2";
                    case 0x1a:
                        return "\x00b3";
                    case 0x1b:
                        return "\x00b4";
                    case 0x1c:
                        return "\x00b5";
                    case 0x1d:
                        return "\x00b6";
                    case 30:
                        return "\x00b7";
                    case 0x1f:
                        return "\x00b8";
                    case 0x20:
                        return "\x00b9";
                    case 0x21:
                        return "\x00ba";
                    case 0x22:
                        return "\x00bb";
                    case 0x23:
                        return "\x00bc";
                    case 0x24:
                        return "\x00bd";
                    case 0x25:
                        return "\x00be";
                    case 0x26:
                        return "\x00bf";
                    case 0x27:
                        return "\x00c0";
                    case 40:
                        return "\x00c1";
                    case 0x29:
                        return "\x00c2";
                    case 0x2a:
                        return "\x00c3";
                    case 0x2b:
                        return "\x00c4";
                    case 0x2c:
                        return "\x00c5";
                    case 0x2d:
                        return "\x00c6";
                    case 0x2e:
                        return "\x00c7";
                    case 0x2f:
                        return "\x00c8";
                    case 0x30:
                        return "\x00c9";
                    case 0x31:
                        return "\x00ca";
                    case 50:
                        return "\x00cb";
                    case 0x33:
                        return "\x00cc";
                    case 0x34:
                        return "\x00cd";
                    case 0x35:
                        return "\x00ce";
                    case 0x36:
                        return "\x00cf";
                    case 0x37:
                        return "\x00d0";
                    case 0x38:
                        return "\x00d1";
                    case 0x39:
                        return "\x00d2";
                    case 0x3a:
                        return "\x00d3";
                    case 0x3b:
                        return "\x00d4";
                    case 60:
                        return "\x00d5";
                    case 0x3d:
                        return "\x00d6";
                    case 0x3e:
                        return "\x00d7";
                    case 0x3f:
                        return "\x00d8";
                    case 0x40:
                        return "\x00d9";
                    case 0x41:
                        return "\x00da";
                    case 0x42:
                        return "\x00db";
                    case 0x43:
                        return "\x00dc";
                    case 0x44:
                        return "\x00dd";
                    case 0x45:
                        return "\x00de";
                    case 70:
                        return "\x00df";
                    case 0x47:
                        return "\x00e0";
                    case 0x48:
                        return "\x00e1";
                    case 0x49:
                        return "\x00e2";
                    case 0x4a:
                        return "\x00e3";
                    case 0x4b:
                        return "\x00e4";
                    case 0x4c:
                        return "\x00e5";
                    case 0x4d:
                        return "\x00e6";
                    case 0x4e:
                        return "\x00e7";
                    case 0x4f:
                        return "\x00e8";
                    case 80:
                        return "\x00e9";
                    case 0x51:
                        return "\x00ea";
                    case 0x52:
                        return "\x00eb";
                    case 0x53:
                        return "\x00ec";
                    case 0x54:
                        return "\x00ed";
                    case 0x55:
                        return "\x00ee";
                    case 0x56:
                        return "\x00ef";
                    case 0x57:
                        return "\x00f0";
                    case 0x58:
                        return "\x00f1";
                    case 0x59:
                        return "\x00f2";
                    case 90:
                        return "\x00f3";
                    case 0x5b:
                        return "\x00f4";
                    case 0x5c:
                        return "\x00f5";
                    case 0x5d:
                        return "\x00f6";
                    case 0x5e:
                        return "\x00f7";
                    case 0x5f:
                        return "\x00f8";
                    case 0x60:
                        return "\x00f9";
                    case 0x61:
                        return "\x00fa";
                    case 0x62:
                        return "\x00fb";
                    case 0x63:
                        return "\x00fc";
                    case 100:
                        return "\x00fd";
                    case 0x65:
                        return "\x00fe";
                    case 0x66:
                        return "\x00ff";
                    case 0x67:
                        return "\u0192";
                    case 0x68:
                        return "\u0391";
                    case 0x69:
                        return "\u0392";
                    case 0x6a:
                        return "\u0393";
                    case 0x6b:
                        return "\u0394";
                    case 0x6c:
                        return "\u0395";
                    case 0x6d:
                        return "\u0396";
                    case 110:
                        return "\u0397";
                    case 0x6f:
                        return "\u0398";
                    case 0x70:
                        return "\u0399";
                    case 0x71:
                        return "\u039a";
                    case 0x72:
                        return "\u039b";
                    case 0x73:
                        return "\u039c";
                    case 0x74:
                        return "\u039d";
                    case 0x75:
                        return "\u039e";
                    case 0x76:
                        return "\u039f";
                    case 0x77:
                        return "\u03a0";
                    case 120:
                        return "\u03a1";
                    case 0x79:
                        return "\u03a3";
                    case 0x7a:
                        return "\u03a4";
                    case 0x7b:
                        return "\u03a5";
                    case 0x7c:
                        return "\u03a6";
                    case 0x7d:
                        return "\u03a7";
                    case 0x7e:
                        return "\u03a8";
                    case 0x7f:
                        return "\u03a9";
                    case 0x80:
                        return "\u03b1";
                    case 0x81:
                        return "\u03b2";
                    case 130:
                        return "\u03b3";
                    case 0x83:
                        return "\u03b4";
                    case 0x84:
                        return "\u03b5";
                    case 0x85:
                        return "\u03b6";
                    case 0x86:
                        return "\u03b7";
                    case 0x87:
                        return "\u03b8";
                    case 0x88:
                        return "\u03b9";
                    case 0x89:
                        return "\u03ba";
                    case 0x8a:
                        return "\u03bb";
                    case 0x8b:
                        return "\u03bc";
                    case 140:
                        return "\u03bd";
                    case 0x8d:
                        return "\u03be";
                    case 0x8e:
                        return "\u03bf";
                    case 0x8f:
                        return "\u03c0";
                    case 0x90:
                        return "\u03c1";
                    case 0x91:
                        return "\u03c2";
                    case 0x92:
                        return "\u03c3";
                    case 0x93:
                        return "\u03c4";
                    case 0x94:
                        return "\u03c5";
                    case 0x95:
                        return "\u03c6";
                    case 150:
                        return "\u03c7";
                    case 0x97:
                        return "\u03c8";
                    case 0x98:
                        return "\u03c9";
                    case 0x99:
                        return "\u03d1";
                    case 0x9a:
                        return "\u03d2";
                    case 0x9b:
                        return "\u03d6";
                    case 0x9c:
                        return "\u2022";
                    case 0x9d:
                        return "\u2026";
                    case 0x9e:
                        return "\u2032";
                    case 0x9f:
                        return "\u2033";
                    case 160:
                        return "\u203e";
                    case 0xa1:
                        return "\u2044";
                    case 0xa2:
                        return "\u2118";
                    case 0xa3:
                        return "\u2111";
                    case 0xa4:
                        return "\u211c";
                    case 0xa5:
                        return "\u2122";
                    case 0xa6:
                        return "\u2135";
                    case 0xa7:
                        return "\u2190";
                    case 0xa8:
                        return "\u2191";
                    case 0xa9:
                        return "\u2192";
                    case 170:
                        return "\u2193";
                    case 0xab:
                        return "\u2194";
                    case 0xac:
                        return "\u21b5";
                    case 0xad:
                        return "\u21d0";
                    case 0xae:
                        return "\u21d1";
                    case 0xaf:
                        return "\u21d2";
                    case 0xb0:
                        return "\u21d3";
                    case 0xb1:
                        return "\u21d4";
                    case 0xb2:
                        return "\u2200";
                    case 0xb3:
                        return "\u2202";
                    case 180:
                        return "\u2203";
                    case 0xb5:
                        return "\u2205";
                    case 0xb6:
                        return "\u2207";
                    case 0xb7:
                        return "\u2208";
                    case 0xb8:
                        return "\u2209";
                    case 0xb9:
                        return "\u220b";
                    case 0xba:
                        return "\u220f";
                    case 0xbb:
                        return "\u2212";
                    case 0xbc:
                        return "\u2212";
                    case 0xbd:
                        return "\u2217";
                    case 190:
                        return "\u221a";
                    case 0xbf:
                        return "\u221d";
                    case 0xc0:
                        return "\u221e";
                    case 0xc1:
                        return "\u2220";
                    case 0xc2:
                        return "\u22a5";
                    case 0xc3:
                        return "\u22a6";
                    case 0xc4:
                        return "\u2229";
                    case 0xc5:
                        return "\u222a";
                    case 0xc6:
                        return "\u222b";
                    case 0xc7:
                        return "\u2234";
                    case 200:
                        return "\u223c";
                    case 0xc9:
                        return "\u2245";
                    case 0xca:
                        return "\u2245";
                    case 0xcb:
                        return "\u2260";
                    case 0xcc:
                        return "\u2261";
                    case 0xcd:
                        return "\u2264";
                    case 0xce:
                        return "\u2265";
                    case 0xcf:
                        return "\u2282";
                    case 0xd0:
                        return "\u2283";
                    case 0xd1:
                        return "\u2284";
                    case 210:
                        return "\u2286";
                    case 0xd3:
                        return "\u2287";
                    case 0xd4:
                        return "\u2295";
                    case 0xd5:
                        return "\u2297";
                    case 0xd6:
                        return "\u22a5";
                    case 0xd7:
                        return "\u22c5";
                    case 0xd8:
                        return "\u2308";
                    case 0xd9:
                        return "\u2309";
                    case 0xda:
                        return "\u230a";
                    case 0xdb:
                        return "\u230b";
                    case 220:
                        return "\u2329";
                    case 0xdd:
                        return "\u232a";
                    case 0xde:
                        return "\u25ca";
                    case 0xdf:
                        return "\u2660";
                    case 0xe0:
                        return "\u2663";
                    case 0xe1:
                        return "\u2665";
                    case 0xe2:
                        return "\u2666";
                    case 0xe3:
                        return "\"";
                    case 0xe4:
                        return "&";
                    case 0xe5:
                        return "<";
                    case 230:
                        return ">";
                    case 0xe7:
                        return "\u0152";
                    case 0xe8:
                        return "\u0153";
                    case 0xe9:
                        return "\u0160";
                    case 0xea:
                        return "\u0161";
                    case 0xeb:
                        return "\u0178";
                    case 0xec:
                        return "\u02c6";
                    case 0xed:
                        return "\u02dc";
                    case 0xee:
                        return "\u2002";
                    case 0xef:
                        return "\u2003";
                    case 240:
                        return "\u2009";
                    case 0xf1:
                        return "\u200c";
                    case 0xf2:
                        return "\u200d";
                    case 0xf3:
                        return "\u200e";
                    case 0xf4:
                        return "\u200f";
                    case 0xf5:
                        return "\u2013";
                    case 0xf6:
                        return "\x0097";
                    case 0xf7:
                        return "\u2018";
                    case 0xf8:
                        return "\u2019";
                    case 0xf9:
                        return "\u201a";
                    case 250:
                        return "\u201c";
                    case 0xfb:
                        return "\u201d";
                    case 0xfc:
                        return "\u201e";
                    case 0xfd:
                        return "\u2020";
                    case 0xfe:
                        return "\u2021";
                    case 0xff:
                        return "\u2030";
                    case 0x100:
                        return "\u2039";
                    case 0x101:
                        return "\u203a";
                    case 0x102:
                        return "\0";
                    case 0x103:
                        return "\x0001";
                    case 260:
                        return "\x0002";
                    case 0x105:
                        return "\x0003";
                    case 0x106:
                        return "\x0004";
                    case 0x107:
                        return "\x0005";
                    case 0x108:
                        return "\x0006";
                    case 0x109:
                        return "\a";
                    case 0x10a:
                        return "\b";
                    case 0x10b:
                        return "\t";
                    case 0x10c:
                        return "\n";
                    case 0x10d:
                        return "\v";
                    case 270:
                        return "\f";
                    case 0x10f:
                        return "\r";
                    case 0x110:
                        return "\x000e";
                    case 0x111:
                        return "\x000f";
                    case 0x112:
                        return "\x0010";
                    case 0x113:
                        return "\x0011";
                    case 0x114:
                        return "\x0012";
                    case 0x115:
                        return "\x0013";
                    case 0x116:
                        return "\x0014";
                    case 0x117:
                        return "\x0015";
                    case 280:
                        return "\x0016";
                    case 0x119:
                        return "\x0017";
                    case 0x11a:
                        return "\x0018";
                    case 0x11b:
                        return "\x0019";
                    case 0x11c:
                        return "\x001a";
                    case 0x11d:
                        return "\x001b";
                    case 0x11e:
                        return "\x001c";
                    case 0x11f:
                        return "\x001d";
                    case 0x120:
                        return "\x001e";
                    case 0x121:
                        return "\x001f";
                    case 290:
                        return " ";
                    case 0x123:
                        return "!";
                    case 0x124:
                        return "\"";
                    case 0x125:
                        return "#";
                    case 0x126:
                        return "$";
                    case 0x127:
                        return "%";
                    case 0x128:
                        return "&";
                    case 0x129:
                        return "\"";
                    case 0x12a:
                        return "(";
                    case 0x12b:
                        return ")";
                    case 300:
                        return "*";
                    case 0x12d:
                        return "+";
                    case 0x12e:
                        return ",";
                    case 0x12f:
                        return "-";
                    case 0x130:
                        return ".";
                    case 0x131:
                        return "/";
                    case 0x132:
                        return "0";
                    case 0x133:
                        return "1";
                    case 0x134:
                        return "2";
                    case 0x135:
                        return "3";
                    case 310:
                        return "4";
                    case 0x137:
                        return "5";
                    case 0x138:
                        return "6";
                    case 0x139:
                        return "7";
                    case 0x13a:
                        return "8";
                    case 0x13b:
                        return "9";
                    case 0x13c:
                        return ":";
                    case 0x13d:
                        return ";";
                    case 0x13e:
                        return "<";
                    case 0x13f:
                        return "=";
                    case 320:
                        return ">";
                    case 0x141:
                        return "?";
                    case 0x142:
                        return "@";
                    case 0x143:
                        return "A";
                    case 0x144:
                        return "B";
                    case 0x145:
                        return "C";
                    case 0x146:
                        return "D";
                    case 0x147:
                        return "E";
                    case 0x148:
                        return "F";
                    case 0x149:
                        return "G";
                    case 330:
                        return "H";
                    case 0x14b:
                        return "I";
                    case 0x14c:
                        return "J";
                    case 0x14d:
                        return "K";
                    case 0x14e:
                        return "L";
                    case 0x14f:
                        return "M";
                    case 0x150:
                        return "N";
                    case 0x151:
                        return "O";
                    case 0x152:
                        return "P";
                    case 0x153:
                        return "Q";
                    case 340:
                        return "R";
                    case 0x155:
                        return "S";
                    case 0x156:
                        return "T";
                    case 0x157:
                        return "U";
                    case 0x158:
                        return "V";
                    case 0x159:
                        return "W";
                    case 0x15a:
                        return "X";
                    case 0x15b:
                        return "Y";
                    case 0x15c:
                        return "Z";
                    case 0x15d:
                        return "[";
                    case 350:
                        return "\\";
                    case 0x15f:
                        return "]";
                    case 0x160:
                        return "^";
                    case 0x161:
                        return "_";
                    case 0x162:
                        return "`";
                    case 0x163:
                        return "a";
                    case 0x164:
                        return "b";
                    case 0x165:
                        return "c";
                    case 0x166:
                        return "d";
                    case 0x167:
                        return "e";
                    case 360:
                        return "f";
                    case 0x169:
                        return "g";
                    case 0x16a:
                        return "h";
                    case 0x16b:
                        return "i";
                    case 0x16c:
                        return "j";
                    case 0x16d:
                        return "k";
                    case 0x16e:
                        return "l";
                    case 0x16f:
                        return "m";
                    case 0x170:
                        return "n";
                    case 0x171:
                        return "o";
                    case 370:
                        return "p";
                    case 0x173:
                        return "q";
                    case 0x174:
                        return "r";
                    case 0x175:
                        return "s";
                    case 0x176:
                        return "t";
                    case 0x177:
                        return "u";
                    case 0x178:
                        return "v";
                    case 0x179:
                        return "w";
                    case 0x17a:
                        return "x";
                    case 0x17b:
                        return "y";
                    case 380:
                        return "z";
                    case 0x17d:
                        return "{";
                    case 0x17e:
                        return "|";
                    case 0x17f:
                        return "}";
                    case 0x180:
                        return "~";
                    case 0x181:
                        return "\x007f";
                    case 0x182:
                        return "\x0080";
                    case 0x183:
                        return "\x0081";
                    case 0x184:
                        return "\x0082";
                    case 0x185:
                        return "\x0083";
                    case 390:
                        return "\x0084";
                    case 0x187:
                        return "\x0085";
                    case 0x188:
                        return "\x0086";
                    case 0x189:
                        return "\x0087";
                    case 0x18a:
                        return "\x0088";
                    case 0x18b:
                        return "\x0089";
                    case 0x18c:
                        return "\x008a";
                    case 0x18d:
                        return "\x008b";
                    case 0x18e:
                        return "\x008c";
                    case 0x18f:
                        return "\x008d";
                    case 400:
                        return "\x008e";
                    case 0x191:
                        return "\x008f";
                    case 0x192:
                        return "\x0090";
                    case 0x193:
                        return "\x0091";
                    case 0x194:
                        return "\x0092";
                    case 0x195:
                        return "\x0093";
                    case 0x196:
                        return "\x0094";
                    case 0x197:
                        return "\x0095";
                    case 0x198:
                        return "\x0096";
                    case 0x199:
                        return "\x0097";
                    case 410:
                        return "\x0098";
                    case 0x19b:
                        return "\x0099";
                    case 0x19c:
                        return "\x009a";
                    case 0x19d:
                        return "\x009b";
                    case 0x19e:
                        return "\x009c";
                    case 0x19f:
                        return "\x009d";
                    case 0x1a0:
                        return "\x009e";
                    case 0x1a1:
                        return "\x009f";
                    case 0x1a2:
                        return "\x00a0";
                    case 0x1a3:
                        return "\x00a1";
                    case 420:
                        return "\x00a2";
                    case 0x1a5:
                        return "\x00a3";
                    case 0x1a6:
                        return "\x00a4";
                    case 0x1a7:
                        return "\x00a5";
                    case 0x1a8:
                        return "\x00a6";
                    case 0x1a9:
                        return "\x00a7";
                    case 0x1aa:
                        return "\x00a8";
                    case 0x1ab:
                        return "\x00a9";
                    case 0x1ac:
                        return "\x00aa";
                    case 0x1ad:
                        return "\x00ab";
                    case 430:
                        return "\x00ac";
                    case 0x1af:
                        return "\x00ad";
                    case 0x1b0:
                        return "\x00ae";
                    case 0x1b1:
                        return "\x00af";
                    case 0x1b2:
                        return "\x00b0";
                    case 0x1b3:
                        return "\x00b1";
                    case 0x1b4:
                        return "\x00b2";
                    case 0x1b5:
                        return "\x00b3";
                    case 0x1b6:
                        return "\x00b4";
                    case 0x1b7:
                        return "\x00b5";
                    case 440:
                        return "\x00b6";
                    case 0x1b9:
                        return "\x00b7";
                    case 0x1ba:
                        return "\x00b8";
                    case 0x1bb:
                        return "\x00b9";
                    case 0x1bc:
                        return "\x00ba";
                    case 0x1bd:
                        return "\x00bb";
                    case 0x1be:
                        return "\x00bc";
                    case 0x1bf:
                        return "\x00bd";
                    case 0x1c0:
                        return "\x00be";
                    case 0x1c1:
                        return "\x00bf";
                    case 450:
                        return "\x00c0";
                    case 0x1c3:
                        return "\x00c1";
                    case 0x1c4:
                        return "\x00c2";
                    case 0x1c5:
                        return "\x00c3";
                    case 0x1c6:
                        return "\x00c4";
                    case 0x1c7:
                        return "\x00c5";
                    case 0x1c8:
                        return "\x00c6";
                    case 0x1c9:
                        return "\x00c7";
                    case 0x1ca:
                        return "\x00c8";
                    case 0x1cb:
                        return "\x00c9";
                    case 460:
                        return "\x00ca";
                    case 0x1cd:
                        return "\x00cb";
                    case 0x1ce:
                        return "\x00cc";
                    case 0x1cf:
                        return "\x00cd";
                    case 0x1d0:
                        return "\x00ce";
                    case 0x1d1:
                        return "\x00cf";
                    case 0x1d2:
                        return "\x00d0";
                    case 0x1d3:
                        return "\x00d1";
                    case 0x1d4:
                        return "\x00d2";
                    case 0x1d5:
                        return "\x00d3";
                    case 470:
                        return "\x00d4";
                    case 0x1d7:
                        return "\x00d5";
                    case 0x1d8:
                        return "\x00d6";
                    case 0x1d9:
                        return "\x00d7";
                    case 0x1da:
                        return "\x00d8";
                    case 0x1db:
                        return "\x00d9";
                    case 0x1dc:
                        return "\x00da";
                    case 0x1dd:
                        return "\x00db";
                    case 0x1de:
                        return "\x00dc";
                    case 0x1df:
                        return "\x00dd";
                    case 480:
                        return "\x00de";
                    case 0x1e1:
                        return "\x00df";
                    case 0x1e2:
                        return "\x00e0";
                    case 0x1e3:
                        return "\x00e1";
                    case 0x1e4:
                        return "\x00e2";
                    case 0x1e5:
                        return "\x00e3";
                    case 0x1e6:
                        return "\x00e4";
                    case 0x1e7:
                        return "\x00e5";
                    case 0x1e8:
                        return "\x00e6";
                    case 0x1e9:
                        return "\x00e7";
                    case 490:
                        return "\x00e8";
                    case 0x1eb:
                        return "\x00e9";
                    case 0x1ec:
                        return "\x00ea";
                    case 0x1ed:
                        return "\x00eb";
                    case 0x1ee:
                        return "\x00ec";
                    case 0x1ef:
                        return "\x00ed";
                    case 0x1f0:
                        return "\x00ee";
                    case 0x1f1:
                        return "\x00ef";
                    case 0x1f2:
                        return "\x00f0";
                    case 0x1f3:
                        return "\x00f1";
                    case 500:
                        return "\x00f2";
                    case 0x1f5:
                        return "\x00f3";
                    case 0x1f6:
                        return "\x00f4";
                    case 0x1f7:
                        return "\x00f5";
                    case 0x1f8:
                        return "\x00f6";
                    case 0x1f9:
                        return "\x00f7";
                    case 0x1fa:
                        return "\x00f8";
                    case 0x1fb:
                        return "\x00f9";
                    case 0x1fc:
                        return "\x00fa";
                    case 0x1fd:
                        return "\x00fb";
                    case 510:
                        return "\x00fc";
                    case 0x1ff:
                        return "\x00fd";
                    case 0x200:
                        return "\x00fe";
                    case 0x201:
                        return "\x00ff";
                }
            }
            if (str[1] != '#')
                return str;
            if (str.Length == 3)
                return "&#;";
            try
            {
                char ch1 = (char) ((ushort) short.Parse(str.Substring(2, str.Length - 3)));
                result = ch1.ToString( );
            }
            catch
            {
                result = str;
            }
            return result;
        }
    }
}