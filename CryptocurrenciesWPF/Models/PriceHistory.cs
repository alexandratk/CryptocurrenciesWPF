using CryptocurrenciesWPF.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptocurrenciesWPF.Models
{
    public class PriceHistory
    {
        [JsonConverter(typeof(DoubleJsonConverter))]
        public double PriceUsd { get; set; }
        public long Time { get; set; }
        public DateTime TimeDateTime { get; set; }
    }
}
