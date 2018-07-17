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
     {
        "symbol":"SNAP",
        "sector":"softwareservices",
        "securityType":"commonstock",
        "bidPrice":0,
        "bidSize":0,
        "askPrice":0,
        "askSize":0,
        "lastUpdated":1531857600000,
        "lastSalePrice":13.405,
        "lastSaleSize":100,
        "lastSaleTime":1531857595826,
        "volume":217309,
        "marketPercent":0.01366},
    */
    public class TopsData
    {
        #region JSON keys
        public static readonly string SYMBOL_KEY = "symbol";
        public static readonly string SECTOR_KEY = "sector";
        public static readonly string SECURITY_TYPE_KEY = "securityType";
        public static readonly string BID_PRICE_KEY = "bidPrice";
        public static readonly string BID_SIZE_KEY = "bidSize";
        public static readonly string ASK_PRICE_KEY = "askPrice";
        public static readonly string ASK_SIZE_KEY = "askSize";
        public static readonly string LAST_UPDATED_KEY = "lastUpdated";
        public static readonly string LAST_SALE_PRICE_KEY = "lastSalePrice";
        public static readonly string LAST_SALE_SIZE_KEY = "lastSaleSize";
        public static readonly string LAST_SALE_TIME_KEY = "lastSaleTime";
        public static readonly string VOLUME_KEY = "volume";
        public static readonly string MARKET_PERCENT_KEY = "marketPercent";
        #endregion

        public TopsData(string symbol)
        {
            Symbol = symbol;
        }

        #region properties
        public string Symbol { get;  }

        public string Sector { get; set; }

        public string SecurityType { get; set; }

        public decimal BidPrice { get; set; }

        public long BidSize { get; set; }

        public decimal AskPrice { get; set; }

        public long AskSize { get; set; }

        public DateTime LastUpdated { get; set; }

        public decimal LastSalePrice { get; set; }

        public long LastSaleSize { get; set; }

        public DateTime LastSaleTime { get; set; }

        public long Volume { get; set; }

        public double MarketPercent { get; set; }
        #endregion

        public static TopsData FromJson(JObject json)
        {
            var symbol = JsonHelper.GetValue(json, SYMBOL_KEY);
            TopsData topData = new TopsData(symbol)
            {
                Sector = JsonHelper.GetValue(json, SECTOR_KEY),
                SecurityType = JsonHelper.GetValue(json, SECURITY_TYPE_KEY),
                BidPrice = JsonHelper.GetDecimalValue(json, BID_PRICE_KEY),
                BidSize = JsonHelper.GetLongValue(json, BID_SIZE_KEY),
                AskPrice = JsonHelper.GetDecimalValue(json, ASK_PRICE_KEY),
                AskSize = JsonHelper.GetLongValue(json, ASK_SIZE_KEY),
                LastUpdated = JsonHelper.GetDateTimeValue(json, LAST_UPDATED_KEY),
                LastSalePrice = JsonHelper.GetDecimalValue(json, LAST_SALE_PRICE_KEY),
                LastSaleSize = JsonHelper.GetLongValue(json, LAST_SALE_SIZE_KEY),
                LastSaleTime = JsonHelper.GetDateTimeValue(json, LAST_SALE_TIME_KEY),
                Volume = JsonHelper.GetLongValue(json, VOLUME_KEY),
                MarketPercent = JsonHelper.GetDoubleValue(json, MARKET_PERCENT_KEY)
            };
            return topData;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("Tops for {0} of type {1}/{2}", Symbol, SecurityType, Sector).
                Append(Environment.NewLine).
                AppendFormat("\tBid = {0}@{1}", BidSize, BidPrice).
                AppendFormat("\tAsk = {0}@{1}", AskSize, AskPrice);
            // TODO : Add other fields
            return sb.ToString();
        }
    }
}
