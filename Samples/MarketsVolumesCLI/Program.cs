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
            foreach (var market in marketDataDict.Values)
            {
                Console.WriteLine(market);
            }
            
        }
    }
}
