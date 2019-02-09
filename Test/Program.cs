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
                Sources = Sources.StockDayAVG,
                StockNo= 1102,
                Commodity = "CAF",
                QueryStartDate = new DateTime(2019,01,25),
                QueryEndDate = new DateTime(2018, 11, 30)
            };
            _ = o.Start();
            Console.WriteLine(o.Response);
        }
    }
}
