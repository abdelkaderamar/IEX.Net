using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace IEX.Api
{
    public enum RequestType { Daily, DailyAdjusted, Weekly, WeeklyAdjusted, Monthly, MonthlyAdjusted }

    public abstract class IexAbstractProvider
    {
        public static readonly string BASE_URL = "https://api.iextrading.com/1.0/";

        private HttpClient _httpClient;

        public IexAbstractProvider()
        {
            _httpClient = new HttpClient();
        }

        public async Task<JContainer> Request(String url)
        {
            string content = await RequestAsync(url);
            var obj = JsonConvert.DeserializeObject(content);
            if (!(obj is JContainer)) return null;
            JContainer json = (JContainer)obj;
            return json;
        }

        public string RequestSync(String url)
        {
            Task<string> task = RequestAsync(url);
            var result = task.Result;
            return result;
        }

        public async Task<string> RequestAsync(String url)
        {
            using (HttpResponseMessage response = await _httpClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    return content;
                }
                return string.Empty;
            }
        }


    }
}