using TWSEParser.Services;
using TWSEParser.Model;
using System;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var check = OptionsService.Start(
                new Options {
                    Sources = Sources.FutPrevious30DaysSalesData,
                    //QueryStartDate = new DateTime(2019,1,29)
                });
            if(check != true)
            {
                Console.WriteLine("False");
            }
        }
    }
}
