using System;
using Newtonsoft.Json.Linq;
using System.Text;
using System.Globalization;

namespace IEX.Api.Data
{
    public class HistoricalData
    {
        #region JSON keys
        public static readonly string DATE_KEY = "date";
        public static readonly string VOLUME_KEY = "volume";
        public static readonly string ROUTED_VOLUME_KEY = "routedVolume";
        public static readonly string MARKET_SHARE_KEY = "marketShare";
        public static readonly string IS_HALF_DAY_KEY = "isHalfday";
        public static readonly string LIT_VOLUME_KEY = "litVolume";
        #endregion

        public HistoricalData(DateTime date, long volume)
        {
            Date = date;
            Volume = volume;
        }

        public DateTime Date { get; }

        public long Volume { get; set; }

        public long RoutedVolume { get; set; }

        public decimal MarketShare { get; set; }

        public bool IsHalfDay { get; set; }

        public long LitVolume { get; set; }

        public static HistoricalData FromJson(JObject json)
        {
            // var date = JsonHelper.GetDateTimeValue(json, DATE_KEY);
            var date = DateTime.ParseExact(JsonHelper.GetValue(json, DATE_KEY), "yyyy-MM-dd", CultureInfo.InvariantCulture);
            var volume = JsonHelper.GetLongValue(json, VOLUME_KEY, -1);
            if (date == null || volume == -1) return null;

            HistoricalData histoData = new HistoricalData(date, volume);
            histoData.RoutedVolume = JsonHelper.GetLongValue(json, ROUTED_VOLUME_KEY);
            histoData.MarketShare = JsonHelper.GetDecimalValue(json, MARKET_SHARE_KEY);
            histoData.IsHalfDay = JsonHelper.GetBooleanValue(json, IS_HALF_DAY_KEY);
            histoData.LitVolume = JsonHelper.GetLongValue(json, LIT_VOLUME_KEY);
            return histoData;
        }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();
            Action<StringBuilder, string, object> appendVal = (sb, label, val) =>
            {
                sb.Append(label).Append(val).Append(Environment.NewLine);
            };
            appendVal(stringBuilder, "Date          = ", Date);
            appendVal(stringBuilder, "Volume        = ", Volume);
            appendVal(stringBuilder, "Routed Volume = ", RoutedVolume);
            appendVal(stringBuilder, "Market Share  = ", MarketShare*100 + " %");
            appendVal(stringBuilder, "Is Half Day   = ", IsHalfDay);
            appendVal(stringBuilder, "Lit Volume    = ", LitVolume);
            return stringBuilder.ToString();
        }
    }
}
