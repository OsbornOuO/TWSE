using System;
using System.Collections.Generic;
using System.Text;

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
                    ps.DownloadFileByURL(@"http://www.taifex.com.tw/file/taifex/Dailydownload/DailydownloadCSV/Daily_","");
                    break;
                case Model.Sources.FutPrevious30DaysSpreadOrdersReport:
                    break;
                case Model.Sources.FutPrevious30DaysSpreadSalesData:
                    break;
                default:
                    return false;
            }
            return false;
        }
    }
}
