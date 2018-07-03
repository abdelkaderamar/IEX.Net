using System;
using System.Collections.Generic;
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
            public T Avg30Value { get; set; }
        } // end class RecordItem

        public RecordItem<long> VolumeRecord { get; set; }

        public RecordItem<long> RecordSymbolsTraded { get; set; }

        public RecordItem<long> RecordRoutedVolume { get; set; }

        public RecordItem<decimal> RecordNotional { get; set; }

        public RecordData FromJson(JObject json)
        {
            RecordData recordData = new RecordData();

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
            appendItem(stringBuilder, VolumeRecord);
            appendItem(stringBuilder, RecordNotional);

            return stringBuilder.ToString();
        }
    }
}
