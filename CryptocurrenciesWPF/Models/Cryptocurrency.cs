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
        public decimal Supply { get; set; }
        public string SupplyString { get; set; }
        public string MaxSupply { get; set; }
        public decimal MarketCapUsd { get; set; }
        public string MarketCapUsdString { get; set; }
        public decimal VolumeUsd24Hr { get; set; }
        public string VolumeUsd24HrString { get; set; }
        public decimal PriceUsd { get; set; }
        public string PriceUsdString { get; set; }
        public decimal ChangePercent24Hr { get; set; }
        public string ChangePercent24HrString { get; set; }
        public string Vwap24Hr { get; set; }
    }
}
