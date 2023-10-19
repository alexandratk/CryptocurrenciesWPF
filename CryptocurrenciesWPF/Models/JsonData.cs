using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptocurrenciesWPF.Models
{
    public class JsonData
    {
        public List<Cryptocurrency> Data { get; set; } = new List<Cryptocurrency>();
    }
}
