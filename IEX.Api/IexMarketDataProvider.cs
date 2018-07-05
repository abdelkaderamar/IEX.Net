using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IEX.Api.Data;
using Newtonsoft.Json.Linq;
using Quobject.SocketIoClientDotNet.Client;

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

            //var socket = IO.Socket("https://ws-api.iextrading.com/1.0/last");
            //long count = 0;
            //socket.On(Socket.EVENT_CONNECT, () =>
            //{
            //    socket.Emit("subscribe", "ibm,fb,aapl");

            //});

            //socket.On(Socket.EVENT_MESSAGE, (data) =>
            //{
            //    Console.WriteLine("{1} Receiving a message {0}", data, ++count);
            //});
        }

    }
}
