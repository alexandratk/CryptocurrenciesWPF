using CryptocurrenciesWPF.Models;
using CryptocurrenciesWPF.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.RegularExpressions;
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
        private int selectedNumber = 10;

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

        public int SelectedNumber
        {
            get { return selectedNumber; }
            set
            {
                selectedNumber = value;
                UpdateList();
                OnPropertyChanged("SelectedNumber");
            }
        }

        public ObservableCollection<Cryptocurrency> Cryptocurrencies { get; set; }

        public ObservableCollection<int> Numbers { get; set; } = new ObservableCollection<int>();

        public CryptocurrenciesListViewModel(NavigatorViewModel navigatorViewModel)
        {
            this.navigatorViewModel = navigatorViewModel;
            UpdateList();
            CreateNumberList();
        }

        public void UpdateList()
        {
            var response = client.GetFromJsonAsync<JsonData>("https://api.coincap.io/v2/assets?limit="+selectedNumber.ToString()).Result;
            Cryptocurrencies = new ObservableCollection<Cryptocurrency>(response.Data);
            OnPropertyChanged("Cryptocurrencies");
        }

        public void CreateNumberList()
        {
            for (int i = 1; i <= 200; i++)
            {
                Numbers.Add(i);
            }
        }
    }
}
