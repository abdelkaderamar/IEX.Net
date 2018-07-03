using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace IEX.Api
{
    public class IexStatProvider : IexProvider
    {
        public static readonly string INTRADAY_FUNC = "/stats/intraday";

        public static readonly string INTRADAY_URL = BASE_URL + INTRADAY_FUNC;

        public IexIntradayStat RequestIexIntradayStat()
        {
            var json = Request(INTRADAY_URL).Result;

            if (!(json is JObject)) return null;

            var intradyStat = IexIntradayStat.FromJson((JObject)json);

            return intradyStat;
        }

    }
}
