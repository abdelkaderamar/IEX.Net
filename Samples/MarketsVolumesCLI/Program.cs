using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IEX.Api;

namespace MarketsVolumesCLI
{
    class Program
    {
        static void Main(string[] args)
        {
            IexMarketProvider marketProvider = new IexMarketProvider();
            var marketDataDict = marketProvider.RequestMarketData();
            Console.WriteLine("Getting markets data");
            foreach (var market in marketDataDict.Values)
            {
                Console.WriteLine(market);
            }
            Console.WriteLine();
            Console.WriteLine("************************************************");
            Console.WriteLine();
            Console.WriteLine("Getting IEX Intraday stats");
            var iexStatProvider = new IexStatProvider();
            var iexIntradayStat = iexStatProvider.RequestIexIntradayStat();
            Console.WriteLine(iexIntradayStat);


            Console.ReadKey();
        }
    }
}
