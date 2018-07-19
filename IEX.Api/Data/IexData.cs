using System;

namespace IEX.Api.Data
{
    public class IexData
    {
        public IexData(string symbol)
        {
            Symbol = symbol;
        }

        public String Symbol { get; }
    }
}
