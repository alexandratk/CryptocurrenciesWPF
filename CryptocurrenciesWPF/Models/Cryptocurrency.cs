using CryptocurrenciesWPF.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptocurrenciesWPF.Models
{
    public class Cryptocurrency
    {
        public string Id { get; set; }
        public string Rank { get; set; }
        public string Symbol { get; set; }
        public string Name { get; set; }
        [JsonConverter(typeof(DoubleJsonConverter))]
        public double Supply { get; set; }
        public string MaxSupply { get; set; }
        [JsonConverter(typeof(DoubleJsonConverter))]
        public double MarketCapUsd { get; set; }
        [JsonConverter(typeof(DoubleJsonConverter))]
        public double VolumeUsd24Hr { get; set; }
        [JsonConverter(typeof(DoubleJsonConverter))]
        public double PriceUsd { get; set; }
        [JsonConverter(typeof(DoubleJsonConverter))]
        public double ChangePercent24Hr { get; set; }
        public string Vwap24Hr { get; set; }
    }
}
