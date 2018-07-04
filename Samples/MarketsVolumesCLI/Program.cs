using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IEX.Api;
using IEX.Api.Data;

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

            Console.WriteLine();
            Console.WriteLine("************************************************");
            Console.WriteLine();
            Console.WriteLine("Getting IEX recent stats");
            var iexRecentStats = iexStatProvider.RequestIexRecentStat();
            foreach (var histoData in iexRecentStats)
            {
                Console.WriteLine(histoData);
            }

            Console.WriteLine();
            Console.WriteLine("************************************************");
            Console.WriteLine();
            Console.WriteLine("Getting IEX record stats");
            var recordData = iexStatProvider.RequestRecordData();
            Console.WriteLine(recordData);

            Console.WriteLine();
            Console.WriteLine("************************************************");
            Console.WriteLine();
            Console.WriteLine("Getting IEX historical summary stats");
            var histoSummary = iexStatProvider.RequestHistoricalSummary(new DateTime(2018, 1, 1));
            Console.WriteLine(histoSummary);

            Console.WriteLine();
            Console.WriteLine("************************************************");
            Console.WriteLine();
            Console.WriteLine("Getting IEX historical daily stats");
            var histoDaily = iexStatProvider.RequestHistoricalDaily();
            Console.WriteLine(histoDaily);


            Console.WriteLine();
            Console.WriteLine("************************************************");
            Console.WriteLine();
            Console.WriteLine("Getting IEX historical stats of 30 last days");
            var last30HistoDaily = iexStatProvider.RequestHistoricalDaily(30);
            foreach (var data in last30HistoDaily)
            {
                Console.WriteLine(data);
            }

            Console.WriteLine();
            Console.WriteLine("************************************************");
            Console.WriteLine();
            Console.WriteLine("Getting IEX historical stats of November 2017");
            var nov18HistoDaily = iexStatProvider.RequestHistoricalDaily(new DateTime(2017, 11, 1));
            foreach (var data in nov18HistoDaily)
            {
                Console.WriteLine(data);
            }

            Console.ReadKey();

        }
    }
}
