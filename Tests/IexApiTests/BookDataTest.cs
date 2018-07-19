using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using IEX.Api.Data;

namespace IexApiTests
{
    [TestClass]
    public class BookDataTest
    {
        [TestMethod]
        public void BookDataFromJson()
        {
            var json = JsonConvert.DeserializeObject(
@"{
    ""YELP"": {
        ""bids"": [
            {
                ""price"": 63.09,
                ""size"": 300,
                ""timestamp"": 1494538496261
            },
        ],
        ""asks"": [
            {
                ""price"": 63.92,
                ""size"": 300,
                ""timestamp"": 1494538381896
            },
            {
                ""price"": 63.97,
                ""size"": 300,
                ""timestamp"": 1494538381885
            }
        ]
    }
}");
            Assert.IsNotNull(json);
            Assert.IsInstanceOfType(json, typeof(JObject));

            var yelpProperty = (json as JObject).First as JProperty;

            var bookData = BookData.FromJson(yelpProperty);
            Assert.IsNotNull(bookData);

            Assert.AreEqual("YELP", bookData.Symbol);

            Assert.IsNotNull(bookData.Bids);
            Assert.AreEqual(1, bookData.Bids.Count);
            {
                var bid0 = bookData.Bids[0];
                Assert.AreEqual(63.09m, bid0.Price);
                Assert.AreEqual(300, bid0.Size);
            }

            Assert.IsNotNull(bookData.Asks);
            Assert.AreEqual(2, bookData.Asks.Count);
            {
                var ask0 = bookData.Asks[0];
                Assert.AreEqual(63.92m, ask0.Price);
                Assert.AreEqual(300, ask0.Size);
            }
            {
                var ask1 = bookData.Asks[1];
                Assert.AreEqual(63.97m, ask1.Price);
                Assert.AreEqual(300, ask1.Size);
            }

        }

        [TestMethod]
        public void BookDataFromJson_Empty()
        {
            var json = JsonConvert.DeserializeObject(@"{}");

            Assert.IsNotNull(json);
            Assert.IsInstanceOfType(json, typeof(JObject));

            var jobject = json as JObject;
            var property = jobject.First as JProperty;
            Assert.IsNull(property);

            Assert.AreEqual(0, jobject.Count);
        }

    }
}
