using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json.Linq;

namespace IEX.Api.Data
{

    public class HistoSummaryData
    {
        #region JSON keys
        public static readonly string AVG_DAILY_VOLUME_KEY = "averageDailyVolume";
        public static readonly string AVG_DAILY_ROUTED_VOLUME_KEY = "averageDailyRoutedVolume";
        public static readonly string AVG_MARKET_SHARE_KEY = "averageMarketShare";
        public static readonly string AVG_ORDER_SIZE_KEY = "averageOrderSize";
        public static readonly string AVG_FILL_SIZE_KEY = "averageFillSize";
        //"bin100Percent": 0.61559,
        //"bin101Percent": 0.01819,
        //"bin200Percent": 0.1587,
        //"bin300Percent": 0.06125,
        //"bin400Percent": 0.03561,
        //"bin500Percent": 0.06507,
        //"bin1000Percent": 0.04019,
        //"bin5000Percent": 0.00342,
        //"bin10000Percent": 0.00199,
        //"bin10000Trades": 4666,
        //"bin20000Trades": 1568,
        //"bin50000Trades": 231,
        public static readonly string UNIQUE_SYMBOLS_TRADED_KEY = "uniqueSymbolsTraded";
        public static readonly string BLOCK_PERCENT_KEY = "blockPercent";
        public static readonly string SELF_CROSS_PERCENT_KEY = "selfCrossPercent";
        public static readonly string ETF_PERCENT_KEY = "etfPercent";
        public static readonly string LARGE_CAP_PERCENT_KEY = "largeCapPercent";
        public static readonly string MID_CAP_PERCENT_KEY = "midCapPercent";
        public static readonly string SMALL_CAP_PERCENT_KEY = "smallCapPercent";
        //"venueARCXFirstWaveWeight": 0.22063,
        //"venueBATSFirstWaveWeight": 0.06249,
        //"venueBATYFirstWaveWeight": 0.07361,
        //"venueEDGAFirstWaveWeight": 0.01083,
        //"venueEDGXFirstWaveWeight": 0.0869,
        public static readonly string VENUE_OVERALL_FIRST_WAVE_WEIGHT_KEY = "venueOverallFirstWaveWeight";
        //"venueXASEFirstWaveWeight": 0.00321,
        //"venueXBOSFirstWaveWeight": 0.02935,
        //"venueXCHIFirstWaveWeight": 0.00108,
        //"venueXCISFirstWaveWeight": 0.00008,
        //"venueXNGSFirstWaveWeight": 0.20358,
        //"venueXNYSFirstWaveWeight": 0.29313,
        //"venueXPHLFirstWaveWeight": 0.01511,
        //"venueARCXFirstWaveRate": 0.97737,
        //"venueBATSFirstWaveRate": 0.99357,
        //"venueBATYFirstWaveRate": 0.99189,
        //"venueEDGAFirstWaveRate": 0.98314,
        //"venueEDGXFirstWaveRate": 0.99334,
        public static readonly string VENUE_OVERALL_FIRST_WAVE_RATE_KEY = "venueOverallFirstWaveRate";
        //"venueXASEFirstWaveRate": 0.94479,
        //"venueXBOSFirstWaveRate": 0.97829,
        //"venueXCHIFirstWaveRate": 0.65811,
        //"venueXCISFirstWaveRate": 0.9468,
        //"venueXNGSFirstWaveRate": 0.98174,
        //"venueXNYSFirstWaveRate": 0.98068,
        //"venueXPHLFirstWaveRate": 0.93629
        #endregion

        #region JSON regular expressions
        private static readonly Regex BIN_PERCENT_REGEX = new Regex(@"bin(?<x>\d+)Percent");
        private static readonly Regex BIN_TRADES_REGEX = new Regex(@"bin(?<x>\d+)Trades");
        private static readonly Regex VENUE_FIRST_WAVE_WEIGHT_REGEX = new Regex(@"venue(?<venue>\w+)FirstWaveWeight");
        private static readonly Regex VENUE_FIRST_WAVE_RATE_REGEX = new Regex(@"venue(?<venue>\w+)FirstWaveRate");
        #endregion

