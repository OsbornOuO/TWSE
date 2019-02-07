using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace TWSEParser.Services
{
    public static class OptionsService
    {
        public static bool Start(this Model.Options options)
        {
            ParserService ps = new ParserService();
            switch (options.Sources)
            {
                case Model.Sources.FutDailyMarketView:
                    break;
                case Model.Sources.FutPrevious30DaysSalesData:
                    ParserFutPrevious30DaysSalesData(options.QueryStartDate,options.QueryEndDate);
                    break;
                case Model.Sources.FutPrevious30DaysSpreadOrdersReport:
                    FutPrevious30DaysSpreadOrdersReport(options.QueryStartDate, options.QueryEndDate);
                    break;
                case Model.Sources.FutPrevious30DaysSpreadSalesData:
                    FutPrevious30DaysSpreadSalesData(options.QueryStartDate, options.QueryEndDate);
                    break;
                default:
                    return false;
            }
            return false;
        }
        private static void ParserFutPrevious30DaysSalesData(DateTime startAt,DateTime endAt)
        {
            var ps =new  ParserService();
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
    }
}
