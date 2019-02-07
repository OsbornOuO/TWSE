using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace TWSEParser.Services
{
    public class ParserService
    {
        public HtmlDocument GetSourceHTML(string URL)
        {
            HtmlWeb web = new HtmlWeb();
            return web.Load(URL);
        }
        public void DownloadFileByURL(string URL, string fileName)
        {
            using (var client = new WebClient())
            {
                client.DownloadFile(URL, fileName);
            }
        }
        public void GetFileName(string filename,DateTime time)
        {
            if (time != null){

            }
        }
    }
}
