using System;
using System.Text.RegularExpressions;

namespace Reader.Workers
{
    public class HtmlToTextWorker
    {
        public static string ConvertHtmlToText(string text)
        {
            text = text.Replace("&nbsp;", " ");
            text = text.Replace("</p>", Environment.NewLine);
            text = text.Replace("</div>", Environment.NewLine);
            text = text.Replace("&#39;", "'");
            text = text.Replace("&amp;", "&");
            text = text.Replace("&lt;", "<");
            text = text.Replace("&lt;>", ">");
            text = text.Replace("</li>", Environment.NewLine);
            text = text.Replace("<br>", Environment.NewLine);
            text = text.Replace("<br/>", Environment.NewLine);
            text = text.Replace("<br/ >", Environment.NewLine);
            text = text.Replace("&#43;","+");
            text = text.Replace("&quot;", "\"");


            return Regex.Replace(text, @"<[^>]+>", ""); 



        }
    }
}
