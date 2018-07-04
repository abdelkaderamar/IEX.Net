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
        public static readonly string HISTORICAL_FUNC = "stats/historical";
        public static readonly string HISTORICAL_DAILY_FUNC = "stats/historical/daily";

        public static readonly string INTRADAY_URL = BASE_URL + INTRADAY_FUNC;
        public static readonly string RECENT_URL = BASE_URL + RECENT_FUNC;
        public static readonly string RECORD_URL = BASE_URL + RECORD_FUNC;
        public static readonly string HISTORICAL_URL = BASE_URL + HISTORICAL_FUNC;
        public static readonly string HISTORICAL_DAILY_URL = BASE_URL + HISTORICAL_DAILY_FUNC;

        public static readonly int MAX_HISTO_DAILY = 90;

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

        public HistoSummaryData RequestHistoricalSummary()
        {
            return RequestHistoricalSummary(HISTORICAL_URL);
        }

        public HistoSummaryData RequestHistoricalSummary(DateTime date)
        {
            var urlArgs = "?date=" + date.ToString("yyyyMM");
            var url = HISTORICAL_URL + urlArgs;

            return RequestHistoricalSummary(url);
        }

        protected HistoSummaryData RequestHistoricalSummary(string url)
        { 
            var json = Request(url).Result;

            if (!(json is JArray)) return null;

            JArray jarray = (JArray)json;
            if (jarray.Count == 0) return null;

            var childs = jarray.Children<JObject>();
            var child = childs.First();
            HistoSummaryData histoSummary = HistoSummaryData.FromJson(child);

            return histoSummary;
        }

        public HistoricalData RequestHistoricalDaily()
        {
            var histoDaily = RequestHistoricalDaily(HISTORICAL_DAILY_URL);
            if (histoDaily.Count() == 0) return null;

            return histoDaily.First();
        }

        public IEnumerable<HistoricalData> RequestHistoricalDaily(DateTime date)
        {
            var urlArgs = "?date=" + date.ToString("yyyyMM");
            var url = HISTORICAL_DAILY_URL + urlArgs;

            return RequestHistoricalDaily(url);
        }

        public IEnumerable<HistoricalData> RequestHistoricalDaily(int last)
        {
            last = Math.Min(last, MAX_HISTO_DAILY);

            var urlArgs = "?last=" + last;
            var url = HISTORICAL_DAILY_URL + urlArgs;

            return RequestHistoricalDaily(url);
        }

        protected IEnumerable<HistoricalData> RequestHistoricalDaily(string url)
        {
            var json = Request(url).Result;

            if (!(json is JArray)) yield break;

            var jarray = (JArray)json;
            foreach (var histoDataJson in jarray.Children<JObject>())
            {
                yield return HistoricalData.FromJson(histoDataJson);
            }
        }


    }
}
