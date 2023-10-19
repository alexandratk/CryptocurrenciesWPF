using CryptocurrenciesWPF.Models;
using CryptocurrenciesWPF.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace CryptocurrenciesWPF.ViewModels
{
    public class CryptocurrenciesListViewModel : BaseViewModel
    {
        private NavigatorViewModel navigatorViewModel;

        private HttpClient client = new HttpClient();

        private Cryptocurrency selectedCryptocurrency;
        public Cryptocurrency SelectedCryptocurrency
        {
            get { return selectedCryptocurrency; }
            set
            {
                selectedCryptocurrency = value;
                navigatorViewModel.GoToProfilePageFromListPage(selectedCryptocurrency);
                OnPropertyChanged("SelectedCryptocurrency");
            }
        }

        public ObservableCollection<Cryptocurrency> Cryptocurrencies { get; set; }

        public CryptocurrenciesListViewModel(NavigatorViewModel navigatorViewModel)
        {
            this.navigatorViewModel = navigatorViewModel;
            UpdateList();
        }

        public void UpdateList()
        {
            var response =  client.GetFromJsonAsync<JsonData>("https://api.coincap.io/v2/assets").Result;
            Cryptocurrencies = new ObservableCollection<Cryptocurrency>(response.Data);
        }
    }
}
