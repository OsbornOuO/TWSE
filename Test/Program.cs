using TWSEParser.Services;
using TWSEParser.Model;
using System;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var o = new Options
            {
                Sources = Sources.LargeTraderFutDown,
                StockNo= 1102,
                Commodity = "CAF",
                QueryStartDate = new DateTime(2019,01,02),
                QueryEndDate = new DateTime(2019, 01,04)
            };
            o.Start();
            Console.WriteLine(o.Response);
        }
    }
}
