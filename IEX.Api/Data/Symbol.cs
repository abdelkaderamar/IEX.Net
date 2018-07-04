using System;
using System.Globalization;
using System.Text;
using Newtonsoft.Json.Linq;

namespace IEX.Api.Data
{
    /*
            "symbol": "A",
            "name": "Agilent Technologies Inc.",
            "date": "2018-07-03",
            "isEnabled": true,
            "type": "cs",
            "iexId": "2"
    */
    public class Symbol
    {
        #region JSON keys
        public static readonly string SYMBOL_KEY = "symbol";
        public static readonly string NAME_KEY = "name";
        public static readonly string DATE_KEY = "date";
        public static readonly string ISENABLED_KEY = "isEnabled";
        public static readonly string TYPE_KEY = "type";
        public static readonly string IEXID_KEY = "iexId";
        #endregion

        public Symbol(string inet)
        {
            INET = inet;
        }

        public string INET { get; }

        public string Name { get; set; }

        public DateTime Date { get; set; }

        public bool IsEnabled { get; set; }

        public string Type { get; set; }

        public long IexId { get; set; }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();
            Action<string, object> appendVal = (label, val) =>
            {
                stringBuilder.Append(label).Append(val).Append(Environment.NewLine);
            };
            appendVal("Name : ", Name);
            appendVal("\tSymbol  : ", INET);
            appendVal("\tDate    : ", Date);
            appendVal("\tEnabled : ", IsEnabled);
            appendVal("\tType    : ", Type);
            appendVal("\tIEX Id  : ", IexId);
            return stringBuilder.ToString();
        }

        public static Symbol FromJson(JObject json)
        {
            string inet = JsonHelper.GetValue(json, SYMBOL_KEY);
            Symbol symbol = new Symbol(inet)
            {
                Name = JsonHelper.GetValue(json, NAME_KEY),
                Date = DateTime.ParseExact(JsonHelper.GetValue(json, DATE_KEY), "yyyy-MM-dd", CultureInfo.InvariantCulture),
                IsEnabled = JsonHelper.GetBooleanValue(json, ISENABLED_KEY),
                Type = JsonHelper.GetValue(json, TYPE_KEY),
                IexId = JsonHelper.GetLongValue(json, IEXID_KEY)
            };
            return symbol;
        }
    }
}
