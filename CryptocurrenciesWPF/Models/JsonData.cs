using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptocurrenciesWPF.Models
{
    public class JsonData<T>
    {
        public List<T> Data { get; set; } = new List<T>();
    }
}
