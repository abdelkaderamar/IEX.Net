using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using IEX.Api.Data;

namespace IEX.Api
{
    public class IexStatProvider : IexAbstractProvider
    {
        public static readonly string INTRADAY_FUNC = "stats/intraday";
        public static readonly string RECENT_FUNC = "stats/recent";
        public static readonly string RECORD_FUNC = "stats/records";

        public static readonly string INTRADAY_URL = BASE_URL + INTRADAY_FUNC;
        public static readonly string RECENT_URL = BASE_URL + RECENT_FUNC;
        public static readonly string RECORD_URL = BASE_URL + RECORD_FUNC;

        public IexIntradayStat RequestIexIntradayStat()
        {
            var json = Request(INTRADAY_URL).Result;

            if (!(json is JObject)) return null;

            var intradyStat = IexIntradayStat.FromJson((JObject)json);

            return intradyStat;
        }

        public IEnumerable<HistoricalData> RequestIexRecentStat()
        {
            var json = Request(RECENT_URL).Result;

            if (!(json is JArray)) yield break;

            var jarray = (JArray)json;
            foreach (var histoDataJson in jarray.Children<JObject>())
            {
                yield return HistoricalData.FromJson(histoDataJson);
            }
        }

        public RecordData RequestRecordData()
        {
            var json = Request(RECORD_URL).Result;
            if (!(json is JObject)) return null;

            RecordData recordData = RecordData.FromJson((JObject)json);
            return recordData;
        }

        public void IexHistoricalSummary()
        {
            throw new NotImplementedException();
        }

        public void IexHistoricalDaily()
        {
            throw new NotImplementedException();
        }

    }
}
