using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace IEX.Api.Data
{
    public class LastData
    {
        #region JSON keys
        public static readonly string SYMBOL_KEY = "symbol";
        public static readonly string PRICE_KEY = "price";
        public static readonly string SIZE_KEY = "size";
        public static readonly string TIME_KEY = "time";
        #endregion

        public LastData(string symbol)
        {
            Symbol = symbol;
        }

        public string Symbol { get; }

        public decimal Price { get; set; }

        public long Size { get; set; }

        public DateTime Time { get; set; }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder(Symbol).Append(Environment.NewLine).
                Append("\tLast Price = ").Append(Price).Append(Environment.NewLine).
                Append("\tSize       = ").Append(Size).Append(Environment.NewLine).
                Append("\tTime       = ").Append(Time).Append(Environment.NewLine);
            return stringBuilder.ToString();
        }

        public static LastData FromJson(JObject json)
        {
            string symbol = JsonHelper.GetValue(json, SYMBOL_KEY);
            LastData last = new LastData(symbol)
            {
                Price = JsonHelper.GetDecimalValue(json, PRICE_KEY),
                Size = JsonHelper.GetLongValue(json, SIZE_KEY),
                Time = JsonHelper.GetDateTimeValue(json, TIME_KEY),
            };
            return last;
        }
    }
}
