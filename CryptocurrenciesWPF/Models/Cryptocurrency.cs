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
        public string Supply { get; set; }
        public string MaxSupply { get; set; }
        public string MarketCapUsd { get; set; }
        public string VolumeUsd24Hr { get; set; }
        public string PriceUsd { get; set; }
        public string ChangePercent24Hr { get; set; }
        public string Vwap24Hr { get; set; }

        public Cryptocurrency() { }

        public Cryptocurrency(Cryptocurrency cryptocurrency)
        {
            Id = cryptocurrency.Id;
            Rank = cryptocurrency.Rank;
            Symbol = cryptocurrency.Symbol;
            Name = cryptocurrency.Name;
            Supply = cryptocurrency.Supply;
            MaxSupply = cryptocurrency.MaxSupply;
            MarketCapUsd = cryptocurrency.MarketCapUsd;
            VolumeUsd24Hr = cryptocurrency.VolumeUsd24Hr;
            PriceUsd = cryptocurrency.PriceUsd;
            ChangePercent24Hr = cryptocurrency.ChangePercent24Hr;
            Vwap24Hr = cryptocurrency.Vwap24Hr;
        }
    }
}
