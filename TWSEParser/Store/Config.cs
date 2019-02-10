using System;
using System.Collections.Generic;
using System.Text;

namespace TWSEParser.Store
{
    public static class Config
    {
        public static Dictionary<Model.Sources, ParserDetail> ParserData;
        static Config()
        {
            ParserData = new Dictionary<Model.Sources, ParserDetail>() {
                {
                    Model.Sources.FutDailyMarketView,
                    new ParserDetail{
                        URL =  @"http://www.taifex.com.tw/cht/3/dlFutDataDown",
                        QueryString = @"down_type=1&commodity_id={0}&queryStartDate={1}&queryEndDate={2}",
                        Method=@"POST"
                    }
                },
                {
                    Model.Sources.FutDailyMarketViewOptions,
                    new ParserDetail{
                        URL = @"http://www.taifex.com.tw/cht/3/getFutcontractDl.do?",
                        QueryString = @"queryStartDate={0}&queryEndDate={1}",
                        Method =@"GET"
                    }
                },
                {
                    Model.Sources.FutPrevious30DaysSalesData,
                    new ParserDetail{
                        URL = @"http://www.taifex.com.tw/cht/3/dlFutPrevious30DaysSalesData",
                        Method =@"GET"
                    }
                },
                {
                    Model.Sources.FutPrevious30DaysSpreadOrdersReport,
                    new ParserDetail{
                        URL = @"http://www.taifex.com.tw/cht/3/dlFutPrevious30DaysSpreadOrdersReport",
                        Method =@"GET"
                    }
                },
                {
                    Model.Sources.FutPrevious30DaysSpreadSalesData,
                    new ParserDetail{
                        URL = @"http://www.taifex.com.tw/cht/3/dlFutPrevious30DaysSpreadSalesData",
                        Method =@"GET"
                    }
                },
                {
                    Model.Sources.StockDayAVG,
                    new ParserDetail{
                        URL = @"http://www.twse.com.tw/exchangeReport/STOCK_DAY_AVG?response=csv&",
                        QueryString = @"date={0}&stockNo={1}",
                        Method =@"GET"
                    }
                },
                {
                    Model.Sources.DailyFXRateDown,
                    new ParserDetail{
                        URL = @"https://www.taifex.com.tw/cht/3/dailyFXRateDown",
                        QueryString = @"queryStartDate={0}&queryEndDate={1}",
                        Method = "POST"
                    }
                },
                {
                    Model.Sources.LargeTraderFutDown,
                    new ParserDetail
                    {
                        URL =@"https://www.taifex.com.tw/cht/3/largeTraderFutDown",
                        QueryString =@"queryStartDate={0}&queryEndDate={1}",
                        Method = "POST"
                    }
                },
                {
                    Model.Sources.LargeTraderOptView,
                    new ParserDetail
                    {
                        URL = @"https://www.taifex.com.tw/cht/3/largeTraderOptView",
                        QueryString = @"queryStartDate={0}&queryEndDate={1}",
                        Method = "POST",
                    }
                },
                {
                    Model.Sources.TotalTableDateView,
                    new ParserDetail
                    {
                        URL = @"https://www.taifex.com.tw/cht/3/totalTableDateView",
                        QueryString =  @"queryStartDate={0}&queryEndDate={1}",
                        Method = "POST",
                    }
                },
                {
                    Model.Sources.FutAndOptDateView,
                    new ParserDetail
                    {
                        URL = @"https://www.taifex.com.tw/cht/3/futAndOptDateView",
                        QueryString = @"queryStartDate={0}&queryEndDate={1}",
                        Method = "POST"
                    }
                },
                {
                    Model.Sources.FutContractsDateView,
                    new ParserDetail
                    {
                        URL = @"https://www.taifex.com.tw/cht/3/futContractsDateView",
                        QueryString = @"queryStartDate={0}&queryEndDate={1}",
                        Method = "POST"
                    }
                },
                {
                    Model.Sources.OptContractsDateView,
                    new ParserDetail
                    {
                        URL =@"https://www.taifex.com.tw/cht/3/optContractsDateView",
                        QueryString = @"queryStartDate={0}&queryEndDate={1}",
                        Method = "POST"
                    }
                },
                {
                    Model.Sources.CallsAndPutsDateView,
                    new ParserDetail
                    {
                        URL =@"https://www.taifex.com.tw/cht/3/callsAndPutsDateView",
                        QueryString = @"queryStartDate={0}&queryEndDate={1}",
                        Method = "POST"
                    }
                }
            };
        }
        public class ParserDetail
        {
            public string URL { get; set; }
            public string QueryString { get; set; }
            public string Method { get; set; }
        }
    }

}
