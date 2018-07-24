// Copyright (c) Abdelkader Amar. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.


﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace IEX.Api.Data
{
    /**
     * 
     "SNAP": [
        {
          "price": 156.1,
          "size": 100,
          "tradeId": 517341294,
          "isISO": false,
          "isOddLot": false,
          "isOutsideRegularHours": false,
          "isSinglePriceCross": false,
          "isTradeThroughExempt": false,
          "timestamp": 1494619192003
        }
      ]
*/
    public class TradeData : IexData
    {
        public TradeData(string symbol) : base(symbol)
        {
            Trades = new List<Trade>();
        }

        public List<Trade> Trades { get; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("Trades for {0}:", Symbol).Append(Environment.NewLine);
            foreach (var trade in Trades)
            {
                sb.AppendFormat("\t{0}", trade).Append(Environment.NewLine);
            }
            return sb.ToString();
        }

        public static TradeData FromJson(JProperty json)
        {
            if (json == null) return null;

            var symbol = json.Name;
            TradeData tradeData = new TradeData(symbol);
            var value = json.Value as JArray;
            if (value != null)
            {
                foreach (var tradeJson in value.Children<JObject>())
                {
                    var trade = Trade.FromJson(tradeJson);
                    tradeData.Trades.Add(trade);
                }
            }
            return tradeData;
        }

    }

    public class Trade
    {
        #region JSON keys
        public static readonly string PRICE_KEY = "price";
        public static readonly string SIZE_KEY = "size";
        public static readonly string TRADEID_KEY = "tradeId";
        public static readonly string ISISO_KEY = "isISO";
        public static readonly string ISODDLOT_KEY = "isOddLot";
        public static readonly string IS_OUTSIDE_REGULAR_HOURS_KEY = "isOutsideRegularHours";
        public static readonly string IS_SINGLE_PRICE_CROSS_KEY = "isSinglePriceCross";
        public static readonly string IS_TRADE_THROUGH_EXEMPT_KEY = "isTradeThroughExempt";
        public static readonly string TIMESTAMP_KEY = "timestamp";
        #endregion

        #region Properties
        public decimal Price { get; set; }

        public long Size { get; set; }

        public string TradeId { get; set; }

        public bool IsIso { get; set; }

        public bool IsOddLot { get; set; }

        public bool IsOutsideRegularHours { get; set; }

        public bool IsSinglePriceCross { get; set; }

        public bool IsTradeThroughExempt { get; set; }

        public DateTime Timestamp { get; set; }
        #endregion

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("Price={0}, Size={1}, Timestamp={2}", Price, Size, Timestamp);
            return sb.ToString();
        }

        public static Trade FromJson(JObject json)
        {
            Trade trade = new Trade()
            {
                Price = JsonHelper.GetDecimalValue(json, PRICE_KEY),
                Size = JsonHelper.GetLongValue(json, SIZE_KEY),
                TradeId = JsonHelper.GetValue(json, TRADEID_KEY),
                IsIso = JsonHelper.GetBooleanValue(json, ISISO_KEY),
                IsOddLot = JsonHelper.GetBooleanValue(json, ISODDLOT_KEY),
                IsOutsideRegularHours = JsonHelper.GetBooleanValue(json, IS_OUTSIDE_REGULAR_HOURS_KEY),
                IsSinglePriceCross = JsonHelper.GetBooleanValue(json, IS_SINGLE_PRICE_CROSS_KEY),
                IsTradeThroughExempt = JsonHelper.GetBooleanValue(json, IS_TRADE_THROUGH_EXEMPT_KEY),
                Timestamp = JsonHelper.GetDateTimeValue(json, TIMESTAMP_KEY)
            };

            return trade;
        }
    }
}
