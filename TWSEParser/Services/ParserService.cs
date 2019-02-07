using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace TWSEParser.Services
{
    public class ParserService
    {
        private const string patternFileURL = @"http://.*CSV.*([_0-9]{8,8}[0-9]{2,2})[A-Z_]*.zip";

        public HtmlDocument GetSourceHTML(string URL)
        {
            HtmlWeb web = new HtmlWeb();
            return web.Load(URL);
        }
        private void DownloadFileByURL(string URL, string fileName)
        {
            using (var client = new WebClient())
            {
                client.DownloadFile(URL, fileName);
            }
        }
        public void GetFileName(HtmlNodeCollection nodes,DateTime startAt,DateTime endAt)
        {
            foreach (var item in nodes)
            {
                var filename = item.Attributes["onClick"];
                if (filename == null)
                    continue;
                Match m = Regex.Match(item.Attributes["onClick"].Value, patternFileURL, RegexOptions.IgnoreCase);
                if (m.Success)
                {
                    var filedate = m.Groups[1].ToString();
                    if(filedate.Length == 11)
                    {
                        filedate = filedate.Substring(0, filedate.Length - 1);
                    }
                    IFormatProvider culture = new System.Globalization.CultureInfo("zh-TW", true);
                    DateTime parsedDate = DateTime.ParseExact(filedate, "yyyy_MM_dd", culture);
                    var s = m.Value.Split('/');

                    if (startAt == DateTime.MinValue && endAt == DateTime.MinValue)
                    {
                        DownloadFileByURL(m.Value, s[s.Length - 1]);
                        return;
                    }
                    else if (startAt != DateTime.MinValue && DateTime.Compare(parsedDate, startAt) < 0)
                        continue;
                    else if (endAt != DateTime.MinValue && DateTime.Compare(parsedDate, endAt) > 0)
                        continue;
                    DownloadFileByURL(m.Value, s[s.Length - 1]);
                }
                else
                    continue;
            }
        }
    }
}
