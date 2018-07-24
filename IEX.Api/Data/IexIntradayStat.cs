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
    public class IexIntradayStat
    {

        public static readonly string VOLUME_KEY = "volume";
        public static readonly string SYMBOLS_TRADED_KEY = "symbolsTraded";
        public static readonly string ROUTED_VOLUME_KEY = "routedVolume";
        public static readonly string NOTIONAL_KEY = "notional";
        public static readonly string MARKET_SHARE_KEY = "marketShare";
        public static readonly string VALUE_KEY = "value";
        public static readonly string LAST_UPDATED_KEY = "lastUpdated";

        public long Volume { get; set; }

        public DateTime VolumeLastUpdated { get; set; }

        public long SymbolsTraded { get; set; }

        public DateTime SymbolsTradedLastUpdated { get; set; }

        public long RoutedVolume { get; set; }

        public DateTime RoutedVolumeLastUpdated { get; set; }

        public decimal Notional { get; set; }

        public DateTime NotionalLastUpdated { get; set; }

        public decimal MarketShare { get; set; }

        public DateTime MarketShareLastUpdated { get; set; }

        public static IexIntradayStat FromJson(JObject json)
        {
            var volumeJson = (JObject)json.GetValue(VOLUME_KEY);
            var symbolsTradedJson = (JObject)json.GetValue(SYMBOLS_TRADED_KEY);
            var routedVolumeJson = (JObject)json.GetValue(ROUTED_VOLUME_KEY);
            var notionalJson = (JObject)json.GetValue(NOTIONAL_KEY);
            var marketShareJson = (JObject)json.GetValue(MARKET_SHARE_KEY);
            if (volumeJson == null || symbolsTradedJson == null || routedVolumeJson == null || notionalJson == null || marketShareJson == null)
                return null;
            IexIntradayStat intradayStat = new IexIntradayStat()
            {
                Volume = JsonHelper.GetLongValue(volumeJson, VALUE_KEY),
                VolumeLastUpdated = JsonHelper.GetDateTimeValue(volumeJson, LAST_UPDATED_KEY),
                SymbolsTraded = JsonHelper.GetLongValue(symbolsTradedJson, VALUE_KEY),
                SymbolsTradedLastUpdated = JsonHelper.GetDateTimeValue(symbolsTradedJson, LAST_UPDATED_KEY),
                RoutedVolume = JsonHelper.GetLongValue(routedVolumeJson, VALUE_KEY),
                RoutedVolumeLastUpdated = JsonHelper.GetDateTimeValue(routedVolumeJson, LAST_UPDATED_KEY),
                Notional = JsonHelper.GetDecimalValue(notionalJson, VALUE_KEY),
                NotionalLastUpdated = JsonHelper.GetDateTimeValue(notionalJson, LAST_UPDATED_KEY),
                MarketShare = JsonHelper.GetDecimalValue(marketShareJson, VALUE_KEY),
                MarketShareLastUpdated = JsonHelper.GetDateTimeValue(marketShareJson, LAST_UPDATED_KEY),

            };
            return intradayStat;
        }

        public override string ToString()
        {
            Action<StringBuilder, string, object, DateTime> appendVal = (sb, label, val, date) =>
            {
                sb.Append(label).Append(val).Append(" [LastUpdate = ").Append(date).Append("]").Append(Environment.NewLine);
            };

            StringBuilder builder = new StringBuilder();

            appendVal(builder, "Volume = ", Volume, VolumeLastUpdated);
            appendVal(builder, "Symbols Traded = ", SymbolsTraded, SymbolsTradedLastUpdated);
            appendVal(builder, "Routed Volume = ", RoutedVolume, RoutedVolumeLastUpdated);
            appendVal(builder, "Notional = ", Notional, NotionalLastUpdated);
            appendVal(builder, "Market Share = ", MarketShare * 100 + " %", MarketShareLastUpdated);
            return builder.ToString();
        }


    }
}
