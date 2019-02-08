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
                Sources = Sources.FutDailyMarketViewOptions,
                QueryStartDate = new DateTime(2019,1,15),
                QueryEndDate = new DateTime(2019, 1, 17)
            };
            _ = o.Start();
            Console.WriteLine(o.Response);
        }
    }
}
