using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using TWSEParser.Store;

namespace TWSEParser.Services
{
    public static class OptionsService
    {
        private static Guid guid;
        public static void Start(this Model.Options options)
        {
            string postData;
            var querystr = Config.ParserData[options.Sources].QueryString;
            switch (options.Sources)
            {
                case Model.Sources.FutDailyMarketView:
                    postData = string.Format(querystr, options.Commodity, options.QueryStartDate.ToString("yyyy/MM/dd"), options.QueryEndDate.ToString("yyyy/MM/dd"));
                    options.Response = HttpReqToDownLoadFile(options.Sources, postData);
                    break;
                case Model.Sources.FutDailyMarketViewOptions:
                    postData = string.Format(querystr, options.QueryStartDate.ToString("yyyy/MM/dd"), options.QueryEndDate.ToString("yyyy/MM/dd"));
                    options.Response = HttpReqToDownLoadFile(options.Sources, postData);
                    break;
                case Model.Sources.FutPrevious30DaysSalesData:
                case Model.Sources.FutPrevious30DaysSpreadOrdersReport:
                case Model.Sources.FutPrevious30DaysSpreadSalesData:
                    options.Response = DownLoadFile(options.Sources, options.QueryStartDate, options.QueryEndDate);
                    break;
                case Model.Sources.StockDayAVG:
                    var date = new DateTime(options.QueryStartDate.Year, options.QueryStartDate.Month, 1);
                    postData = string.Format(querystr, date.ToString("yyyymmdd"), options.StockNo);
                    options.Response = HttpReqToDownLoadFile(options.Sources, postData);
                    break;
                case Model.Sources.DailyFXRateDown:
                    postData = String.Format(querystr, options.QueryStartDate.ToString("yyyy/MM/dd"), options.QueryStartDate.ToString("yyyy/MM/dd"));
                    options.Response = HttpReqToDownLoadFile(options.Sources, postData);
                    break;
                default:
                    options.Response = false;
                    break;
            }
        }
        private static List<string> DownLoadFile(Model.Sources sources,DateTime startAt,DateTime endAt)
        {
            var ps = new ParserService();
            var htmlDoc = ps.GetSourceHTML(Config.ParserData[sources].URL);
            return ps.GetFileName(htmlDoc.DocumentNode.SelectNodes("//td/input"), startAt, endAt);
        }
        private static object HttpReqToDownLoadFile(Model.Sources sources,string postData)
        {
            var ps = new ParserService();
            ps.GenerateHttpRequest(Config.ParserData[sources].URL, Config.ParserData[sources].Method, postData);

            switch (sources)
            {
                case Model.Sources.FutDailyMarketView:
                    return ps.storeResponseToCSV();
                case Model.Sources.FutDailyMarketViewOptions:
                    return ps.GetResponse();
                case Model.Sources.FutPrevious30DaysSalesData:
                case Model.Sources.FutPrevious30DaysSpreadOrdersReport:
                case Model.Sources.FutPrevious30DaysSpreadSalesData:
                case Model.Sources.StockDayAVG:
                case Model.Sources.DailyFXRateDown:
                    return ps.storeResponseToCSV();
                default:
                    return null;
            }
        }
        //private static object FutDailyMarketViewOptions(DateTime startAt, DateTime endAt)
        //{
        //    var url = @"http://www.taifex.com.tw/cht/3/getFutcontractDl.do?";
        //    url += String.Format(@"queryStartDate={0}&queryEndDate={1}", startAt.ToString("yyyy /MM/dd"), endAt.ToString("yyyy/MM/dd"));
        //    WebRequest request = WebRequest.Create(url);
        //    request.Method = "GET";
        //    using (var httpResponse = (HttpWebResponse)request.GetResponse())
        //    using (var streamReader = new StreamReader(httpResponse.GetResponseStream(), Encoding.UTF8))
        //    {
        //        return JsonConvert.DeserializeObject(streamReader.ReadToEnd());
        //    }
        //}
        //private static object FutDailyMarketView(Model.Options options)
        //{
        //    var ps = new ParserService();
        //    var url = @"http://www.taifex.com.tw/cht/3/dlFutDataDown";
        //    string postData = String.Format("down_type=1&commodity_id={0}&queryStartDate={1}&queryEndDate={2}", options.Commodity, options.QueryStartDate, options.QueryEndDate);

        //    ps.GenerateHttpRequest(url,"POST",postData);
        //    return ps.storeResponseToCSV();
        //}
        //private static object StockDayAVG(DateTime startAt, int stockNo)
        //{
        //    var ps = new ParserService();

        //    var url = @"http://www.twse.com.tw/exchangeReport/STOCK_DAY_AVG?response=csv&";
        //    var date = new DateTime(startAt.Year, startAt.Month, 1);
        //    var postData = "date=" + date.ToString("yyyyMMdd") + "&stockNo=" + stockNo;
        //    ps.GenerateHttpRequest(url, "GET", postData);
        //    return ps.storeResponseToCSV();
        //}
        //private static object DailyFXRateDown(DateTime startAt, DateTime endAt)
        //{
        //    var ps = new ParserService();

        //    var url = @"https://www.taifex.com.tw/cht/3/dailyFXRateDown";
        //    string postData = String.Format("queryStartDate={0}&queryEndDate={1}", startAt.ToString("yyyy/MM/dd"), endAt.ToString("yyyy/MM/dd"));
        //    ps.GenerateHttpRequest(url,"POST",postData);
        //    return ps.storeResponseToCSV();
        //}
    }
}
