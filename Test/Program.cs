using TWSEParser.Services;
using TWSEParser.Model;
using System;
using System.Collections.Generic;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            for (var i = 5; i < 6; i++)
            {
                var o = new Options
                {
                    Sources = (Sources)i,
                    StockNo = 1102,
                    Commodity = "CAF",
                    QueryStartDate = new DateTime(2019, 01, 02),
                    QueryEndDate = new DateTime(2019, 01, 04)
                };
                o.Start();
                Console.WriteLine(o.Response);
            }
            //var o = new Options
            //{
            //    Sources = (Sources)5,
            //    StockNo = 1102,
            //    Commodity = "CAF",
            //    QueryStartDate = new DateTime(2018, 12, 02),
            //    QueryEndDate = new DateTime(2018, 12, 04)
            //};
            //o.Start();
            //Console.WriteLine(o.Response);
        }
    }
}
