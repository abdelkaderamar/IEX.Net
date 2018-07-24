// Copyright (c) Abdelkader Amar. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.


﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using IEX.Api.Data;

namespace IexApiTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void FromJson()
        {
            var json = JsonConvert.DeserializeObject("{\"symbol\":\"SNAP\",\"marketPercent\":0,\"volume\":0,\"lastSalePrice\":0,\"lastSaleSize\":0,\"lastSaleTime\":0,\"lastUpdated\":0,\"bids\":[],\"asks\":[],\"systemEvent\":{},\"trades\":[{\"price\":13.405,\"size\":100,\"tradeId\":603927365,\"isISO\":false,\"isOddLot\":false,\"isOutsideRegularHours\":false,\"isSinglePriceCross\":false,\"isTradeThroughExempt\":false,\"timestamp\":1531857595826},{\"price\":13.4,\"size\":900,\"tradeId\":603235654,\"isISO\":true,\"isOddLot\":false,\"isOutsideRegularHours\":false,\"isSinglePriceCross\":false,\"isTradeThroughExempt\":false,\"timestamp\":1531857593460},{\"price\":13.4,\"size\":900,\"tradeId\":603227109,\"isISO\":false,\"isOddLot\":false,\"isOutsideRegularHours\":false,\"isSinglePriceCross\":false,\"isTradeThroughExempt\":false,\"timestamp\":1531857593420},{\"price\":13.4,\"size\":400,\"tradeId\":603226100,\"isISO\":false,\"isOddLot\":false,\"isOutsideRegularHours\":false,\"isSinglePriceCross\":false,\"isTradeThroughExempt\":false,\"timestamp\":1531857593412},{\"price\":13.4,\"size\":100,\"tradeId\":603226093,\"isISO\":false,\"isOddLot\":false,\"isOutsideRegularHours\":false,\"isSinglePriceCross\":false,\"isTradeThroughExempt\":false,\"timestamp\":1531857593412},{\"price\":13.4,\"size\":100,\"tradeId\":603226086,\"isISO\":false,\"isOddLot\":false,\"isOutsideRegularHours\":false,\"isSinglePriceCross\":false,\"isTradeThroughExempt\":false,\"timestamp\":1531857593412},{\"price\":13.4,\"size\":500,\"tradeId\":603225931,\"isISO\":false,\"isOddLot\":false,\"isOutsideRegularHours\":false,\"isSinglePriceCross\":false,\"isTradeThroughExempt\":false,\"timestamp\":1531857593411},{\"price\":13.4,\"size\":1072,\"tradeId\":603225882,\"isISO\":true,\"isOddLot\":false,\"isOutsideRegularHours\":false,\"isSinglePriceCross\":false,\"isTradeThroughExempt\":false,\"timestamp\":1531857593411},{\"price\":13.4,\"size\":700,\"tradeId\":603225846,\"isISO\":false,\"isOddLot\":false,\"isOutsideRegularHours\":false,\"isSinglePriceCross\":false,\"isTradeThroughExempt\":false,\"timestamp\":1531857593411},{\"price\":13.4,\"size\":628,\"tradeId\":603225748,\"isISO\":false,\"isOddLot\":false,\"isOutsideRegularHours\":false,\"isSinglePriceCross\":false,\"isTradeThroughExempt\":false,\"timestamp\":1531857593411},{\"price\":13.4,\"size\":100,\"tradeId\":603225738,\"isISO\":false,\"isOddLot\":false,\"isOutsideRegularHours\":false,\"isSinglePriceCross\":false,\"isTradeThroughExempt\":false,\"timestamp\":1531857593411},{\"price\":13.4,\"size\":1072,\"tradeId\":603225729,\"isISO\":false,\"isOddLot\":false,\"isOutsideRegularHours\":false,\"isSinglePriceCross\":false,\"isTradeThroughExempt\":false,\"timestamp\":1531857593411},{\"price\":13.395,\"size\":300,\"tradeId\":602933933,\"isISO\":false,\"isOddLot\":false,\"isOutsideRegularHours\":false,\"isSinglePriceCross\":false,\"isTradeThroughExempt\":false,\"timestamp\":1531857591175},{\"price\":13.395,\"size\":100,\"tradeId\":602933926,\"isISO\":false,\"isOddLot\":false,\"isOutsideRegularHours\":false,\"isSinglePriceCross\":false,\"isTradeThroughExempt\":false,\"timestamp\":1531857591175},{\"price\":13.4,\"size\":2028,\"tradeId\":602795983,\"isISO\":true,\"isOddLot\":false,\"isOutsideRegularHours\":false,\"isSinglePriceCross\":false,\"isTradeThroughExempt\":false,\"timestamp\":1531857590352},{\"price\":13.4,\"size\":900,\"tradeId\":602795961,\"isISO\":false,\"isOddLot\":false,\"isOutsideRegularHours\":false,\"isSinglePriceCross\":false,\"isTradeThroughExempt\":false,\"timestamp\":1531857590352},{\"price\":13.395,\"size\":100,\"tradeId\":600891507,\"isISO\":false,\"isOddLot\":false,\"isOutsideRegularHours\":false,\"isSinglePriceCross\":false,\"isTradeThroughExempt\":false,\"timestamp\":1531857579374},{\"price\":13.4,\"size\":100,\"tradeId\":600891020,\"isISO\":true,\"isOddLot\":false,\"isOutsideRegularHours\":false,\"isSinglePriceCross\":false,\"isTradeThroughExempt\":false,\"timestamp\":1531857579367},{\"price\":13.395,\"size\":100,\"tradeId\":599936095,\"isISO\":false,\"isOddLot\":false,\"isOutsideRegularHours\":false,\"isSinglePriceCross\":false,\"isTradeThroughExempt\":false,\"timestamp\":1531857573217},{\"price\":13.395,\"size\":100,\"tradeId\":599132909,\"isISO\":false,\"isOddLot\":false,\"isOutsideRegularHours\":false,\"isSinglePriceCross\":false,\"isTradeThroughExempt\":false,\"timestamp\":1531857569622}],\"tradeBreaks\":[]}");

            Assert.IsNotNull(json);
            Assert.IsInstanceOfType(json, typeof(JObject));

            var deepData = DeepData.FromJson(json as JObject);
            Assert.IsNotNull(deepData);

            Assert.AreEqual("SNAP", deepData.Symbol);
            Assert.AreEqual(0.0, deepData.MarketPercent);
            Assert.AreEqual(0, deepData.Volume);
            Assert.AreEqual(0m, deepData.LastSalePrice);
            Assert.AreEqual(0, deepData.LastSaleSize);
        }
    }
}
