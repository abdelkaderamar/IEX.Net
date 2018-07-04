using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using IEX.Api.Data;

namespace IEX.Api
{
    public class ReferenceDataProvider : IexAbstractProvider
    {

        public static readonly string SYMBOLS_FUNC = "ref-data/symbols";

        public static readonly string SYMBOLS_URL = BASE_URL + SYMBOLS_FUNC;

        public IEnumerable<Symbol> RequestSymbols()
        {
            var json = Request(SYMBOLS_URL).Result;

            if (!(json is JArray)) yield break;

            JArray jarray = (JArray)json;

            foreach (var symbol in jarray.Children<JObject>())
            {
                yield return Symbol.FromJson(symbol);
            }
        }
    }
}
