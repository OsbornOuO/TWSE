using HtmlAgilityPack;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace TWSEParser.Services
{
    public class ParserService
    {
        private static Guid guid;

        private WebRequest webRequest;

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
        public List<string> GetFileName(HtmlNodeCollection nodes,DateTime startAt,DateTime endAt)
        {
            var files = new List<string>();
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
                        files.Add(s[s.Length - 1]);
                        return files;
                    }
                    else if (startAt != DateTime.MinValue && DateTime.Compare(parsedDate, startAt) < 0)
                        continue;
                    else if (endAt != DateTime.MinValue && DateTime.Compare(parsedDate, endAt) > 0)
                        continue;
                    files.Add(s[s.Length - 1]);
                    DownloadFileByURL(m.Value, s[s.Length - 1]);
                }
                else
                    continue;
            }
            return files;
        }
        public void GenerateHttpRequest(string url, string type, string querystr)
        {
            switch (type)
            {
                case "GET":
                    WebRequest request = WebRequest.Create(url+querystr);
                    Console.WriteLine(url + querystr);
                    request.Method = type;
                    webRequest = request;
                    return;
                case "POST":
                    request = WebRequest.Create(url);
                    request.Method = type;
                    request.ContentType = "application/x-www-form-urlencoded";
                    string postData = querystr;
                    var data = Encoding.ASCII.GetBytes(querystr);
                    request.ContentLength = data.Length;
                    using (var stream = request.GetRequestStream())
                    {
                        stream.Write(data, 0, postData.Length);
                    }
                    webRequest = request;
                    return;
                default:
                    break;
            }
        }
        public string storeResponseToCSV()
        {
            guid = Guid.NewGuid();
            using (var resp = (HttpWebResponse)webRequest.GetResponse())
            using (var output = File.OpenWrite(guid.ToString() + ".csv"))
            using (var input = resp.GetResponseStream())
            {
                var disposition = resp.Headers.AllKeys.Where(k => k == "Content-Disposition").FirstOrDefault();
                if(disposition == null)
                {
                    return null;
                }         
                input.CopyTo(output);
            }
            return guid.ToString() + ".csv";
        }
        public object GetResponse()
        {
            using (var resp = (HttpWebResponse)webRequest.GetResponse())
            using (var streamReader = new StreamReader(resp.GetResponseStream(), Encoding.UTF8))
            {
                if (resp.ContentType.Contains("json"))
                {
                    return JsonConvert.DeserializeObject(streamReader.ReadToEnd());
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
