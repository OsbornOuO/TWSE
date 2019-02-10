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
                case Model.Sources.StockDayAVG:
                    var date = new DateTime(options.QueryStartDate.Year, options.QueryStartDate.Month, 1);
                    postData = string.Format(querystr, date.ToString("yyyymmdd"), options.StockNo);
                    options.Response = HttpReqToDownLoadFile(options.Sources, postData);
                    break;
                case Model.Sources.FutPrevious30DaysSalesData:
                case Model.Sources.FutPrevious30DaysSpreadOrdersReport:
                case Model.Sources.FutPrevious30DaysSpreadSalesData:
                    options.Response = DownLoadFile(options.Sources, options.QueryStartDate, options.QueryEndDate);
                    break;
                case Model.Sources.FutDailyMarketViewOptions:
                case Model.Sources.DailyFXRateDown:
                case Model.Sources.LargeTraderFutDown:
                case Model.Sources.LargeTraderOptView:
                case Model.Sources.TotalTableDateView:
                case Model.Sources.FutAndOptDateView:
                case Model.Sources.FutContractsDateView:
                case Model.Sources.OptContractsDateView:
                case Model.Sources.CallsAndPutsDateView:
                    postData = string.Format(querystr, options.QueryStartDate.ToString("yyyy/MM/dd"), options.QueryEndDate.ToString("yyyy/MM/dd"));
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
                case Model.Sources.FutDailyMarketViewOptions:
                    return ps.GetResponse();
                case Model.Sources.FutDailyMarketView:
                case Model.Sources.FutPrevious30DaysSalesData:
                case Model.Sources.FutPrevious30DaysSpreadOrdersReport:
                case Model.Sources.FutPrevious30DaysSpreadSalesData:
                case Model.Sources.StockDayAVG:
                case Model.Sources.DailyFXRateDown:
                case Model.Sources.LargeTraderFutDown:
                case Model.Sources.LargeTraderOptView:
                case Model.Sources.TotalTableDateView:
                case Model.Sources.FutAndOptDateView:
                case Model.Sources.FutContractsDateView:
                case Model.Sources.OptContractsDateView:
                case Model.Sources.CallsAndPutsDateView:
                    return ps.storeResponseToCSV();
                default:
                    return null;
            }
        }
    }
}
