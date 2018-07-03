using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Globalization;

namespace IEX.Api
{
    public class IexMarketProvider : IexProvider
    {
        public static readonly string MARKET_FUNC = "market";

        public static readonly string MARKET_URL = BASE_URL + MARKET_FUNC;

        public IexMarketProvider()
        {
        }

        public async Task<IDictionary<string, MarketData>> RequestMarketDataAsync()
        {
            var json = await Request(MARKET_URL);

            var marketDataDict = ParseData(json);

            return marketDataDict;
        }


        public IDictionary<string, MarketData> RequestMarketData()
        {
            var json = Request(MARKET_URL).Result;

            var marketDataDict = ParseData(json);

            return marketDataDict;
        }

        public IDictionary<string, MarketData> ParseData(JContainer json)
        {
            IDictionary<string, MarketData> marketDataDict = new Dictionary<string, MarketData>();
            if (!(json is JArray)) return marketDataDict;

            var jarray = (JArray)json;
            foreach (var marketJson in jarray.Children<JObject>())
            {
                try
                {
                    var marketData = MarketData.FromJson(marketJson);
                    marketDataDict[marketData.MIC] = marketData;

                }
                catch (Exception)
                {
                    // TODO : log error message
                    continue;
                }
            }
            return marketDataDict;
        }

    }
}
