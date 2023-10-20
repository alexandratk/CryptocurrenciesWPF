using CryptocurrenciesWPF.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CryptocurrenciesWPF.ViewModels
{
    public class CryptocurrencyProfileViewModel : BaseViewModel
    {
        private NavigatorViewModel navigatorViewModel;

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

        public ObservableCollection<Market> Markets { get; set; }

        public CryptocurrencyProfileViewModel(NavigatorViewModel navigatorViewModel)
        {
            this.navigatorViewModel = navigatorViewModel;
            currentCryptocurrency = new Cryptocurrency();
        }

        public async Task UpdateMarketsList()
        {
            string currentId = currentCryptocurrency.Id;
            var response = await navigatorViewModel.HTTPClient.GetFromJsonAsync<JsonData<Market>>("https://api.coincap.io/v2/assets/" + currentId + "/markets");
            Markets = new ObservableCollection<Market>(response.Data);
            OnPropertyChanged("Markets");
        }
    }
}
