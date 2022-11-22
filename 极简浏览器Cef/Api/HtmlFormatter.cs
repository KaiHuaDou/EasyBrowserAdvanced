using System;
using System.Collections;
using System.IO;
using System.Text;
using System.Xml;
public static class HtmlFormatter
{
    public static string ConvertToXml(string str, bool bLineAndIndent)
    {
        XmlDocument document1 = ConvertToXmlDocument(str);

        if (bLineAndIndent)
        {

            StringBuilder builder1 = new StringBuilder();
            XmlTextWriter writer1 = new XmlTextWriter(new StringWriter(builder1));
            writer1.IndentChar = ' ';
            writer1.Indentation = 4;
            writer1.Formatting = Formatting.Indented;
            document1.DocumentElement.WriteContentTo(writer1);
            return builder1.ToString();
        }
        return document1.DocumentElement.InnerXml;
    }
    public static XmlDocument ConvertToXmlDocument(string str)
    {
        XmlDocument document1 = new XmlDocument();
        document1.LoadXml("<xml/>");
        SafeHtmlParser.ParseHtml(document1.DocumentElement, str);
        return document1;
    }
}
 public class SafeHtmlParser
{
    internal class inside
    {
        internal static Hashtable r;
    }

    // Methods
    static SafeHtmlParser()
    {
        char[] chArray1 = new char[2] { '&', ';' };
        fh = chArray1;
    }
    private SafeHtmlParser()
    {
    }
    private string a()
    {
        StringBuilder builder1 = new StringBuilder();
        int num1 = this.fg;
        builder1.Append(this.e());
        this.fg = num1;
        if (builder1.Length == 0)
        {
            return null;
        }
        return builder1.ToString();
    }
    private string a(char A_0)
    {
        if (this.fg >= this.ff)
        {
            return "";
        }
        int num1 = this.fd.IndexOf(A_0, this.fg);
        if (num1 == -1)
        {
            return this.e();
        }
        int num2 = this.fd.IndexOf('<', this.fg, (int)(num1 - this.fg));
        if (num2 != -1)
        {
            num1 = num2;
        }
        string text1 = this.fd.Substring(this.fg, num1 - this.fg);
        this.fg = num1 + 1;
        return text1;
    }
    private string a(out string A_0)
    {
        int num1 = this.fg;
        A_0 = this.d();
        string text1 = this.b();
        this.fg = num1;
        return text1;
    }
    private static string a(string A_0)
    {
        string text1;
        object obj1;
        if (inside.r == null)
        {
            Hashtable hashtable1 = new Hashtable(0x404, 0.5f);
            hashtable1.Add("&quot;", 0);
            hashtable1.Add("&amp;", 1);
            hashtable1.Add("&lt;", 2);
            hashtable1.Add("&gt;", 3);
            hashtable1.Add("&nbsp;", 4);
            hashtable1.Add("&iexcl;", 5);
            hashtable1.Add("&cent;", 6);
            hashtable1.Add("&pound;", 7);
            hashtable1.Add("&curren;", 8);
            hashtable1.Add("&yen;", 9);
            hashtable1.Add("&brvbar;", 10);
            hashtable1.Add("&brkbar;", 11);
            hashtable1.Add("&sect;", 12);
            hashtable1.Add("&uml;", 13);
            hashtable1.Add("&die;", 14);
            hashtable1.Add("&copy;", 15);
            hashtable1.Add("&ordf;", 0x10);
            hashtable1.Add("&laquo;", 0x11);
            hashtable1.Add("&not", 0x12);
            hashtable1.Add("&shy;", 0x13);
            hashtable1.Add("&reg;", 20);
            hashtable1.Add("&macr;", 0x15);
            hashtable1.Add("&hibar;", 0x16);
            hashtable1.Add("&deg;", 0x17);
            hashtable1.Add("&plusmn;", 0x18);
            hashtable1.Add("&sup2;", 0x19);
            hashtable1.Add("&sup3;", 0x1a);
            hashtable1.Add("&acute;", 0x1b);
            hashtable1.Add("&micro;", 0x1c);
            hashtable1.Add("&para;", 0x1d);
            hashtable1.Add("&middot;", 30);
            hashtable1.Add("&cedil;", 0x1f);
            hashtable1.Add("&sup1;", 0x20);
            hashtable1.Add("&ordm;", 0x21);
            hashtable1.Add("&raquo;", 0x22);
            hashtable1.Add("&frac14;", 0x23);
            hashtable1.Add("&frac12;", 0x24);
            hashtable1.Add("&frac34;", 0x25);
            hashtable1.Add("&iquest;", 0x26);
            hashtable1.Add("&Agrave;", 0x27);
            hashtable1.Add("&Aacute;", 40);
            hashtable1.Add("&Acirc;", 0x29);
            hashtable1.Add("&Atilde;", 0x2a);
            hashtable1.Add("&Auml;", 0x2b);
            hashtable1.Add("&Aring;", 0x2c);
            hashtable1.Add("&AElig;", 0x2d);
            hashtable1.Add("&Ccedil;", 0x2e);
            hashtable1.Add("&Egrave;", 0x2f);
            hashtable1.Add("&Eacute;", 0x30);
            hashtable1.Add("&Ecirc;", 0x31);
            hashtable1.Add("&Euml;", 50);
            hashtable1.Add("&Igrave;", 0x33);
            hashtable1.Add("&Iacute;", 0x34);
            hashtable1.Add("&Icirc;", 0x35);
            hashtable1.Add("&Iuml;", 0x36);
            hashtable1.Add("&ETH;", 0x37);
            hashtable1.Add("&Ntilde;", 0x38);
            hashtable1.Add("&Ograve;", 0x39);
            hashtable1.Add("&Oacute;", 0x3a);
            hashtable1.Add("&Ocirc;", 0x3b);
            hashtable1.Add("&Otilde;", 60);
            hashtable1.Add("&Ouml;", 0x3d);
            hashtable1.Add("&times;", 0x3e);
            hashtable1.Add("&Oslash;", 0x3f);
            hashtable1.Add("&Ugrave;", 0x40);
            hashtable1.Add("&Uacute;", 0x41);
            hashtable1.Add("&Ucirc;", 0x42);
            hashtable1.Add("&Uuml;", 0x43);
            hashtable1.Add("&Yacute;", 0x44);
            hashtable1.Add("&THORN;", 0x45);
            hashtable1.Add("&szlig;", 70);
            hashtable1.Add("&agrave;", 0x47);
            hashtable1.Add("&aacute;", 0x48);
            hashtable1.Add("&acirc;", 0x49);
            hashtable1.Add("&atilde;", 0x4a);
            hashtable1.Add("&auml;", 0x4b);
            hashtable1.Add("&aring;", 0x4c);
            hashtable1.Add("&aelig;", 0x4d);
            hashtable1.Add("&ccedil;", 0x4e);
            hashtable1.Add("&egrave;", 0x4f);
            hashtable1.Add("&eacute;", 80);
            hashtable1.Add("&ecirc;", 0x51);
            hashtable1.Add("&euml;", 0x52);
            hashtable1.Add("&igrave;", 0x53);
            hashtable1.Add("&iacute;", 0x54);
            hashtable1.Add("&icirc;", 0x55);
            hashtable1.Add("&iuml;", 0x56);
            hashtable1.Add("&eth;", 0x57);
            hashtable1.Add("&ntilde;", 0x58);
            hashtable1.Add("&ograve;", 0x59);
            hashtable1.Add("&oacute;", 90);
            hashtable1.Add("&ocirc;", 0x5b);
            hashtable1.Add("&otilde;", 0x5c);
            hashtable1.Add("&ouml;", 0x5d);
            hashtable1.Add("&divide;", 0x5e);
            hashtable1.Add("&oslash;", 0x5f);
            hashtable1.Add("&ugrave;", 0x60);
            hashtable1.Add("&uacute;", 0x61);
            hashtable1.Add("&ucirc;", 0x62);
            hashtable1.Add("&uuml;", 0x63);
            hashtable1.Add("&yacute;", 100);
            hashtable1.Add("&thorn;", 0x65);
            hashtable1.Add("&yuml;", 0x66);
            hashtable1.Add("&fnof;", 0x67);
            hashtable1.Add("&Alpha;", 0x68);
            hashtable1.Add("&Beta;", 0x69);
            hashtable1.Add("&Gamma;", 0x6a);
            hashtable1.Add("&Delta;", 0x6b);
            hashtable1.Add("&Epsilon;", 0x6c);
            hashtable1.Add("&Zeta;", 0x6d);
            hashtable1.Add("&Eta;", 110);
            hashtable1.Add("&Theta;", 0x6f);
            hashtable1.Add("&Iota;", 0x70);
            hashtable1.Add("&Kappa;", 0x71);
            hashtable1.Add("&Lambda;", 0x72);
            hashtable1.Add("&Mu;", 0x73);
            hashtable1.Add("&Nu;", 0x74);
            hashtable1.Add("&Xi;", 0x75);
            hashtable1.Add("&Omicron;", 0x76);
            hashtable1.Add("&Pi;", 0x77);
            hashtable1.Add("&Rho;", 120);
            hashtable1.Add("&Sigma;", 0x79);
            hashtable1.Add("&Tau;", 0x7a);
            hashtable1.Add("&Upsilon;", 0x7b);
            hashtable1.Add("&Phi;", 0x7c);
            hashtable1.Add("&Chi;", 0x7d);
            hashtable1.Add("&Psi;", 0x7e);
            hashtable1.Add("&Omega;", 0x7f);
            hashtable1.Add("&alpha;", 0x80);
            hashtable1.Add("&beta;", 0x81);
            hashtable1.Add("&gamma;", 130);
            hashtable1.Add("&delta;", 0x83);
            hashtable1.Add("&epsilon;", 0x84);
            hashtable1.Add("&zeta;", 0x85);
            hashtable1.Add("&eta;", 0x86);
            hashtable1.Add("&theta;", 0x87);
            hashtable1.Add("&iota;", 0x88);
            hashtable1.Add("&kappa;", 0x89);
            hashtable1.Add("&lambda;", 0x8a);
            hashtable1.Add("&mu;", 0x8b);
            hashtable1.Add("&nu;", 140);
            hashtable1.Add("&xi;", 0x8d);
            hashtable1.Add("&omicron;", 0x8e);
            hashtable1.Add("&pi;", 0x8f);
            hashtable1.Add("&rho;", 0x90);
            hashtable1.Add("&sigmaf;", 0x91);
            hashtable1.Add("&sigma;", 0x92);
            hashtable1.Add("&tau;", 0x93);
            hashtable1.Add("&upsilon;", 0x94);
            hashtable1.Add("&phi;", 0x95);
            hashtable1.Add("&chi;", 150);
            hashtable1.Add("&psi;", 0x97);
            hashtable1.Add("&omega;", 0x98);
            hashtable1.Add("&thetasym;", 0x99);
            hashtable1.Add("&upsih;", 0x9a);
            hashtable1.Add("&piv;", 0x9b);
            hashtable1.Add("&bull;", 0x9c);
            hashtable1.Add("&hellip;", 0x9d);
            hashtable1.Add("&prime;", 0x9e);
            hashtable1.Add("&Prime;", 0x9f);
            hashtable1.Add("&oline;", 160);
            hashtable1.Add("&frasl;", 0xa1);
            hashtable1.Add("&weierp;", 0xa2);
            hashtable1.Add("&image;", 0xa3);
            hashtable1.Add("&real;", 0xa4);
            hashtable1.Add("&trade;", 0xa5);
            hashtable1.Add("&alefsym;", 0xa6);
            hashtable1.Add("&larr;", 0xa7);
            hashtable1.Add("&uarr;", 0xa8);
            hashtable1.Add("&rarr;", 0xa9);
            hashtable1.Add("&darr;", 170);
            hashtable1.Add("&harr;", 0xab);
            hashtable1.Add("&crarr;", 0xac);
            hashtable1.Add("&lArr;", 0xad);
            hashtable1.Add("&uArr;", 0xae);
            hashtable1.Add("&rArr;", 0xaf);
            hashtable1.Add("&dArr;", 0xb0);
            hashtable1.Add("&hArr;", 0xb1);
            hashtable1.Add("&forall;", 0xb2);
            hashtable1.Add("&part;", 0xb3);
            hashtable1.Add("&exist;", 180);
            hashtable1.Add("&empty;", 0xb5);
            hashtable1.Add("&nabla;", 0xb6);
            hashtable1.Add("&isin;", 0xb7);
            hashtable1.Add("&notin;", 0xb8);
            hashtable1.Add("&ni;", 0xb9);
            hashtable1.Add("&prod;", 0xba);
            hashtable1.Add("&sum;", 0xbb);
            hashtable1.Add("&minus;", 0xbc);
            hashtable1.Add("&lowast;", 0xbd);
            hashtable1.Add("&radic;", 190);
            hashtable1.Add("&prop;", 0xbf);
            hashtable1.Add("&infin;", 0xc0);
            hashtable1.Add("&ang;", 0xc1);
            hashtable1.Add("&and;", 0xc2);
            hashtable1.Add("&or;", 0xc3);
            hashtable1.Add("&cap;", 0xc4);
            hashtable1.Add("&cup;", 0xc5);
            hashtable1.Add("&int;", 0xc6);
            hashtable1.Add("&there4;", 0xc7);
            hashtable1.Add("&sim;", 200);
            hashtable1.Add("&cong;", 0xc9);
            hashtable1.Add("&asymp;", 0xca);
            hashtable1.Add("&ne;", 0xcb);
            hashtable1.Add("&equiv;", 0xcc);
            hashtable1.Add("&le;", 0xcd);
            hashtable1.Add("&ge;", 0xce);
            hashtable1.Add("&sub;", 0xcf);
            hashtable1.Add("&sup;", 0xd0);
            hashtable1.Add("&nsub;", 0xd1);
            hashtable1.Add("&sube;", 210);
            hashtable1.Add("&supe;", 0xd3);
            hashtable1.Add("&oplus;", 0xd4);
            hashtable1.Add("&otimes;", 0xd5);
            hashtable1.Add("&perp;", 0xd6);
            hashtable1.Add("&sdot;", 0xd7);
            hashtable1.Add("&lceil;", 0xd8);
            hashtable1.Add("&rceil;", 0xd9);
            hashtable1.Add("&lfloor;", 0xda);
            hashtable1.Add("&rfloor;", 0xdb);
            hashtable1.Add("&lang;", 220);
            hashtable1.Add("&rang;", 0xdd);
            hashtable1.Add("&loz;", 0xde);
            hashtable1.Add("&spades;", 0xdf);
            hashtable1.Add("&clubs;", 0xe0);
            hashtable1.Add("&hearts;", 0xe1);
            hashtable1.Add("&diams;", 0xe2);
            hashtable1.Add("&quot", 0xe3);
            hashtable1.Add("&amp", 0xe4);
            hashtable1.Add("&lt", 0xe5);
            hashtable1.Add("&gt", 230);
            hashtable1.Add("&OElig", 0xe7);
            hashtable1.Add("&oelig", 0xe8);
            hashtable1.Add("&Scaron", 0xe9);
            hashtable1.Add("&scaron", 0xea);
            hashtable1.Add("&Yuml", 0xeb);
            hashtable1.Add("&circ", 0xec);
            hashtable1.Add("&tilde", 0xed);
            hashtable1.Add("&ensp", 0xee);
            hashtable1.Add("&emsp", 0xef);
            hashtable1.Add("&thinsp", 240);
            hashtable1.Add("&zwnj", 0xf1);
            hashtable1.Add("&zwj", 0xf2);
            hashtable1.Add("&lrm", 0xf3);
            hashtable1.Add("&rlm", 0xf4);
            hashtable1.Add("&ndash", 0xf5);
            hashtable1.Add("&mdash", 0xf6);
            hashtable1.Add("&lsquo", 0xf7);
            hashtable1.Add("&rsquo", 0xf8);
            hashtable1.Add("&sbquo", 0xf9);
            hashtable1.Add("&ldquo", 250);
            hashtable1.Add("&rdquo", 0xfb);
            hashtable1.Add("&bdquo", 0xfc);
            hashtable1.Add("&dagger", 0xfd);
            hashtable1.Add("&Dagger", 0xfe);
            hashtable1.Add("&permil", 0xff);
            hashtable1.Add("&lsaquo", 0x100);
            hashtable1.Add("&rsaquo", 0x101);
            hashtable1.Add("&#0;", 0x102);
            hashtable1.Add("&#1;", 0x103);
            hashtable1.Add("&#2;", 260);
            hashtable1.Add("&#3;", 0x105);
            hashtable1.Add("&#4;", 0x106);
            hashtable1.Add("&#5;", 0x107);
            hashtable1.Add("&#6;", 0x108);
            hashtable1.Add("&#7;", 0x109);
            hashtable1.Add("&#8;", 0x10a);
            hashtable1.Add("&#9;", 0x10b);
            hashtable1.Add("&#10;", 0x10c);
            hashtable1.Add("&#11;", 0x10d);
            hashtable1.Add("&#12;", 270);
            hashtable1.Add("&#13;", 0x10f);
            hashtable1.Add("&#14;", 0x110);
            hashtable1.Add("&#15;", 0x111);
            hashtable1.Add("&#16;", 0x112);
            hashtable1.Add("&#17;", 0x113);
            hashtable1.Add("&#18;", 0x114);
            hashtable1.Add("&#19;", 0x115);
            hashtable1.Add("&#20;", 0x116);
            hashtable1.Add("&#21;", 0x117);
            hashtable1.Add("&#22;", 280);
            hashtable1.Add("&#23;", 0x119);
            hashtable1.Add("&#24;", 0x11a);
            hashtable1.Add("&#25;", 0x11b);
            hashtable1.Add("&#26;", 0x11c);
            hashtable1.Add("&#27;", 0x11d);
            hashtable1.Add("&#28;", 0x11e);
            hashtable1.Add("&#29;", 0x11f);
            hashtable1.Add("&#30;", 0x120);
            hashtable1.Add("&#31;", 0x121);
            hashtable1.Add("&#32;", 290);
            hashtable1.Add("&#33;", 0x123);
            hashtable1.Add("&#34;", 0x124);
            hashtable1.Add("&#35;", 0x125);
            hashtable1.Add("&#36;", 0x126);
            hashtable1.Add("&#37;", 0x127);
            hashtable1.Add("&#38;", 0x128);
            hashtable1.Add("&#39;", 0x129);
            hashtable1.Add("&#40;", 0x12a);
            hashtable1.Add("&#41;", 0x12b);
            hashtable1.Add("&#42;", 300);
            hashtable1.Add("&#43;", 0x12d);
            hashtable1.Add("&#44;", 0x12e);
            hashtable1.Add("&#45;", 0x12f);
            hashtable1.Add("&#46;", 0x130);
            hashtable1.Add("&#47;", 0x131);
            hashtable1.Add("&#48;", 0x132);
            hashtable1.Add("&#49;", 0x133);
            hashtable1.Add("&#50;", 0x134);
            hashtable1.Add("&#51;", 0x135);
            hashtable1.Add("&#52;", 310);
            hashtable1.Add("&#53;", 0x137);
            hashtable1.Add("&#54;", 0x138);
            hashtable1.Add("&#55;", 0x139);
            hashtable1.Add("&#56;", 0x13a);
            hashtable1.Add("&#57;", 0x13b);
            hashtable1.Add("&#58;", 0x13c);
            hashtable1.Add("&#59;", 0x13d);
            hashtable1.Add("&#60;", 0x13e);
            hashtable1.Add("&#61;", 0x13f);
            hashtable1.Add("&#62;", 320);
            hashtable1.Add("&#63;", 0x141);
            hashtable1.Add("&#64;", 0x142);
            hashtable1.Add("&#65;", 0x143);
            hashtable1.Add("&#66;", 0x144);
            hashtable1.Add("&#67;", 0x145);
            hashtable1.Add("&#68;", 0x146);
            hashtable1.Add("&#69;", 0x147);
            hashtable1.Add("&#70;", 0x148);
            hashtable1.Add("&#71;", 0x149);
            hashtable1.Add("&#72;", 330);
            hashtable1.Add("&#73;", 0x14b);
            hashtable1.Add("&#74;", 0x14c);
            hashtable1.Add("&#75;", 0x14d);
            hashtable1.Add("&#76;", 0x14e);
            hashtable1.Add("&#77;", 0x14f);
            hashtable1.Add("&#78;", 0x150);
            hashtable1.Add("&#79;", 0x151);
            hashtable1.Add("&#80;", 0x152);
            hashtable1.Add("&#81;", 0x153);
            hashtable1.Add("&#82;", 340);
            hashtable1.Add("&#83;", 0x155);
            hashtable1.Add("&#84;", 0x156);
            hashtable1.Add("&#85;", 0x157);
            hashtable1.Add("&#86;", 0x158);
            hashtable1.Add("&#87;", 0x159);
            hashtable1.Add("&#88;", 0x15a);
            hashtable1.Add("&#89;", 0x15b);
            hashtable1.Add("&#90;", 0x15c);
            hashtable1.Add("&#91;", 0x15d);
            hashtable1.Add("&#92;", 350);
            hashtable1.Add("&#93;", 0x15f);
            hashtable1.Add("&#94;", 0x160);
            hashtable1.Add("&#95;", 0x161);
            hashtable1.Add("&#96;", 0x162);
            hashtable1.Add("&#97;", 0x163);
            hashtable1.Add("&#98;", 0x164);
            hashtable1.Add("&#99;", 0x165);
            hashtable1.Add("&#100;", 0x166);
            hashtable1.Add("&#101;", 0x167);
            hashtable1.Add("&#102;", 360);
            hashtable1.Add("&#103;", 0x169);
            hashtable1.Add("&#104;", 0x16a);
            hashtable1.Add("&#105;", 0x16b);
            hashtable1.Add("&#106;", 0x16c);
            hashtable1.Add("&#107;", 0x16d);
            hashtable1.Add("&#108;", 0x16e);
            hashtable1.Add("&#109;", 0x16f);
            hashtable1.Add("&#110;", 0x170);
            hashtable1.Add("&#111;", 0x171);
            hashtable1.Add("&#112;", 370);
            hashtable1.Add("&#113;", 0x173);
            hashtable1.Add("&#114;", 0x174);
            hashtable1.Add("&#115;", 0x175);
            hashtable1.Add("&#116;", 0x176);
            hashtable1.Add("&#117;", 0x177);
            hashtable1.Add("&#118;", 0x178);
            hashtable1.Add("&#119;", 0x179);
            hashtable1.Add("&#120;", 0x17a);
            hashtable1.Add("&#121;", 0x17b);
            hashtable1.Add("&#122;", 380);
            hashtable1.Add("&#123;", 0x17d);
            hashtable1.Add("&#124;", 0x17e);
            hashtable1.Add("&#125;", 0x17f);
            hashtable1.Add("&#126;", 0x180);
            hashtable1.Add("&#127;", 0x181);
            hashtable1.Add("&#128;", 0x182);
            hashtable1.Add("&#129;", 0x183);
            hashtable1.Add("&#130;", 0x184);
            hashtable1.Add("&#131;", 0x185);
            hashtable1.Add("&#132;", 390);
            hashtable1.Add("&#133;", 0x187);
            hashtable1.Add("&#134;", 0x188);
            hashtable1.Add("&#135;", 0x189);
            hashtable1.Add("&#136;", 0x18a);
            hashtable1.Add("&#137;", 0x18b);
            hashtable1.Add("&#138;", 0x18c);
            hashtable1.Add("&#139;", 0x18d);
            hashtable1.Add("&#140;", 0x18e);
            hashtable1.Add("&#141;", 0x18f);
            hashtable1.Add("&#142;", 400);
            hashtable1.Add("&#143;", 0x191);
            hashtable1.Add("&#144;", 0x192);
            hashtable1.Add("&#145;", 0x193);
            hashtable1.Add("&#146;", 0x194);
            hashtable1.Add("&#147;", 0x195);
            hashtable1.Add("&#148;", 0x196);
            hashtable1.Add("&#149;", 0x197);
            hashtable1.Add("&#150;", 0x198);
            hashtable1.Add("&#151;", 0x199);
            hashtable1.Add("&#152;", 410);
            hashtable1.Add("&#153;", 0x19b);
            hashtable1.Add("&#154;", 0x19c);
            hashtable1.Add("&#155;", 0x19d);
            hashtable1.Add("&#156;", 0x19e);
            hashtable1.Add("&#157;", 0x19f);
            hashtable1.Add("&#158;", 0x1a0);
            hashtable1.Add("&#159;", 0x1a1);
            hashtable1.Add("&#160;", 0x1a2);
            hashtable1.Add("&#161;", 0x1a3);
            hashtable1.Add("&#162;", 420);
            hashtable1.Add("&#163;", 0x1a5);
            hashtable1.Add("&#164;", 0x1a6);
            hashtable1.Add("&#165;", 0x1a7);
            hashtable1.Add("&#166;", 0x1a8);
            hashtable1.Add("&#167;", 0x1a9);
            hashtable1.Add("&#168;", 0x1aa);
            hashtable1.Add("&#169;", 0x1ab);
            hashtable1.Add("&#170;", 0x1ac);
            hashtable1.Add("&#171;", 0x1ad);
            hashtable1.Add("&#172;", 430);
            hashtable1.Add("&#173;", 0x1af);
            hashtable1.Add("&#174;", 0x1b0);
            hashtable1.Add("&#175;", 0x1b1);
            hashtable1.Add("&#176;", 0x1b2);
            hashtable1.Add("&#177;", 0x1b3);
            hashtable1.Add("&#178;", 0x1b4);
            hashtable1.Add("&#179;", 0x1b5);
            hashtable1.Add("&#180;", 0x1b6);
            hashtable1.Add("&#181;", 0x1b7);
            hashtable1.Add("&#182;", 440);
            hashtable1.Add("&#183;", 0x1b9);
            hashtable1.Add("&#184;", 0x1ba);
            hashtable1.Add("&#185;", 0x1bb);
            hashtable1.Add("&#186;", 0x1bc);
            hashtable1.Add("&#187;", 0x1bd);
            hashtable1.Add("&#188;", 0x1be);
            hashtable1.Add("&#189;", 0x1bf);
            hashtable1.Add("&#190;", 0x1c0);
            hashtable1.Add("&#191;", 0x1c1);
            hashtable1.Add("&#192;", 450);
            hashtable1.Add("&#193;", 0x1c3);
            hashtable1.Add("&#194;", 0x1c4);
            hashtable1.Add("&#195;", 0x1c5);
            hashtable1.Add("&#196;", 0x1c6);
            hashtable1.Add("&#197;", 0x1c7);
            hashtable1.Add("&#198;", 0x1c8);
            hashtable1.Add("&#199;", 0x1c9);
            hashtable1.Add("&#200;", 0x1ca);
            hashtable1.Add("&#201;", 0x1cb);
            hashtable1.Add("&#202;", 460);
            hashtable1.Add("&#203;", 0x1cd);
            hashtable1.Add("&#204;", 0x1ce);
            hashtable1.Add("&#205;", 0x1cf);
            hashtable1.Add("&#206;", 0x1d0);
            hashtable1.Add("&#207;", 0x1d1);
            hashtable1.Add("&#208;", 0x1d2);
            hashtable1.Add("&#209;", 0x1d3);
            hashtable1.Add("&#210;", 0x1d4);
            hashtable1.Add("&#211;", 0x1d5);
            hashtable1.Add("&#212;", 470);
            hashtable1.Add("&#213;", 0x1d7);
            hashtable1.Add("&#214;", 0x1d8);
            hashtable1.Add("&#215;", 0x1d9);
            hashtable1.Add("&#216;", 0x1da);
            hashtable1.Add("&#217;", 0x1db);
            hashtable1.Add("&#218;", 0x1dc);
            hashtable1.Add("&#219;", 0x1dd);
            hashtable1.Add("&#220;", 0x1de);
            hashtable1.Add("&#221;", 0x1df);
            hashtable1.Add("&#222;", 480);
            hashtable1.Add("&#223;", 0x1e1);
            hashtable1.Add("&#224;", 0x1e2);
            hashtable1.Add("&#225;", 0x1e3);
            hashtable1.Add("&#226;", 0x1e4);
            hashtable1.Add("&#227;", 0x1e5);
            hashtable1.Add("&#228;", 0x1e6);
            hashtable1.Add("&#229;", 0x1e7);
            hashtable1.Add("&#230;", 0x1e8);
            hashtable1.Add("&#231;", 0x1e9);
            hashtable1.Add("&#232;", 490);
            hashtable1.Add("&#233;", 0x1eb);
            hashtable1.Add("&#234;", 0x1ec);
            hashtable1.Add("&#235;", 0x1ed);
            hashtable1.Add("&#236;", 0x1ee);
            hashtable1.Add("&#237;", 0x1ef);
            hashtable1.Add("&#238;", 0x1f0);
            hashtable1.Add("&#239;", 0x1f1);
            hashtable1.Add("&#240;", 0x1f2);
            hashtable1.Add("&#241;", 0x1f3);
            hashtable1.Add("&#242;", 500);
            hashtable1.Add("&#243;", 0x1f5);
            hashtable1.Add("&#244;", 0x1f6);
            hashtable1.Add("&#245;", 0x1f7);
            hashtable1.Add("&#246;", 0x1f8);
            hashtable1.Add("&#247;", 0x1f9);
            hashtable1.Add("&#248;", 0x1fa);
            hashtable1.Add("&#249;", 0x1fb);
            hashtable1.Add("&#250;", 0x1fc);
            hashtable1.Add("&#251;", 0x1fd);
            hashtable1.Add("&#252;", 510);
            hashtable1.Add("&#253;", 0x1ff);
            hashtable1.Add("&#254;", 0x200);
            hashtable1.Add("&#255;", 0x201);
            inside.r = hashtable1;
        }
        if (((obj1 = A_0.ToLower()) != null) && ((obj1 = inside.r[obj1]) != null))
        {
            switch ((int)obj1)
            {
                case 0: return new string('"', 1);
                case 1: return new string('&', 1);
                case 2: return new string('<', 1);
                case 3: return new string('>', 1);
                case 4: return new string('\x00a0', 1);
                case 5: return new string('\x00a1', 1);
                case 6: return new string('\x00a2', 1);
                case 7: return new string('\x00a3', 1);
                case 8: return new string('\x00a4', 1);
                case 9: return new string('\x00a5', 1);
                case 10: return new string('\x00a6', 1);
                case 11: return new string('\x00a6', 1);
                case 12: return new string('\x00a7', 1);
                case 13: return new string('\x00a8', 1);
                case 14: return new string('\x00a8', 1);
                case 15: return new string('\x00a9', 1);
                case 0x10: return new string('\x00aa', 1);
                case 0x11: return new string('\x00ab', 1);
                case 0x12: return new string('\x00ac', 1);
                case 0x13: return new string('\x00ad', 1);
                case 20: return new string('\x00ae', 1);
                case 0x15: return new string('\x00af', 1);
                case 0x16: return new string('\x00af', 1);
                case 0x17: return new string('\x00b0', 1);
                case 0x18: return new string('\x00b1', 1);
                case 0x19: return new string('\x00b2', 1);
                case 0x1a: return new string('\x00b3', 1);
                case 0x1b: return new string('\x00b4', 1);
                case 0x1c: return new string('\x00b5', 1);
                case 0x1d: return new string('\x00b6', 1);
                case 30: return new string('\x00b7', 1);
                case 0x1f: return new string('\x00b8', 1);
                case 0x20: return new string('\x00b9', 1);
                case 0x21: return new string('\x00ba', 1);
                case 0x22: return new string('\x00bb', 1);
                case 0x23: return new string('\x00bc', 1);
                case 0x24: return new string('\x00bd', 1);
                case 0x25: return new string('\x00be', 1);
                case 0x26: return new string('\x00bf', 1);
                case 0x27: return new string('\x00c0', 1);
                case 40: return new string('\x00c1', 1);
                case 0x29: return new string('\x00c2', 1);
                case 0x2a: return new string('\x00c3', 1);
                case 0x2b: return new string('\x00c4', 1);
                case 0x2c: return new string('\x00c5', 1);
                case 0x2d: return new string('\x00c6', 1);
                case 0x2e: return new string('\x00c7', 1);
                case 0x2f: return new string('\x00c8', 1);
                case 0x30: return new string('\x00c9', 1);
                case 0x31: return new string('\x00ca', 1);
                case 50: return new string('\x00cb', 1);
                case 0x33: return new string('\x00cc', 1);
                case 0x34: return new string('\x00cd', 1);
                case 0x35: return new string('\x00ce', 1);
                case 0x36: return new string('\x00cf', 1);
                case 0x37: return new string('\x00d0', 1);
                case 0x38: return new string('\x00d1', 1);
                case 0x39: return new string('\x00d2', 1);
                case 0x3a: return new string('\x00d3', 1);
                case 0x3b: return new string('\x00d4', 1);
                case 60: return new string('\x00d5', 1);
                case 0x3d: return new string('\x00d6', 1);
                case 0x3e: return new string('\x00d7', 1);
                case 0x3f: return new string('\x00d8', 1);
                case 0x40: return new string('\x00d9', 1);
                case 0x41: return new string('\x00da', 1);
                case 0x42: return new string('\x00db', 1);
                case 0x43: return new string('\x00dc', 1);
                case 0x44: return new string('\x00dd', 1);
                case 0x45: return new string('\x00de', 1);
                case 70: return new string('\x00df', 1);
                case 0x47: return new string('\x00e0', 1);
                case 0x48: return new string('\x00e1', 1);
                case 0x49: return new string('\x00e2', 1);
                case 0x4a: return new string('\x00e3', 1);
                case 0x4b: return new string('\x00e4', 1);
                case 0x4c: return new string('\x00e5', 1);
                case 0x4d: return new string('\x00e6', 1);
                case 0x4e: return new string('\x00e7', 1);
                case 0x4f: return new string('\x00e8', 1);
                case 80: return new string('\x00e9', 1);
                case 0x51: return new string('\x00ea', 1);
                case 0x52: return new string('\x00eb', 1);
                case 0x53: return new string('\x00ec', 1);
                case 0x54: return new string('\x00ed', 1);
                case 0x55: return new string('\x00ee', 1);
                case 0x56: return new string('\x00ef', 1);
                case 0x57: return new string('\x00f0', 1);
                case 0x58: return new string('\x00f1', 1);
                case 0x59: return new string('\x00f2', 1);
                case 90: return new string('\x00f3', 1);
                case 0x5b: return new string('\x00f4', 1);
                case 0x5c: return new string('\x00f5', 1);
                case 0x5d: return new string('\x00f6', 1);
                case 0x5e: return new string('\x00f7', 1);
                case 0x5f: return new string('\x00f8', 1);
                case 0x60: return new string('\x00f9', 1);
                case 0x61: return new string('\x00fa', 1);
                case 0x62: return new string('\x00fb', 1);
                case 0x63: return new string('\x00fc', 1);
                case 100: return new string('\x00fd', 1);
                case 0x65: return new string('\x00fe', 1);
                case 0x66: return new string('\x00ff', 1);
                case 0x67: return new string('\u0192', 1);
                case 0x68: return new string('\u0391', 1);
                case 0x69: return new string('\u0392', 1);
                case 0x6a: return new string('\u0393', 1);
                case 0x6b: return new string('\u0394', 1);
                case 0x6c: return new string('\u0395', 1);
                case 0x6d: return new string('\u0396', 1);
                case 110: return new string('\u0397', 1);
                case 0x6f: return new string('\u0398', 1);
                case 0x70: return new string('\u0399', 1);
                case 0x71: return new string('\u039a', 1);
                case 0x72: return new string('\u039b', 1);
                case 0x73: return new string('\u039c', 1);
                case 0x74: return new string('\u039d', 1);
                case 0x75: return new string('\u039e', 1);
                case 0x76: return new string('\u039f', 1);
                case 0x77: return new string('\u03a0', 1);
                case 120: return new string('\u03a1', 1);
                case 0x79: return new string('\u03a3', 1);
                case 0x7a: return new string('\u03a4', 1);
                case 0x7b: return new string('\u03a5', 1);
                case 0x7c: return new string('\u03a6', 1);
                case 0x7d: return new string('\u03a7', 1);
                case 0x7e: return new string('\u03a8', 1);
                case 0x7f: return new string('\u03a9', 1);
                case 0x80: return new string('\u03b1', 1);
                case 0x81: return new string('\u03b2', 1);
                case 130: return new string('\u03b3', 1);
                case 0x83: return new string('\u03b4', 1);
                case 0x84: return new string('\u03b5', 1);
                case 0x85: return new string('\u03b6', 1);
                case 0x86: return new string('\u03b7', 1);
                case 0x87: return new string('\u03b8', 1);
                case 0x88: return new string('\u03b9', 1);
                case 0x89: return new string('\u03ba', 1);
                case 0x8a: return new string('\u03bb', 1);
                case 0x8b: return new string('\u03bc', 1);
                case 140: return new string('\u03bd', 1);
                case 0x8d: return new string('\u03be', 1);
                case 0x8e: return new string('\u03bf', 1);
                case 0x8f: return new string('\u03c0', 1);
                case 0x90: return new string('\u03c1', 1);
                case 0x91: return new string('\u03c2', 1);
                case 0x92: return new string('\u03c3', 1);
                case 0x93: return new string('\u03c4', 1);
                case 0x94: return new string('\u03c5', 1);
                case 0x95: return new string('\u03c6', 1);
                case 150: return new string('\u03c7', 1);
                case 0x97: return new string('\u03c8', 1);
                case 0x98: return new string('\u03c9', 1);
                case 0x99: return new string('\u03d1', 1);
                case 0x9a: return new string('\u03d2', 1);
                case 0x9b: return new string('\u03d6', 1);
                case 0x9c: return new string('\u2022', 1);
                case 0x9d: return new string('\u2026', 1);
                case 0x9e: return new string('\u2032', 1);
                case 0x9f: return new string('\u2033', 1);
                case 160: return new string('\u203e', 1);
                case 0xa1: return new string('\u2044', 1);
                case 0xa2: return new string('\u2118', 1);
                case 0xa3: return new string('\u2111', 1);
                case 0xa4: return new string('\u211c', 1);
                case 0xa5: return new string('\u2122', 1);
                case 0xa6: return new string('\u2135', 1);
                case 0xa7: return new string('\u2190', 1);
                case 0xa8: return new string('\u2191', 1);
                case 0xa9: return new string('\u2192', 1);
                case 170: return new string('\u2193', 1);
                case 0xab: return new string('\u2194', 1);
                case 0xac: return new string('\u21b5', 1);
                case 0xad: return new string('\u21d0', 1);
                case 0xae: return new string('\u21d1', 1);
                case 0xaf: return new string('\u21d2', 1);
                case 0xb0: return new string('\u21d3', 1);
                case 0xb1: return new string('\u21d4', 1);
                case 0xb2: return new string('\u2200', 1);
                case 0xb3: return new string('\u2202', 1);
                case 180: return new string('\u2203', 1);
                case 0xb5: return new string('\u2205', 1);
                case 0xb6: return new string('\u2207', 1);
                case 0xb7: return new string('\u2208', 1);
                case 0xb8: return new string('\u2209', 1);
                case 0xb9: return new string('\u220b', 1);
                case 0xba: return new string('\u220f', 1);
                case 0xbb: return new string('\u2212', 1);
                case 0xbc: return new string('\u2212', 1);
                case 0xbd: return new string('\u2217', 1);
                case 190: return new string('\u221a', 1);
                case 0xbf: return new string('\u221d', 1);
                case 0xc0: return new string('\u221e', 1);
                case 0xc1: return new string('\u2220', 1);
                case 0xc2: return new string('\u22a5', 1);
                case 0xc3: return new string('\u22a6', 1);
                case 0xc4: return new string('\u2229', 1);
                case 0xc5: return new string('\u222a', 1);
                case 0xc6: return new string('\u222b', 1);
                case 0xc7: return new string('\u2234', 1);
                case 200: return new string('\u223c', 1);
                case 0xc9: return new string('\u2245', 1);
                case 0xca: return new string('\u2245', 1);
                case 0xcb: return new string('\u2260', 1);
                case 0xcc: return new string('\u2261', 1);
                case 0xcd: return new string('\u2264', 1);
                case 0xce: return new string('\u2265', 1);
                case 0xcf: return new string('\u2282', 1);
                case 0xd0: return new string('\u2283', 1);
                case 0xd1: return new string('\u2284', 1);
                case 210: return new string('\u2286', 1);
                case 0xd3: return new string('\u2287', 1);
                case 0xd4: return new string('\u2295', 1);
                case 0xd5: return new string('\u2297', 1);
                case 0xd6: return new string('\u22a5', 1);
                case 0xd7: return new string('\u22c5', 1);
                case 0xd8: return new string('\u2308', 1);
                case 0xd9: return new string('\u2309', 1);
                case 0xda: return new string('\u230a', 1);
                case 0xdb: return new string('\u230b', 1);
                case 220: return new string('\u2329', 1);
                case 0xdd: return new string('\u232a', 1);
                case 0xde: return new string('\u25ca', 1);
                case 0xdf: return new string('\u2660', 1);
                case 0xe0: return new string('\u2663', 1);
                case 0xe1: return new string('\u2665', 1);
                case 0xe2: return new string('\u2666', 1);
                case 0xe3: return new string('"', 1);
                case 0xe4: return new string('&', 1);
                case 0xe5: return new string('<', 1);
                case 230: return new string('>', 1);
                case 0xe7: return new string('\u0152', 1);
                case 0xe8: return new string('\u0153', 1);
                case 0xe9: return new string('\u0160', 1);
                case 0xea: return new string('\u0161', 1);
                case 0xeb: return new string('\u0178', 1);
                case 0xec: return new string('\u02c6', 1);
                case 0xed: return new string('\u02dc', 1);
                case 0xee: return new string('\u2002', 1);
                case 0xef: return new string('\u2003', 1);
                case 240: return new string('\u2009', 1);
                case 0xf1: return new string('\u200c', 1);
                case 0xf2: return new string('\u200d', 1);
                case 0xf3: return new string('\u200e', 1);
                case 0xf4: return new string('\u200f', 1);
                case 0xf5: return new string('\u2013', 1);
                case 0xf6: return new string('\x0097', 1);
                case 0xf7: return new string('\u2018', 1);
                case 0xf8: return new string('\u2019', 1);
                case 0xf9: return new string('\u201a', 1);
                case 250: return new string('\u201c', 1);
                case 0xfb: return new string('\u201d', 1);
                case 0xfc: return new string('\u201e', 1);
                case 0xfd: return new string('\u2020', 1);
                case 0xfe: return new string('\u2021', 1);
                case 0xff: return new string('\u2030', 1);
                case 0x100: return new string('\u2039', 1);
                case 0x101: return new string('\u203a', 1);
                case 0x102: return new string('\0', 1);
                case 0x103: return new string('\x0001', 1);
                case 260: return new string('\x0002', 1);
                case 0x105: return new string('\x0003', 1);
                case 0x106: return new string('\x0004', 1);
                case 0x107: return new string('\x0005', 1);
                case 0x108: return new string('\x0006', 1);
                case 0x109: return new string('\a', 1);
                case 0x10a: return new string('\b', 1);
                case 0x10b: return new string('\t', 1);
                case 0x10c: return new string('\n', 1);
                case 0x10d: return new string('\v', 1);
                case 270: return new string('\f', 1);
                case 0x10f: return new string('\r', 1);
                case 0x110: return new string('\x000e', 1);
                case 0x111: return new string('\x000f', 1);
                case 0x112: return new string('\x0010', 1);
                case 0x113: return new string('\x0011', 1);
                case 0x114: return new string('\x0012', 1);
                case 0x115: return new string('\x0013', 1);
                case 0x116: return new string('\x0014', 1);
                case 0x117: return new string('\x0015', 1);
                case 280: return new string('\x0016', 1);
                case 0x119: return new string('\x0017', 1);
                case 0x11a: return new string('\x0018', 1);
                case 0x11b: return new string('\x0019', 1);
                case 0x11c: return new string('\x001a', 1);
                case 0x11d: return new string('\x001b', 1);
                case 0x11e: return new string('\x001c', 1);
                case 0x11f: return new string('\x001d', 1);
                case 0x120: return new string('\x001e', 1);
                case 0x121: return new string('\x001f', 1);
                case 290: return new string(' ', 1);
                case 0x123: return new string('!', 1);
                case 0x124: return new string('"', 1);
                case 0x125: return new string('#', 1);
                case 0x126: return new string('$', 1);
                case 0x127: return new string('%', 1);
                case 0x128: return new string('&', 1);
                case 0x129: return new string('\'', 1);
                case 0x12a: return new string('(', 1);
                case 0x12b: return new string(')', 1);
                case 300: return new string('*', 1);
                case 0x12d: return new string('+', 1);
                case 0x12e: return new string(',', 1);
                case 0x12f: return new string('-', 1);
                case 0x130: return new string('.', 1);
                case 0x131: return new string('/', 1);
                case 0x132: return new string('0', 1);
                case 0x133: return new string('1', 1);
                case 0x134: return new string('2', 1);
                case 0x135: return new string('3', 1);
                case 310: return new string('4', 1);
                case 0x137: return new string('5', 1);
                case 0x138: return new string('6', 1);
                case 0x139: return new string('7', 1);
                case 0x13a: return new string('8', 1);
                case 0x13b: return new string('9', 1);
                case 0x13c: return new string(':', 1);
                case 0x13d: return new string(';', 1);
                case 0x13e: return new string('<', 1);
                case 0x13f: return new string('=', 1);
                case 320: return new string('>', 1);
                case 0x141: return new string('?', 1);
                case 0x142: return new string('@', 1);
                case 0x143: return new string('A', 1);
                case 0x144: return new string('B', 1);
                case 0x145: return new string('C', 1);
                case 0x146: return new string('D', 1);
                case 0x147: return new string('E', 1);
                case 0x148: return new string('F', 1);
                case 0x149: return new string('G', 1);
                case 330: return new string('H', 1);
                case 0x14b: return new string('I', 1);
                case 0x14c: return new string('J', 1);
                case 0x14d: return new string('K', 1);
                case 0x14e: return new string('L', 1);
                case 0x14f: return new string('M', 1);
                case 0x150: return new string('N', 1);
                case 0x151: return new string('O', 1);
                case 0x152: return new string('P', 1);
                case 0x153: return new string('Q', 1);
                case 340: return new string('R', 1);
                case 0x155: return new string('S', 1);
                case 0x156: return new string('T', 1);
                case 0x157: return new string('U', 1);
                case 0x158: return new string('V', 1);
                case 0x159: return new string('W', 1);
                case 0x15a: return new string('X', 1);
                case 0x15b: return new string('Y', 1);
                case 0x15c: return new string('Z', 1);
                case 0x15d: return new string('[', 1);
                case 350: return new string('\\', 1);
                case 0x15f: return new string(']', 1);
                case 0x160: return new string('^', 1);
                case 0x161: return new string('_', 1);
                case 0x162: return new string('`', 1);
                case 0x163: return new string('a', 1);
                case 0x164: return new string('b', 1);
                case 0x165: return new string('c', 1);
                case 0x166: return new string('d', 1);
                case 0x167: return new string('e', 1);
                case 360: return new string('f', 1);
                case 0x169: return new string('g', 1);
                case 0x16a: return new string('h', 1);
                case 0x16b: return new string('i', 1);
                case 0x16c: return new string('j', 1);
                case 0x16d: return new string('k', 1);
                case 0x16e: return new string('l', 1);
                case 0x16f: return new string('m', 1);
                case 0x170: return new string('n', 1);
                case 0x171: return new string('o', 1);
                case 370: return new string('p', 1);
                case 0x173: return new string('q', 1);
                case 0x174: return new string('r', 1);
                case 0x175: return new string('s', 1);
                case 0x176: return new string('t', 1);
                case 0x177: return new string('u', 1);
                case 0x178: return new string('v', 1);
                case 0x179: return new string('w', 1);
                case 0x17a: return new string('x', 1);
                case 0x17b: return new string('y', 1);
                case 380: return new string('z', 1);
                case 0x17d: return new string('{', 1);
                case 0x17e: return new string('|', 1);
                case 0x17f: return new string('}', 1);
                case 0x180: return new string('~', 1);
                case 0x181: return new string('\x007f', 1);
                case 0x182: return new string('\x0080', 1);
                case 0x183: return new string('\x0081', 1);
                case 0x184: return new string('\x0082', 1);
                case 0x185: return new string('\x0083', 1);
                case 390: return new string('\x0084', 1);
                case 0x187: return new string('\x0085', 1);
                case 0x188: return new string('\x0086', 1);
                case 0x189: return new string('\x0087', 1);
                case 0x18a: return new string('\x0088', 1);
                case 0x18b: return new string('\x0089', 1);
                case 0x18c: return new string('\x008a', 1);
                case 0x18d: return new string('\x008b', 1);
                case 0x18e: return new string('\x008c', 1);
                case 0x18f: return new string('\x008d', 1);
                case 400: return new string('\x008e', 1);
                case 0x191: return new string('\x008f', 1);
                case 0x192: return new string('\x0090', 1);
                case 0x193: return new string('\x0091', 1);
                case 0x194: return new string('\x0092', 1);
                case 0x195: return new string('\x0093', 1);
                case 0x196: return new string('\x0094', 1);
                case 0x197: return new string('\x0095', 1);
                case 0x198: return new string('\x0096', 1);
                case 0x199: return new string('\x0097', 1);
                case 410: return new string('\x0098', 1);
                case 0x19b: return new string('\x0099', 1);
                case 0x19c: return new string('\x009a', 1);
                case 0x19d: return new string('\x009b', 1);
                case 0x19e: return new string('\x009c', 1);
                case 0x19f: return new string('\x009d', 1);
                case 0x1a0: return new string('\x009e', 1);
                case 0x1a1: return new string('\x009f', 1);
                case 0x1a2: return new string('\x00a0', 1);
                case 0x1a3: return new string('\x00a1', 1);
                case 420: return new string('\x00a2', 1);
                case 0x1a5: return new string('\x00a3', 1);
                case 0x1a6: return new string('\x00a4', 1);
                case 0x1a7: return new string('\x00a5', 1);
                case 0x1a8: return new string('\x00a6', 1);
                case 0x1a9: return new string('\x00a7', 1);
                case 0x1aa: return new string('\x00a8', 1);
                case 0x1ab: return new string('\x00a9', 1);
                case 0x1ac: return new string('\x00aa', 1);
                case 0x1ad: return new string('\x00ab', 1);
                case 430: return new string('\x00ac', 1);
                case 0x1af: return new string('\x00ad', 1);
                case 0x1b0: return new string('\x00ae', 1);
                case 0x1b1: return new string('\x00af', 1);
                case 0x1b2: return new string('\x00b0', 1);
                case 0x1b3: return new string('\x00b1', 1);
                case 0x1b4: return new string('\x00b2', 1);
                case 0x1b5: return new string('\x00b3', 1);
                case 0x1b6: return new string('\x00b4', 1);
                case 0x1b7: return new string('\x00b5', 1);
                case 440: return new string('\x00b6', 1);
                case 0x1b9: return new string('\x00b7', 1);
                case 0x1ba: return new string('\x00b8', 1);
                case 0x1bb: return new string('\x00b9', 1);
                case 0x1bc: return new string('\x00ba', 1);
                case 0x1bd: return new string('\x00bb', 1);
                case 0x1be: return new string('\x00bc', 1);
                case 0x1bf: return new string('\x00bd', 1);
                case 0x1c0: return new string('\x00be', 1);
                case 0x1c1: return new string('\x00bf', 1);
                case 450: return new string('\x00c0', 1);
                case 0x1c3: return new string('\x00c1', 1);
                case 0x1c4: return new string('\x00c2', 1);
                case 0x1c5: return new string('\x00c3', 1);
                case 0x1c6: return new string('\x00c4', 1);
                case 0x1c7: return new string('\x00c5', 1);
                case 0x1c8: return new string('\x00c6', 1);
                case 0x1c9: return new string('\x00c7', 1);
                case 0x1ca: return new string('\x00c8', 1);
                case 0x1cb: return new string('\x00c9', 1);
                case 460: return new string('\x00ca', 1);
                case 0x1cd: return new string('\x00cb', 1);
                case 0x1ce: return new string('\x00cc', 1);
                case 0x1cf: return new string('\x00cd', 1);
                case 0x1d0: return new string('\x00ce', 1);
                case 0x1d1: return new string('\x00cf', 1);
                case 0x1d2: return new string('\x00d0', 1);
                case 0x1d3: return new string('\x00d1', 1);
                case 0x1d4: return new string('\x00d2', 1);
                case 0x1d5: return new string('\x00d3', 1);
                case 470: return new string('\x00d4', 1);
                case 0x1d7: return new string('\x00d5', 1);
                case 0x1d8: return new string('\x00d6', 1);
                case 0x1d9: return new string('\x00d7', 1);
                case 0x1da: return new string('\x00d8', 1);
                case 0x1db: return new string('\x00d9', 1);
                case 0x1dc: return new string('\x00da', 1);
                case 0x1dd: return new string('\x00db', 1);
                case 0x1de: return new string('\x00dc', 1);
                case 0x1df: return new string('\x00dd', 1);
                case 480: return new string('\x00de', 1);
                case 0x1e1: return new string('\x00df', 1);
                case 0x1e2: return new string('\x00e0', 1);
                case 0x1e3: return new string('\x00e1', 1);
                case 0x1e4: return new string('\x00e2', 1);
                case 0x1e5: return new string('\x00e3', 1);
                case 0x1e6: return new string('\x00e4', 1);
                case 0x1e7: return new string('\x00e5', 1);
                case 0x1e8: return new string('\x00e6', 1);
                case 0x1e9: return new string('\x00e7', 1);
                case 490: return new string('\x00e8', 1);
                case 0x1eb: return new string('\x00e9', 1);
                case 0x1ec: return new string('\x00ea', 1);
                case 0x1ed: return new string('\x00eb', 1);
                case 0x1ee: return new string('\x00ec', 1);
                case 0x1ef: return new string('\x00ed', 1);
                case 0x1f0: return new string('\x00ee', 1);
                case 0x1f1: return new string('\x00ef', 1);
                case 0x1f2: return new string('\x00f0', 1);
                case 0x1f3: return new string('\x00f1', 1);
                case 500: return new string('\x00f2', 1);
                case 0x1f5: return new string('\x00f3', 1);
                case 0x1f6: return new string('\x00f4', 1);
                case 0x1f7: return new string('\x00f5', 1);
                case 0x1f8: return new string('\x00f6', 1);
                case 0x1f9: return new string('\x00f7', 1);
                case 0x1fa: return new string('\x00f8', 1);
                case 0x1fb: return new string('\x00f9', 1);
                case 0x1fc: return new string('\x00fa', 1);
                case 0x1fd: return new string('\x00fb', 1);
                case 510: return new string('\x00fc', 1);
                case 0x1ff: return new string('\x00fd', 1);
                case 0x200: return new string('\x00fe', 1);
                case 0x201: return new string('\x00ff', 1);
            }
        }
        if (A_0[1] != '#')
        {
            return A_0;
        }
        if (A_0.Length == 3)
        {
            return "&#;";
        }
        try
        {
            char ch1 = (char)((ushort)short.Parse(A_0.Substring(2, A_0.Length - 3)));
            text1 = ch1.ToString();
        }
        catch
        {
            text1 = A_0;
        }
        return text1;
    }
    private string a(params char[] A_0)
    {
        if (this.fg >= this.ff)
        {
            return "";
        }
        int num1 = this.fd.IndexOfAny(A_0, this.fg);
        if (num1 == -1)
        {
            return this.e();
        }
        int num2 = this.fd.IndexOf('<', this.fg, (int)(num1 - this.fg));
        if (num2 != -1)
        {
            num1 = num2;
        }
        string text1 = this.fd.Substring(this.fg, num1 - this.fg);
        this.fg = num1 + 1;
        return text1;
    }
    private void a(XmlElement A_0)
    {
        string text1 = this.b();
        if (text1 != null)
        {
            this.fg += text1.Length;
            A_0.AppendChild(this.fc.CreateProcessingInstruction(text1, this.a('>')));
        }
        else
        {
            A_0.AppendChild(this.fc.CreateTextNode("<?"));
        }
    }
    private void a(XmlElement A_0, string A_1, string A_2)
    {
        A_1 = A_1.ToLower();
        if (A_1.IndexOf(':') == -1)
        {
            A_0.SetAttribute(A_1, A_2);
        }
        else
        {
            char[] chArray1 = new char[1] { ':' };
            string[] textArray1 = A_1.Split(chArray1, 2);
            string text1 = textArray1[0];
            string text2 = textArray1[1];
            if (text1 == "")
            {
                A_0.SetAttribute(text2, A_2);
            }
            else if (text2 == "")
            {
                A_0.SetAttribute(text1, A_2);
            }
            else
            {
                A_0.SetAttribute(text2, A_2);
            }
        }
    }
    private string b()
    {
        int num1 = this.ff;
        if (this.fg >= num1)
        {
            return null;
        }
        char ch1 = this.fd[this.fg];
        if (((ch1 < 'a') || (ch1 > 'z')) && ((ch1 < 'A') || (ch1 > 'Z')))
        {
            return null;
        }
        int num2 = this.fg + 1;
        while (num2 < num1)
        {
            ch1 = this.fd[num2];
            if ((((ch1 < 'a') || (ch1 > 'z')) && ((ch1 < 'A') || (ch1 > 'Z'))) && (((ch1 < '0') || (ch1 > '9')) && (((ch1 != '_') && (ch1 != '-')) && ((ch1 != '.') && (ch1 != ':')))))
            {
                break;
            }
            num2++;
        }
        return this.fd.Substring(this.fg, num2 - this.fg);
    }
    private string b(char A_0)
    {
        if (this.fg >= this.ff)
        {
            return "";
        }
        int num1 = this.fd.IndexOf(A_0, this.fg);
        if (num1 == -1)
        {
            return this.f();
        }
        string text1 = this.fd.Substring(this.fg, num1 - this.fg);
        this.fg = num1 + 1;
        return text1;
    }
    private static string b(string A_0)
    {
        if (A_0 == null)
        {
            return null;
        }
        if (A_0 == "")
        {
            return "";
        }
        StringBuilder builder1 = new StringBuilder();
        int num1 = A_0.Length;
        int num2 = 0;
        for (int num3 = A_0.IndexOf('&'); num3 != -1; num3 = A_0.IndexOf('&', num2))
        {
            int num4 = A_0.IndexOfAny(SafeHtmlParser.fh, num3 + 1);
            if (num4 == -1)
            {
                break;
            }
            if (A_0[num4] == '&')
            {
                builder1.Append(A_0, num2, (int)(num4 - num2));
                num3 = num4;
                continue;
            }
            if (num2 != num3)
            {
                builder1.Append(A_0, num2, (int)(num3 - num2));
            }
            builder1.Append(SafeHtmlParser.a(A_0.Substring(num3, (num4 + 1) - num3)));
            num2 = num4 + 1;
            if (num2 >= num1)
            {
                break;
            }
        }
        if (num2 < num1)
        {
            builder1.Append(A_0, num2, (int)(num1 - num2));
        }
        return builder1.ToString();
    }
    private void b(XmlElement A_0)
    {
        if (string.Compare(this.fd, this.fg, "[CDATA[", 0, 7, true) == 0)
        {
            this.fg += 7;
            A_0.AppendChild(this.fc.CreateCDataSection(this.c("]]>")));
        }
        else if (string.Compare(this.fd, this.fg, "--", 0, "--".Length, true) == 0)
        {
            this.fg += 2;
            string text1 = this.c("-->");
            if (text1.IndexOf("--") != -1)
            {
                text1 = text1.Replace("--", "- -");
            }
            if (text1.StartsWith("-") || text1.EndsWith("-"))
            {
                text1 = " " + text1 + " ";
            }
            A_0.AppendChild(this.fc.CreateComment(text1));
        }
        else
        {
            string text2 = this.b();
            if (text2 != null)
            {
                this.fg += text2.Length;
                try
                {
                    A_0.AppendChild(this.fc.CreateDocumentType(text2, null, null, this.a('>')));
                }
                catch
                {
                }
            }
            else
            {
                A_0.AppendChild(this.fc.CreateTextNode("<!"));
            }
        }
    }
    private string c()
    {
        int num1 = this.ff;
        int num2 = this.fg;
        while (num2 < num1)
        {
            char ch1 = this.fd[num2];
            if (!char.IsWhiteSpace(ch1))
            {
                break;
            }
            num2++;
        }
        if (num2 == this.fg)
        {
            return null;
        }
        return this.fd.Substring(this.fg, num2 - this.fg);
    }
    private string c(string A_0)
    {
        if (this.fg >= this.ff)
        {
            return "";
        }
        int num1 = this.fd.IndexOf(A_0, this.fg);
        if (num1 == -1)
        {
            return this.e();
        }
        int num2 = this.fd.IndexOf('<', this.fg, (int)(num1 - this.fg));
        if (num2 != -1)
        {
            num1 = num2;
        }
        string text1 = this.fd.Substring(this.fg, num1 - this.fg);
        this.fg = num1 + A_0.Length;
        return text1;
    }
    private void c(XmlElement A_0)
    {
        while (this.fg < this.ff)
        {
            this.d();
            string text1 = this.b();
            if (text1 == null)
            {
                return;
            }
            this.fg += text1.Length;
            this.d();
            if (this.fg >= this.ff)
            {
                this.a(A_0, text1, text1);
                return;
            }
            if (this.fd[this.fg] == '=')
            {
                string text2;
                this.fg++;
                this.d();
                if (this.fg >= this.ff)
                {
                    this.a(A_0, text1, text1);
                    return;
                }
                char ch1 = this.fd[this.fg];
                if ((ch1 == '"') || (ch1 == '\''))
                {
                    this.fg++;
                    char[] chArray1 = new char[2] { '>', ch1 };
                    text2 = this.a(chArray1);
                    if ((this.fg < this.ff) && (this.fd[this.fg] == ch1))
                    {
                        this.fg++;
                    }
                }
                else if (ch1 == '>')
                {
                    text2 = null;
                }
                else
                {
                    text2 = this.g();
                }
                if (text2 == null)
                {
                    text2 = "";
                }
                else
                {
                    text2 = SafeHtmlParser.b(text2);
                }
                this.a(A_0, text1, text2);
            }
        }
    }
    private string d()
    {
        string text1 = this.c();
        if (text1 == null)
        {
            return null;
        }
        this.fg += text1.Length;
        return text1;
    }
    private void d(string A_0)
    {
        if (((this.fg + 1) <= this.ff) && ((this.fd[this.fg] == '<') && (this.fd[this.fg + 1] == '/')))
        {
            int num1 = this.fg;
            this.fg += 2;
            string text1 = this.b();
            if (text1 != null)
            {
                this.fg += text1.Length;
                this.d();
                if ((this.fg < this.ff) && (this.fd[this.fg] == '>'))
                {
                    this.fg++;
                }
                if (string.Compare(text1, A_0, true) == 0)
                {
                    return;
                }
            }
            this.fg = num1;
        }
    }
    private void d(XmlElement A_0)
    {
        int num1 = this.ff;
        if (this.fg < num1)
        {
            char ch1 = this.fd[this.fg];
            if (ch1 == '!')
            {
                this.fg++;
                this.b(A_0);
                return;
            }
            if (ch1 == '?')
            {
                this.fg++;
                this.a(A_0);
                return;
            }
            string text1 = this.b();
            if (text1 != null)
            {
                this.fg += text1.Length;
                XmlElement element1 = this.e(text1);
                string text2 = element1.LocalName.ToUpper();
                A_0.AppendChild(element1);
                this.c(element1);
                if (this.fg < num1)
                {
                    ch1 = this.fd[this.fg];
                    if (ch1 == '/')
                    {
                        this.fg++;
                        if ((this.fg < num1) && (this.fd[this.fg] == '>'))
                        {
                            this.fg++;
                        }
                        return;
                    }
                    if (ch1 == '>')
                    {
                        this.fg++;
                    }
                    if (this.fg >= num1)
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
                        int num2 = this.fe.IndexOf("</script", this.fg);
                        if (num2 == -1)
                        {
                            text3 = this.f();
                        }
                        else if (num2 == this.fg)
                        {
                            text3 = "";
                        }
                        else
                        {
                            text3 = this.fd.Substring(this.fg, num2 - this.fg);
                            this.fg = num2;
                        }
                        text3 = text3.Replace("/*<![CDATA[*/", "").Replace("/*]]>*/", "").Trim().Replace("]]>", "]]&gt;");
                        element1.AppendChild(this.fc.CreateTextNode("/*"));
                        element1.AppendChild(this.fc.CreateCDataSection("*/\r\n" + text3 + "\r\n/*"));
                        element1.AppendChild(this.fc.CreateTextNode("*/"));
                    }
                    else
                    {
                        this.e(element1);
                        if ((",IFRAME,A,P,DIV,OBJECT,ADDRESS,BIG,BLOCKQUOTE,B,CAPTION,CENTER,CITE,CODE,\r\n\t\t\t\t\t\t\t\t\t,DD,DFN,DIR,DL,DT,EM,FONT,FORM,H1,H2,H3,H4,H5,H6,HEAD,HTML,I,LI,MAP,MENU,OL,OPTION,\r\n\t\t\t\t\t\t\t\t\t,PRE,SELECT,SMALL,STRIKE,STRONG,SUB,SUP,TABLE,TD,TEXTAREA,TH,TITLE,TR,TT,U,".IndexOf("," + text2 + ",") != -1) && (element1.ChildNodes.Count == 0))
                        {
                            element1.AppendChild(this.fc.CreateTextNode(""));
                        }
                    }
                    this.d(text1);
                }
                return;
            }
        }
        A_0.AppendChild(this.fc.CreateTextNode("<"));
    }
    private string e()
    {
        if (this.fg >= this.ff)
        {
            return "";
        }
        if (this.fd[this.fg] == '<')
        {
            return "";
        }
        int num1 = this.fd.IndexOf('<', this.fg);
        if (num1 != -1)
        {
            string text1 = this.fd.Substring(this.fg, num1 - this.fg);
            this.fg = num1;
            return text1;
        }
        string text2 = this.fd.Substring(this.fg);
        this.fg = this.ff;
        return text2;
    }
    private XmlElement e(string A_0)
    {
        XmlElement element1;
        A_0 = A_0.ToLower();
        try
        {
            if (A_0.IndexOf(":") == -1)
            {
                return this.fc.CreateElement(A_0);
            }
            char[] chArray1 = new char[1] { ':' };
            string[] textArray1 = A_0.Split(chArray1, 2);
            string text1 = textArray1[0];
            string text2 = textArray1[1];
            if (text2.IndexOf(":") != -1)
            {
                text2 = text2.Replace(':', '-');
            }
            if (text2 == "")
            {
                this.fc.CreateElement(text1);
            }
            if (text1 == "")
            {
                return this.fc.CreateElement(text2);
            }
            element1 = this.fc.CreateElement(text1, text2, "http://unknownprefix/" + text1);
        }
        catch
        {
            element1 = this.fc.CreateElement("error");
        }
        return element1;
    }
    private void e(XmlElement A_0)
    {
        int num1 = this.ff;
        while (this.fg < num1)
        {
            char ch1 = this.fd[this.fg];
            if (ch1 == '<')
            {
                if (((this.fg + 1) < num1) && (this.fd[this.fg + 1] == '/'))
                {
                    return;
                }
                this.fg++;
                this.d(A_0);
                continue;
            }
            string text1 = this.d();
            string text2 = this.a();
            if (text2 == null)
            {
                if (text1 == null)
                {
                    this.fg++;
                }
                continue;
            }
            this.fg += text2.Length;
            A_0.AppendChild(this.fc.CreateTextNode(text1 + SafeHtmlParser.b(text2)));
        }
    }
    private string f()
    {
        if (this.fg < this.ff)
        {
            string text1 = this.fd.Substring(this.fg);
            this.fg = this.ff;
            return text1;
        }
        return "";
    }
    private string g()
    {
        int num1 = this.fg;
        while (this.fg < this.ff)
        {
            char ch1 = this.fd[this.fg];
            if ((ch1 == '>') || char.IsWhiteSpace(ch1))
            {
                break;
            }
            this.fg++;
        }
        if (num1 == this.fg)
        {
            return null;
        }
        return this.fd.Substring(num1, this.fg - num1);
    }
    private void h()
    {
        if (((this.fg + 1) <= this.ff) && ((this.fd[this.fg] == '<') && (this.fd[this.fg + 1] == '/')))
        {
            this.fg += 2;
            string text1 = this.b();
            if (text1 != null)
            {
                this.fg += text1.Length;
                this.d();
                if ((this.fg < this.ff) && (this.fd[this.fg] == '>'))
                {
                    this.fg++;
                }
            }
        }
    }
    public static void ParseHtml(XmlElement element, string xmlstring)
    {
        if (element == null)
        {
            throw new ArgumentNullException("element");
        }
        if (xmlstring == null)
        {
            throw new ArgumentNullException("xmlstring");
        }
        SafeHtmlParser parser1 = new SafeHtmlParser();
        parser1.fc = element.OwnerDocument;
        parser1.fd = xmlstring;
        parser1.fe = xmlstring.ToLower();
        parser1.ff = xmlstring.Length;
        parser1.fg = 0;
        while (true)
        {
            parser1.e(element);
            if (parser1.fg >= parser1.ff)
            {
                return;
            }
            parser1.h();
        }
    }
    // Fields
    private const string fa = ",LINK,META,BASE,BGSOUND,BR,HR,INPUT,PARAM,IMG,AREA,";
    private const string fb = ",IFRAME,A,P,DIV,OBJECT,ADDRESS,BIG,BLOCKQUOTE,B,CAPTION,CENTER,CITE,CODE,\r\n\t\t\t\t\t\t\t\t\t,DD,DFN,DIR,DL,DT,EM,FONT,FORM,H1,H2,H3,H4,H5,H6,HEAD,HTML,I,LI,MAP,MENU,OL,OPTION,\r\n\t\t\t\t\t\t\t\t\t,PRE,SELECT,SMALL,STRIKE,STRONG,SUB,SUP,TABLE,TD,TEXTAREA,TH,TITLE,TR,TT,U,";
    private XmlDocument fc;
    private string fd;
    private string fe;
    private int ff;
    private int fg;
    private static char[] fh;
}