// Copyright (c) Abdelkader Amar. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.


﻿using System;
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
        public static readonly string TOPS_FUNC = "/tops";
        public static readonly string LAST_FUNC = "/tops/last";
        public static readonly string BOOKS_FUNC = "/deep/book?";
        public static readonly string TRADES_FUNC = "/deep/trades?";

        public static readonly string TOPS_URL = BASE_URL + TOPS_FUNC;
        public static readonly string LAST_URL = BASE_URL + LAST_FUNC;
        public static readonly string BOOKS_URL = BASE_URL + BOOKS_FUNC;
        public static readonly string TRADES_URL = BASE_URL + TRADES_FUNC;

        public static readonly string SYMBOLS_ARG = "symbols=";

        public IEnumerable<TopsData> RequestTops()
        {
            return RequestTops(TOPS_URL);
        }

        public IEnumerable<TopsData> RequestTops(string[] symbols)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<LastData> RequestLast()
        {
            return RequestLast(LAST_URL);
        }

        public void RequestLast(string [] symbols)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<BookData> RequestBook(string[] symbols)
        {
            var url = BOOKS_URL + SYMBOLS_ARG + string.Join(",", symbols);
            var json = Request(url).Result;

            if (json == null) yield break;
            if (!(json is JObject)) yield break;

            JObject jobject = json as JObject;

            foreach (var bookProperty in jobject.Children<JProperty>())
            {
                BookData book = BookData.FromJson(bookProperty);
                if (book != null) yield return book;
            }

        }

        public IEnumerable<TradeData> RequestTrades(string[] symbols)
        {
            var url = TRADES_URL + SYMBOLS_ARG + string.Join(",", symbols);
            var json = Request(url).Result;

            if (json == null) yield break;
            if (!(json is JObject)) yield break;

            JObject jobject = json as JObject;

            foreach (var tradeProperty in jobject.Children<JProperty>())
            {
                TradeData tradeData = TradeData.FromJson(tradeProperty);
                if (tradeData != null) yield return tradeData;
            }

        }

        protected IEnumerable<TopsData> RequestTops(string url)
        {
            var json = Request(url).Result;

            if (!(json is JArray)) yield break;

            JArray jarray = (JArray)json;

            foreach (var child in jarray.Children<JObject>())
            {
                var topsData = TopsData.FromJson(child);
                if (topsData != null) yield return topsData;
            }
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
