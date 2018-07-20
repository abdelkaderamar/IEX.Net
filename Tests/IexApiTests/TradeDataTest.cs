using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using IEX.Api.Data;

namespace IexApiTests
{
    [TestClass]
    public class TradeDataTest
    {
        [TestMethod]
        public void FromJson()
        {
        }

        [TestMethod]
        public void TradeDataFromJson_Empty()
        {
            var json = JsonConvert.DeserializeObject(
@" {
""SNAP"": [
    {
      ""price"": 156.1,
      ""size"": 100,
      ""tradeId"": 517341294,
      ""isISO"": false,
      ""isOddLot"": false,
      ""isOutsideRegularHours"": false,
      ""isSinglePriceCross"": false,
      ""isTradeThroughExempt"": false,
      ""timestamp"": 1000
    }
  ]
}");

            Assert.IsNotNull(json);
            Assert.IsInstanceOfType(json, typeof(JObject));

            var yelpProperty = (json as JObject).First as JProperty;

            var tradeData = TradeData.FromJson(yelpProperty);
            Assert.IsNotNull(tradeData);

            Assert.AreEqual("SNAP", tradeData.Symbol);

            Assert.IsNotNull(tradeData.Trades);
            Assert.AreEqual(1, tradeData.Trades.Count);

            var trade0 = tradeData.Trades[0];
            Assert.IsNotNull(trade0);
            Assert.AreEqual(156.1m, trade0.Price);
            Assert.AreEqual(100, trade0.Size);
            Assert.AreEqual("517341294", trade0.TradeId);
            Assert.AreEqual(false, trade0.IsIso);
            Assert.AreEqual(false, trade0.IsOddLot);
            Assert.AreEqual(false, trade0.IsOutsideRegularHours);
            Assert.AreEqual(false, trade0.IsSinglePriceCross);
            Assert.AreEqual(false, trade0.IsTradeThroughExempt);
            Assert.AreEqual(new DateTime(1970, 1, 1, 1, 0, 1, DateTimeKind.Local), trade0.Timestamp);
        }

    }
}