        private IDictionary<long, double> _binPercents = new SortedDictionary<long, double>();

        private IDictionary<long, double> _binTrades = new SortedDictionary<long, double>();

        private IDictionary<string, double> _venueFirstWaveWeights = new SortedDictionary<string, double>();

        private IDictionary<string, double> _venueFirstWaveRates = new SortedDictionary<string, double>();

        public double AverageDailyVolume { get; set; }

        public double AverageDailyRoutedVolume { get; set; }

        public double AverageMarketShare { get; set; }

        public double AverageOrderSize { get; set; }

        public double AverageFillSize { get; set; }

        public long UniqueSymbolsTraded { get; set; }

        public double BlockPercent { get; set; }

        public double SelfCrossPercent { get; set; }

        public double EtfPercent { get; set; }

        public double LargeCapPercent { get; set; }

        public double MidCapPercent { get; set; }

        public double SmallCapPercent { get; set; }

        public double GetBinPercent(int x)
        {
            return _binPercents[x];
        }

        public double GetBinTrades(int x)
        {
            return _binTrades[x];
        }

        public double GetVenueFirstWaveWeight(string venue)
        {
            return _venueFirstWaveWeights[venue];
        }

        public double GetOverallFirstWaveWeight()
        {
            return _venueFirstWaveWeights["Overall"];
        }

        public double GetVenueFirstWaveRate(string market)
        {
            return _venueFirstWaveRates["Overall"];
        }

        public double GetVenueOverallFirstWaveRate()
        {
            return _venueFirstWaveRates["Overall"];
        }

        public override string ToString()
        {
            Action<StringBuilder, string, object> appendVal = (sb, label, val) =>
            {
                sb.Append(label).Append(val).Append(Environment.NewLine);
            };
            StringBuilder stringBuilder = new StringBuilder();
            appendVal(stringBuilder, "Average Daily Volume        = ", AverageDailyVolume);
            appendVal(stringBuilder, "Average Daily Routed Volume = ", AverageDailyRoutedVolume);
            appendVal(stringBuilder, "Average Market Share        = ", AverageMarketShare);
            appendVal(stringBuilder, "Average Order Size          = ", AverageOrderSize);
            appendVal(stringBuilder, "Average Fill Size           = ", AverageFillSize);
            appendVal(stringBuilder, "Unique Symbols Traded       = ", UniqueSymbolsTraded);
            appendVal(stringBuilder, "Block Percent               = ", BlockPercent);
            appendVal(stringBuilder, "Self Cross Percent          = ", SelfCrossPercent);
            appendVal(stringBuilder, "Etf Percent                 = ", EtfPercent);
            appendVal(stringBuilder, "Large Cap Percent           = ", LargeCapPercent);
            appendVal(stringBuilder, "Mid Cap Percent             = ", MidCapPercent);
            appendVal(stringBuilder, "Small Cap Percent           = ", SmallCapPercent);
            stringBuilder.Append("Bin Percents").Append(Environment.NewLine);
            foreach(var binPercent in _binPercents)
            {
                stringBuilder.AppendFormat("\tBin {0} Percents = {1}", binPercent.Key, binPercent.Value).Append(Environment.NewLine);
            }
            stringBuilder.Append("Bin Trades").Append(Environment.NewLine);
            foreach (var binTrade in _binTrades)
            {
                stringBuilder.AppendFormat("\tBin {0} Trades = {1}", binTrade.Key, binTrade.Value).Append(Environment.NewLine);
            }
            stringBuilder.Append("Venues First Wave Weight").Append(Environment.NewLine);
            foreach (var venueFirstwaveWeight in _venueFirstWaveWeights)
            {
                stringBuilder.AppendFormat("\t{0} First Wave Weight = {1}", venueFirstwaveWeight.Key, venueFirstwaveWeight.Value).Append(Environment.NewLine);
            }
            stringBuilder.Append("Venues First Wave Rate").Append(Environment.NewLine);
            foreach (var venueFirstwaveRate in _venueFirstWaveRates)
            {
                stringBuilder.AppendFormat("\t{0} First Wave Rate = {1}", venueFirstwaveRate.Key, venueFirstwaveRate.Value).Append(Environment.NewLine);
            }
            return stringBuilder.ToString();
        }

