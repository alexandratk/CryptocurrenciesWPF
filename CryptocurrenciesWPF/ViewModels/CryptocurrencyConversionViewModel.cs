using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptocurrenciesWPF.ViewModels
{
    public class CryptocurrencyConversionViewModel : BaseViewModel
    {
        private NavigatorViewModel navigatorViewModel;

        public CryptocurrencyConversionViewModel(NavigatorViewModel navigatorViewModel) 
        {
            this.navigatorViewModel = navigatorViewModel;
        }
    }
}
