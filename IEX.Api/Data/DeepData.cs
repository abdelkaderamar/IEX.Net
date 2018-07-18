using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace IEX.Api.Data
{
    /**
     * 	
    "symbol": "SNAP",
	"marketPercent": 0,
	"volume": 0,
	"lastSalePrice": 0,
	"lastSaleSize": 0,
	"lastSaleTime": 0,
	"lastUpdated": 0,
	"bids": [],
	"asks": [],
	"systemEvent": {},
    */
    public class DeepData
    {
        #region JSON keys
        public static readonly string SYMBOL_KEY = "symbol";
        public static readonly string MARKET_PERCENT_KEY = "marketPercent";
        public static readonly string VOLUME_KEY = "volume";
        public static readonly string LAST_SALE_PRICE_KEY = "lastSalePrice";
        public static readonly string LAST_SALE_SIZE_KEY = "lastSaleSize";
        public static readonly string LAST_SALE_TIME_KEY = "lastSaleTime";
        public static readonly string LAST_UPDATED_KEY = "lastUpdated";
        public static readonly string BIDS_KEY = "bids";
        public static readonly string ASKS_KEY = "asks";
        public static readonly string SYSTEM_EVENT_KEY = "systemEvent";
        public static readonly string TRADES_KEY = "trades";
        public static readonly string TRADE_BREAKS_KEY = "tradeBreaks";
        #endregion

        public DeepData(string symbol)
        {
            Symbol = symbol;
        }

        #region Properties

        public string Symbol { get; }

        public double MarketPercent { get; set; }
        
        public long Volume { get; set; }

        public decimal LastSalePrice { get; set; }

        public long LastSaleSize { get; set; }

        public DateTime LastSaleTime { get; set; }

        public DateTime LastUpdated { get; set; }

        // TODO bids, asks, systemEvent, trades and tradeBreaks
        #endregion

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("Deep Data for {0}:", Symbol).Append(Environment.NewLine).
                AppendFormat("\t- Market Percent = [{0}], Volume = [{1}]", MarketPercent, Volume).Append(Environment.NewLine).
                AppendFormat("\t- Last : Sale Price = [{0}], Sale Size = [{1}], Sale Time = [{2}]", LastSalePrice, LastSaleSize, LastSaleTime).Append(Environment.NewLine).
                AppendFormat("\t- Last Update = {0}", LastUpdated);
            // TODO :  bids, asks, trades and tradeBreaks
            return sb.ToString();
        }

        public static DeepData FromJson(JObject json)
        {
            string symbol = JsonHelper.GetValue(json, SYMBOL_KEY);
            DeepData deepData = new DeepData(symbol)
            {
                MarketPercent = JsonHelper.GetDoubleValue(json, MARKET_PERCENT_KEY),
                Volume = JsonHelper.GetLongValue(json, VOLUME_KEY),
                LastSalePrice = JsonHelper.GetDecimalValue(json, LAST_SALE_PRICE_KEY),
                LastSaleSize = JsonHelper.GetLongValue(json, LAST_SALE_SIZE_KEY),
                LastSaleTime = JsonHelper.GetDateTimeValue(json, LAST_SALE_TIME_KEY),
                LastUpdated = JsonHelper.GetDateTimeValue(json, LAST_UPDATED_KEY),
            };
            return deepData;
        }
    }
}
