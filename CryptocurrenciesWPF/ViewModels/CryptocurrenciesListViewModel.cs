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
                OnPropertyChanged("SearchText");
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
                    OnPropertyChanged("SelectedCryptocurrency");
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
                UpdateCryptocurrenciesList().ContinueWith(x => { OnPropertyChanged("SelectedNumber"); });
            }
        }

        public ObservableCollection<Cryptocurrency> Cryptocurrencies { get; set; } = new ObservableCollection<Cryptocurrency>();

        public List<int> Numbers { get; set; } = new List<int>();

        public CryptocurrenciesListViewModel(NavigatorViewModel navigatorViewModel)
        {
            this.navigatorViewModel = navigatorViewModel;
            SelectedNumber = 10;
            UpdateCryptocurrenciesList().ContinueWith(x => { CreateNumberList(); });
        }

        public async Task UpdateCryptocurrenciesList()
        {
            var responseQuery = await navigatorViewModel.HTTPClient.GetAsync("https://api.coincap.io/v2/assets?limit="+selectedNumber.ToString() + "&search=" + searchText);

            if (!responseQuery.IsSuccessStatusCode) { return; }

            var responseValue = await responseQuery.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<JsonData<Cryptocurrency>>(responseValue);

            Cryptocurrencies.Clear();
            response?.Data?.ForEach(x =>
            {

                x.SupplyString = x.Supply.ToString("C2", CultureInfo.CreateSpecificCulture("en"));
                x.MarketCapUsdString = x.MarketCapUsd.ToString("C2", CultureInfo.CreateSpecificCulture("en"));
                x.VolumeUsd24HrString = x.VolumeUsd24Hr.ToString("C2", CultureInfo.CreateSpecificCulture("en"));
                x.PriceUsdString = x.PriceUsd.ToString("C2", CultureInfo.CreateSpecificCulture("en"));
                x.ChangePercent24HrString = x.ChangePercent24Hr.ToString("P", CultureInfo.InvariantCulture);
                if (x.ChangePercent24Hr > 0)
                {
                    x.ChangePercent24HrString = "+" + x.ChangePercent24HrString;
                }

                Cryptocurrencies.Add(x);
            });

            OnPropertyChanged("Cryptocurrencies");
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
