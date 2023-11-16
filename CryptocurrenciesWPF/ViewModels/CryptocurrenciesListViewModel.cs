using CryptocurrenciesWPF.Commands;
using CryptocurrenciesWPF.Models;
using CryptocurrenciesWPF.Views;
using Newtonsoft.Json;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
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
        private string HTTP_PATH = $"https://api.coincap.io/v2/assets?limit=";

        private NavigatorViewModel navigatorViewModel;

        private Cryptocurrency selectedCryptocurrency;
        private int selectedNumber;
        private string searchText = "";

        public string SearchText
        {
            get { return searchText; }
            set
            {
                searchText = value;
                OnPropertyChanged(nameof(SearchText));
                UpdateCryptocurrenciesList();
            }
        }

        public Cryptocurrency SelectedCryptocurrency
        {
            get { return selectedCryptocurrency; }
            set
            {
                if (value != null)
                {
                    selectedCryptocurrency = null;
                    OnPropertyChanged(nameof(SelectedCryptocurrency));
                    navigatorViewModel.GoToProfilePageFromListPage(value);
                }
            }
        }

        public int SelectedNumber
        {
            get { return selectedNumber; }
            set
            {
                selectedNumber = value;
                UpdateCryptocurrenciesList().ContinueWith(x =>
                    { OnPropertyChanged(nameof(SelectedNumber)); });
            }
        }

        public ObservableCollection<Cryptocurrency> Cryptocurrencies { get; set; } 
            = new ObservableCollection<Cryptocurrency>();

        public List<int> Numbers { get; set; } = new List<int>();

        public CryptocurrenciesListViewModel(NavigatorViewModel navigatorViewModel)
        {
            this.navigatorViewModel = navigatorViewModel;
            SelectedNumber = 10;
            UpdateCryptocurrenciesList().ContinueWith(x => { CreateNumberList(); });
        }

        public async Task UpdateCryptocurrenciesList()
        {
            var responseQuery = await navigatorViewModel.Client
                .GetCryptocurrenciesListHTTP(selectedNumber.ToString(), searchText);

            if (!responseQuery.IsSuccessStatusCode) { return; }

            var responseValue = await responseQuery.Content.ReadAsStringAsync();
            var response = JsonConvert
                .DeserializeObject<JsonData<Cryptocurrency>>(responseValue);

            Cryptocurrencies.Clear();
            response?.Data?.ForEach(x =>
            {
                Cryptocurrencies.Add(x);
            });

            OnPropertyChanged(nameof(Cryptocurrencies));
        }


        public void CreateNumberList()
        {
            Numbers.Add(5);
            Numbers.Add(10);
            Numbers.Add(20);
            Numbers.Add(50);
            Numbers.Add(100);
            Numbers.Add(200);
            OnPropertyChanged(nameof(Numbers));
        }
    }
}
