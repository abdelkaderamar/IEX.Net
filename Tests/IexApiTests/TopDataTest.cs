// Copyright (c) Abdelkader Amar. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.


﻿using System;
using System.Collections.Generic;
using IEX.Api.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace IexApiTests
{
    [TestClass]
    public class TopDataTest
    {
        [TestMethod]
        public void FromJson()
        {
            var json = JsonConvert.DeserializeObject("[{\"symbol\":\"SNAP\",\"sector\":\"softwareservices\",\"securityType\":\"commonstock\",\"bidPrice\":0,\"bidSize\":0,\"askPrice\":0,\"askSize\":0,\"lastUpdated\":1531857600000,\"lastSalePrice\":13.405,\"lastSaleSize\":100,\"lastSaleTime\":1531857595826,\"volume\":217309,\"marketPercent\":0.01366},{\"symbol\":\"FB\",\"sector\":\"softwareservices\",\"securityType\":\"commonstock\",\"bidPrice\":0,\"bidSize\":0,\"askPrice\":0,\"askSize\":0,\"lastUpdated\":1531857600450,\"lastSalePrice\":209.99,\"lastSaleSize\":200,\"lastSaleTime\":1531857599424,\"volume\":297684,\"marketPercent\":0.01983},{\"symbol\":\"AIG+\",\"sector\":\"n/a\",\"securityType\":\"warrant\",\"bidPrice\":0,\"bidSize\":0,\"askPrice\":0,\"askSize\":0,\"lastUpdated\":1531857600000,\"lastSalePrice\":16.25,\"lastSaleSize\":200,\"lastSaleTime\":1531857591509,\"volume\":6400,\"marketPercent\":0.05478}]");
            Assert.IsNotNull(json);
            Assert.IsInstanceOfType(json, typeof(JArray));

            JArray jarray = json as JArray;

            List<JObject> list = new List<JObject>(jarray.Children<JObject>());
            Assert.AreEqual(3, list.Count);

            var snap = TopsData.FromJson(list[0]);
            Assert.IsNotNull(snap);
            Assert.AreEqual("SNAP", snap.Symbol);
            Assert.AreEqual("softwareservices", snap.Sector);
            Assert.AreEqual("commonstock", snap.SecurityType);
            Assert.AreEqual(0m, snap.BidPrice);
            Assert.AreEqual(0, snap.BidSize);
            Assert.AreEqual(0m, snap.AskPrice);
            Assert.AreEqual(0, snap.AskSize);
            Assert.AreEqual(13.405m, snap.LastSalePrice);
            Assert.AreEqual(100, snap.LastSaleSize);
            Assert.AreEqual(217309, snap.Volume);
            Assert.AreEqual(0.01366, snap.MarketPercent);
        }
    }
}
