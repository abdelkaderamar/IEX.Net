using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace IEX.Api.Data
{
    /*
     * 
     {
	"volume": {
		"recordValue": 286354807,
		"recordDate": "2018-02-09",
		"previousDayValue": 138644731,
		"avg30Value": 170329429
	},
	"symbolsTraded": {
		"recordValue": 6381,
		"recordDate": "2018-02-06",
		"previousDayValue": 5809,
		"avg30Value": 5835
	},
	"routedVolume": {
		"recordValue": 74855222,
		"recordDate": "2016-11-10",
		"previousDayValue": 26051216,
		"avg30Value": 34633220
	},
	"notional": {
		"recordValue": 17269723089.111,
		"recordDate": "2018-02-06",
		"previousDayValue": 6807866951.3245,
		"avg30Value": 8402963385.1299
	}
    }
    */
    public class RecordData
    {
        #region JSON keys
        public static readonly string VOLUME_KEY = "volume";
        public static readonly string SYMBOLS_TRADED_KEY = "symbolsTraded";
        public static readonly string ROUTED_VOLUME_KEY = "routedVolume";
        public static readonly string NOTIONAL_KEY = "notional";
        public static readonly string RECORD_VALUE_KEY = "recordValue";
        public static readonly string RECORD_DATE_KEY = "recordDate";
        public static readonly string PREV_DAY_VALUE_KEY = "previousDayValue";
        public static readonly string AVG30_VALUE_KEY = "avg30Value";
        #endregion

        public class RecordItem<T>
        {
            public RecordItem(T recordValue, DateTime date, string name)
            {
                RecordValue = recordValue;
                Date = date;
                Name = name;
            }

            public string Name { get; }
            public T RecordValue { get; set; }
            public DateTime Date { get; set; }
            public T PreviousDayValue { get; set; }
            public decimal Avg30Value { get; set; }

            public static RecordItem<T> FromJson(JObject json, string name)
            {
                try
                {
                    var converter = TypeDescriptor.GetConverter(typeof(T));
                    if (converter != null)
                    {
                        T value = (T)converter.ConvertFromString(JsonHelper.GetValue(json, RECORD_VALUE_KEY));
                        DateTime date = DateTime.ParseExact(JsonHelper.GetValue(json, RECORD_DATE_KEY), "yyyy-MM-dd", CultureInfo.InvariantCulture);
                        RecordItem<T> record = new RecordItem<T>(value, date, name)
                        {
                            PreviousDayValue = (T)converter.ConvertFromString(JsonHelper.GetValue(json, PREV_DAY_VALUE_KEY)),
                            Avg30Value = JsonHelper.GetDecimalValue(json, AVG30_VALUE_KEY)
                        };
                        return record;
                    }
                    return null;
                }
                catch (NotSupportedException e)
                {
                    return null;
                }
            } // end FromJson

            public override string ToString()
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append(Name).Append(" Record").Append(Environment.NewLine).
                    Append("\tRecord Value       = ").Append(RecordValue).Append(Environment.NewLine).
                    Append("\tRecord Data        = ").Append(Date).Append(Environment.NewLine).
                    Append("\tPrevious Day Value = ").Append(PreviousDayValue).Append(Environment.NewLine).
                    Append("\tAvg 30 Day         = ").Append(Avg30Value);
                return stringBuilder.ToString();
            }
        } // end class RecordItem

        public RecordItem<long> VolumeRecord { get; set; }

        public RecordItem<long> RecordSymbolsTraded { get; set; }

        public RecordItem<long> RecordRoutedVolume { get; set; }

        public RecordItem<decimal> RecordNotional { get; set; }

        public static RecordData FromJson(JObject json)
        {
            RecordData recordData = new RecordData();

            var volumeJson = (JObject)json.GetValue(VOLUME_KEY);
            var symbolsTradedJson = (JObject)json.GetValue(SYMBOLS_TRADED_KEY);
            var routedVolumeJson = (JObject)json.GetValue(ROUTED_VOLUME_KEY);
            var notionalJson = (JObject)json.GetValue(NOTIONAL_KEY);

            if (volumeJson != null) recordData.VolumeRecord = RecordItem<long>.FromJson(volumeJson, "Volume");
            if (symbolsTradedJson != null) recordData.RecordSymbolsTraded = RecordItem<long>.FromJson(symbolsTradedJson, "Symbols Traded");
            if (routedVolumeJson != null) recordData.RecordRoutedVolume = RecordItem<long>.FromJson(routedVolumeJson, "Routed Volume");
            if (notionalJson != null) recordData.RecordNotional = RecordItem<decimal>.FromJson(notionalJson, "Notional");

            return recordData;
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();

            Action<StringBuilder, object> appendItem = (StringBuilder sb, object x) =>
            {
                if (x != null)
                    sb.Append(x.ToString() + Environment.NewLine);
            };

            appendItem(stringBuilder, VolumeRecord);
            appendItem(stringBuilder, RecordSymbolsTraded);
            appendItem(stringBuilder, RecordRoutedVolume);
            appendItem(stringBuilder, RecordNotional);

            return stringBuilder.ToString();
        }
    }
}
