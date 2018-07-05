using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IEX.Api.Data;
using Newtonsoft.Json.Linq;

namespace IEX.Api
{
    public class IexMarketDataProvider : IexAbstractProvider
    {
        public static readonly string LAST_FUNC = "/tops/last";

        public static readonly string LAST_URL = BASE_URL + LAST_FUNC;

        public IEnumerable<LastData> RequestLast()
        {
            return RequestLast(LAST_URL);
        }

        public void RequestLast(string [] symbols)
        {
            throw new NotImplementedException();
        }

        protected IEnumerable<LastData> RequestLast(string url)
        {
            var json = Request(url).Result;

            if (!(json is JArray)) yield break;

            JArray jarray = (JArray)json;

            foreach (var child in jarray.Children<JObject>())
            {
                var lastData = LastData.FromJson(child);
                if (lastData != null) yield return lastData;
            }
        }

    }
}
