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
                Sources = Sources.DailyFXRateDown,
                StockNo= 1102,
                Commodity = "CAF",
                QueryStartDate = new DateTime(2019,01,01),
                QueryEndDate = new DateTime(2019, 02,09)
            };
            o.Start();
            Console.WriteLine(o.Response);
        }
    }
}
