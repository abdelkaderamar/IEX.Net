// Copyright (c) Abdelkader Amar. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.


﻿using System;
using Newtonsoft.Json.Linq;
using System.Text;

namespace IEX.Api.Data
{
    public class MarketData
    {

        #region JSON Keys
        public static readonly string MIC_KEY = "mic";
        public static readonly string TAPE_ID_KEY = "tapeId";
        public static readonly string VENUENAME_KEY = "venueName";
        public static readonly string VOLUME_KEY = "volume";
        public static readonly string TAPE_A_KEY = "tapeA";
        public static readonly string TAPE_B_KEY = "tapeB";
        public static readonly string TAPE_C_KEY = "tapeC";
        public static readonly string MARKET_PERCENT_KEY = "marketPercent";
        public static readonly string LASTUPDATED_KEY = "lastUpdated";
        #endregion

        public MarketData(string mic)
        {
            MIC = mic;
        }

        public string MIC { get; }

        public string TapeId { get; set; }

        public string VenueName { get; set; }

        public decimal Volume { get; set; }

        public decimal VolumeTapeA { get; set; }

        public decimal VolumeTapeB { get; set; }

        public decimal VolumeTapeC { get; set; }

        public decimal MarketPercent { get; set; }

        public DateTime LastUpdated { get; set; }

        public static MarketData FromJson(JObject json)
        {
            var mic = JsonHelper.GetValue(json, MIC_KEY);
            MarketData marketData = new MarketData(mic)
            {
                TapeId = JsonHelper.GetValue(json, TAPE_ID_KEY),
                VenueName = JsonHelper.GetValue(json, VENUENAME_KEY),
                Volume = JsonHelper.GetDecimalValue(json, VOLUME_KEY),
                VolumeTapeA = JsonHelper.GetDecimalValue(json, TAPE_A_KEY),
                VolumeTapeB = JsonHelper.GetDecimalValue(json, TAPE_B_KEY),
                VolumeTapeC = JsonHelper.GetDecimalValue(json, TAPE_C_KEY),
                MarketPercent = JsonHelper.GetDecimalValue(json, MARKET_PERCENT_KEY),
                LastUpdated = JsonHelper.GetDateTimeValue(json, LASTUPDATED_KEY)
            };
            return marketData;
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("Name = ").Append(VenueName).Append(", MIC = ").Append(MIC).Append(Environment.NewLine).
                Append("\tVolume = ").Append(Volume).Append(Environment.NewLine).
                Append("\tMaket Percent = ").Append(MarketPercent).Append(Environment.NewLine).
                Append("\tVolume Tape A = ").Append(VolumeTapeA).Append(Environment.NewLine).
                Append("\tVolume Tape B = ").Append(VolumeTapeB).Append(Environment.NewLine).
                Append("\tVolume Tape C = ").Append(VolumeTapeC).Append(Environment.NewLine).
                Append("\tLast Updated = ").Append(LastUpdated).Append(Environment.NewLine);
            return builder.ToString();
        }
    }
}
