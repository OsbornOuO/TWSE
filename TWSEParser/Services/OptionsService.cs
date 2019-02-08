using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;

namespace TWSEParser.Services
{
    public static class OptionsService
    {
        private static readonly HttpClient client = new HttpClient();
        public static string Start(this Model.Options options)
        {
            ParserService ps = new ParserService();
            switch (options.Sources)
            {
                case Model.Sources.FutDailyMarketView:
                    break;
                case Model.Sources.FutDailyMarketViewOptions:
                    options.Response = FutDailyMarketViewOptionsAsync(options.QueryStartDate, options.QueryEndDate);
                    break;
                case Model.Sources.FutPrevious30DaysSalesData:
                    ParserFutPrevious30DaysSalesData(options.QueryStartDate, options.QueryEndDate);
                    break;
                case Model.Sources.FutPrevious30DaysSpreadOrdersReport:
                    FutPrevious30DaysSpreadOrdersReport(options.QueryStartDate, options.QueryEndDate);
                    break;
                case Model.Sources.FutPrevious30DaysSpreadSalesData:
                    FutPrevious30DaysSpreadSalesData(options.QueryStartDate, options.QueryEndDate);
                    break;
                default:
                    break;
            }
            return "";
        }
        private static void ParserFutPrevious30DaysSalesData(DateTime startAt, DateTime endAt)
        {
            var ps = new ParserService();
            var htmlDoc = ps.GetSourceHTML(@"http://www.taifex.com.tw/cht/3/dlFutPrevious30DaysSalesData");
            ps.GetFileName(htmlDoc.DocumentNode.SelectNodes("//td/input"), startAt, endAt);
        }
        private static void FutPrevious30DaysSpreadOrdersReport(DateTime startAt, DateTime endAt)
        {
            var ps = new ParserService();
            var htmlDoc = ps.GetSourceHTML(@"http://www.taifex.com.tw/cht/3/dlFutPrevious30DaysSpreadOrdersReport");
            ps.GetFileName(htmlDoc.DocumentNode.SelectNodes("//td/input"), startAt, endAt);
        }
        private static void FutPrevious30DaysSpreadSalesData(DateTime startAt, DateTime endAt)
        {
            var ps = new ParserService();
            var htmlDoc = ps.GetSourceHTML(@"http://www.taifex.com.tw/cht/3/dlFutPrevious30DaysSpreadSalesData");
            ps.GetFileName(htmlDoc.DocumentNode.SelectNodes("//td/input"), startAt, endAt);
        }
        private static object FutDailyMarketViewOptionsAsync(DateTime startAt, DateTime endAt)
        {
            var url = @"http://www.taifex.com.tw/cht/3/getFutcontractDl.do?";
            url += "queryStartDate=" + startAt.ToString("yyyy/MM/dd") + "&queryEndDate=" + endAt.ToString("yyyy/MM/dd");
            Console.WriteLine(url);
            WebRequest request = WebRequest.Create(url);
            request.Method = "GET";
            using (var httpResponse = (HttpWebResponse)request.GetResponse())
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream(), Encoding.UTF8))
            {
                return JsonConvert.DeserializeObject(streamReader.ReadToEnd());
            }
        }
    }
}
