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
        private static Guid guid;
        public static void Start(this Model.Options options)
        {
            switch (options.Sources)
            {
                case Model.Sources.FutDailyMarketView:
                    options.Response = FutDailyMarketView(options);
                    break;
                case Model.Sources.FutDailyMarketViewOptions:
                    options.Response = FutDailyMarketViewOptions(options.QueryStartDate, options.QueryEndDate);
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
                case Model.Sources.StockDayAVG:
                    options.Response = StockDayAVG(options.QueryStartDate, options.StockNo);
                    break;
                case Model.Sources.DailyFXRateDown:
                    options.Response = DailyFXRateDown(options.QueryStartDate, options.QueryEndDate);
                    break;
                default:
                    break;
            }
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
        private static object FutDailyMarketViewOptions(DateTime startAt, DateTime endAt)
        {
            var url = @"http://www.taifex.com.tw/cht/3/getFutcontractDl.do?";
            url += "queryStartDate=" + startAt.ToString("yyyy/MM/dd") + "&queryEndDate=" + endAt.ToString("yyyy/MM/dd");
            WebRequest request = WebRequest.Create(url);
            request.Method = "GET";
            using (var httpResponse = (HttpWebResponse)request.GetResponse())
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream(), Encoding.UTF8))
            {
                return JsonConvert.DeserializeObject(streamReader.ReadToEnd());
            }
        }
        private static object FutDailyMarketView(Model.Options options)
        {
            var url = @"http://www.taifex.com.tw/cht/3/dlFutDataDown";
            string postData = String.Format("down_type=1&commodity_id={0}&queryStartDate={1}&queryEndDate={2}", options.Commodity, options.QueryStartDate, options.QueryEndDate);
            var request = GenerateHttpRequest(url,"POST",postData);
            return storeResponseToCSV(request);
        }
        private static object StockDayAVG(DateTime startAt, int stockNo)
        {
            var url = @"http://www.twse.com.tw/exchangeReport/STOCK_DAY_AVG?response=csv&";
            var date = new DateTime(startAt.Year, startAt.Month, 1);
            url += "date=" + date.ToString("yyyyMMdd") + "&stockNo=" + stockNo;
            WebRequest request = WebRequest.Create(url);
            request.Method = "GET";
            return storeResponseToCSV(request);
        }
        private static object DailyFXRateDown(DateTime startAt, DateTime endAt)
        {
            var url = @"https://www.taifex.com.tw/cht/3/dailyFXRateDown";
            string postData = String.Format("queryStartDate={0}&queryEndDate={1}", startAt.ToString("yyyy/MM/dd"), endAt.ToString("yyyy/MM/dd"));
            var request = GenerateHttpRequest(url,"POST",postData);
            return storeResponseToCSV(request);
        }
        private static WebRequest GenerateHttpRequest(string url,string type ,string querystr)
        {
            WebRequest request = WebRequest.Create(url);
            request.Method = type;
            request.ContentType = "application/x-www-form-urlencoded";
            string postData = querystr;
            var data = Encoding.ASCII.GetBytes(postData);
            request.ContentLength = data.Length;
            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, postData.Length);
            }
            return request;
        }
        private static string storeResponseToCSV(WebRequest request)
        {
            guid = Guid.NewGuid();
            using (var resp = (HttpWebResponse)request.GetResponse())
            using (var output = File.OpenWrite(guid.ToString() + ".csv"))
            using (var input = resp.GetResponseStream())
            {
                input.CopyTo(output);
            }
            return guid.ToString() + ".csv";
        }
    }
}
