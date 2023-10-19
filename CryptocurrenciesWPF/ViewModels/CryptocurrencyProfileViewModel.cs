using CryptocurrenciesWPF.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CryptocurrenciesWPF.ViewModels
{
    public class CryptocurrencyProfileViewModel : BaseViewModel
    {
        private Cryptocurrency currentCryptocurrency;
        public Cryptocurrency CurrentCryptocurrency
        {
            get { return currentCryptocurrency; }
            set
            {
                currentCryptocurrency = value;
                OnPropertyChanged("CurrentCryptocurrency");
            }
        }

    }
}