        public static HistoSummaryData FromJson(JObject json)
        {
            HistoSummaryData histoSummary = new HistoSummaryData()
            {
                AverageDailyVolume = JsonHelper.GetDoubleValue(json, AVG_DAILY_VOLUME_KEY),
                AverageDailyRoutedVolume = JsonHelper.GetDoubleValue(json, AVG_DAILY_ROUTED_VOLUME_KEY),
                AverageMarketShare = JsonHelper.GetDoubleValue(json, AVG_MARKET_SHARE_KEY),
                AverageOrderSize = JsonHelper.GetDoubleValue(json, AVG_ORDER_SIZE_KEY),
                AverageFillSize = JsonHelper.GetDoubleValue(json, AVG_FILL_SIZE_KEY),
                UniqueSymbolsTraded = JsonHelper.GetLongValue(json, UNIQUE_SYMBOLS_TRADED_KEY),
                BlockPercent = JsonHelper.GetDoubleValue(json, BLOCK_PERCENT_KEY),
                SelfCrossPercent = JsonHelper.GetDoubleValue(json, SELF_CROSS_PERCENT_KEY),
                EtfPercent = JsonHelper.GetDoubleValue(json, ETF_PERCENT_KEY),
                LargeCapPercent = JsonHelper.GetDoubleValue(json, LARGE_CAP_PERCENT_KEY),
                MidCapPercent = JsonHelper.GetDoubleValue(json, MID_CAP_PERCENT_KEY),
                SmallCapPercent = JsonHelper.GetDoubleValue(json, SMALL_CAP_PERCENT_KEY)
            };
            var properties = json.Properties();
            foreach (var property in properties)
            {
                if (property.Name.StartsWith("bin"))
                {
                    MatchCollection matchesBinPercent = BIN_PERCENT_REGEX.Matches(property.Name);
                    if (matchesBinPercent.Count > 0)
                    {
                        foreach (Match match in matchesBinPercent)
                        {
                            GroupCollection groups = match.Groups;
                            long x;
                            if (long.TryParse(groups["x"].Value, out x))
                            {
                                histoSummary._binPercents[x] = JsonHelper.GetDoubleValue(json, property.Name);
                            }
                        }
                    }
                    MatchCollection matchesBinTrades = BIN_TRADES_REGEX.Matches(property.Name);
                    if (matchesBinTrades.Count > 0)
                    {
                        foreach (Match match in matchesBinTrades)
                        {
                            GroupCollection groups = match.Groups;
                            long x;
                            if (long.TryParse(groups["x"].Value, out x))
                            {
                                histoSummary._binTrades[x] = JsonHelper.GetDoubleValue(json, property.Name);
                            }
                        }

                    }
                }
                else if (property.Name.StartsWith("venue"))
                {
                    MatchCollection matchesVenueFirstWeight = VENUE_FIRST_WAVE_WEIGHT_REGEX.Matches(property.Name);
                    if (matchesVenueFirstWeight.Count > 0)
                    {
                        Console.WriteLine("{0} matches Venue First Weight regex", property.Name);
                        foreach (Match match in matchesVenueFirstWeight)
                        {
                            GroupCollection groups = match.Groups;
                            histoSummary._venueFirstWaveWeights[groups["venue"].Value] = JsonHelper.GetDoubleValue(json, property.Name);
                        }
                    }
                    MatchCollection matchesVenueFirstRate = VENUE_FIRST_WAVE_RATE_REGEX.Matches(property.Name);
                    if (matchesVenueFirstRate.Count > 0)
                    {
                        foreach (Match match in matchesVenueFirstRate)
                        {
                            GroupCollection groups = match.Groups;
                            histoSummary._venueFirstWaveRates[groups["venue"].Value] = JsonHelper.GetDoubleValue(json, property.Name);
                        }
                    }
                }
            }
            return histoSummary;
        }
    }
}
