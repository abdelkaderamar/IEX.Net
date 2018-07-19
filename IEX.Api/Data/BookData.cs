using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace IEX.Api.Data
{
    /**
     * 
     {
    "YELP": {
        "bids": [
            {
                "price": 63.09,
                "size": 300,
                "timestamp": 1494538496261
            },
        ],
        "asks": [
            {
                "price": 63.92,
                "size": 300,
                "timestamp": 1494538381896
            },
            {
                "price": 63.97,
                "size": 300,
                "timestamp": 1494538381885
            }
          ]
        }
        }
    */
    public enum BookPriceType { BID, ASK };

    public class BookData : IexData
    {
        public static readonly string BIDS_KEY = "bids";
        public static readonly string ASKS_KEY = "asks";

        public BookData(string symbol) : base(symbol)
        {
            Bids = new List<BookPrice>();
            Asks = new List<BookPrice>();
        }

        public List<BookPrice> Bids { get; }

        public List<BookPrice> Asks { get; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("Book of {0}", Symbol).Append(Environment.NewLine).
                Append("Bids:").Append(Environment.NewLine);
            foreach (var bid in Bids) sb.AppendFormat("\t{0}", bid.ToString());
            sb.Append("Asks:").Append(Environment.NewLine);
            foreach (var ask in Asks) sb.AppendFormat("\t{0}", ask.ToString());
            return sb.ToString();
        }

        public static BookData FromJson(JProperty json)
        {
            var symbol = json.Name;
            BookData bookData = new BookData(symbol);
            var value = json.Value as JObject;
            if (value != null)
            {
                Action<List<BookPrice>, BookPriceType, JArray> parsePrices = (prices, priceType, jarray) =>
                {
                    foreach (var priceJson in jarray.Children<JObject>())
                    {
                        var price = BookPrice.FromJson(priceJson, priceType);
                        if (price != null) { prices.Add(price); }
                    }
                };

                var bids = value.GetValue(BIDS_KEY) as JArray;
                parsePrices(bookData.Bids, BookPriceType.BID, bids);

                var asks = value.GetValue(ASKS_KEY) as JArray;
                parsePrices(bookData.Asks, BookPriceType.ASK, asks);
            }
            return bookData;
        }

    }

    public class BookPrice
    {
        public static readonly string PRICE_KEY = "price";
        public static readonly string SIZE_KEY = "size";
        public static readonly string TIMESTAMP_KEY = "timestamp";

        public BookPrice(BookPriceType type)
        {
            Type = type;
        }

        public BookPriceType Type { get; set; }

        public decimal Price { get; set; }

        public long Size { get; set; }

        public DateTime Timestamp { get; set; }

        public override string ToString()
        {
            return Size + " @ " + Price + " / " + Timestamp;
        }

        public static BookPrice FromJson(JObject json, BookPriceType type)
        {
            BookPrice bookPrice = new BookPrice(type)
            {
                Price = JsonHelper.GetDecimalValue(json, PRICE_KEY),
                Size = JsonHelper.GetLongValue(json, SIZE_KEY),
                Timestamp = JsonHelper.GetDateTimeValue(json, TIMESTAMP_KEY)
            };
            return bookPrice;
        }
    }
}
