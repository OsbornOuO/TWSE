using System;

namespace TWSEParser.Model
{
    public class Options
    {
        //資料來源
        public Sources Sources { get; set; }

        //契約
        public string Commodity { get; set; }

        //起始日期
        public DateTime QueryStartDate { get; set; }
        
        //結束日期
        public DateTime QueryEndDate { get; set; }

        public object Response { get; set; }
    }
    public enum Sources :int {
        FutDailyMarketView,
        FutDailyMarketViewOptions,
        FutPrevious30DaysSalesData,
        FutPrevious30DaysSpreadOrdersReport,
        FutPrevious30DaysSpreadSalesData
    }

}
