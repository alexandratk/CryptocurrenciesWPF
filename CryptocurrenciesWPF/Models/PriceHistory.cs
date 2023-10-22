using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptocurrenciesWPF.Models
{
    public class PriceHistory
    {
        public string PriceUsd { get; set; }
        public ulong Time { get; set; }
        public DateTime TimeDateTime { get; set; }
    }
}
