using CryptocurrenciesWPF.Commands;
using CryptocurrenciesWPF.Models;
using CryptocurrenciesWPF.Views;
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
             //   OnPropertyChanged("SelectedNumber");
            }
        }

        public ObservableCollection<Cryptocurrency> Cryptocurrencies { get; set; }

        public List<int> Numbers { get; set; } = new List<int>();

        public CryptocurrenciesListViewModel(NavigatorViewModel navigatorViewModel)
        {
            this.navigatorViewModel = navigatorViewModel;
            SelectedNumber = 10;
            UpdateCryptocurrenciesList().ContinueWith(x => { CreateNumberList(); });
        }

        public async Task UpdateCryptocurrenciesList()
        {
            var response = await navigatorViewModel.HTTPClient.GetFromJsonAsync<JsonData<Cryptocurrency>>("https://api.coincap.io/v2/assets?limit="+selectedNumber.ToString() + "&search=" + searchText);
            Cryptocurrencies = new ObservableCollection<Cryptocurrency>(response.Data);
            int cryptocurrenciesCount = Cryptocurrencies.Count;
            for (int i = 0; i < cryptocurrenciesCount; i++)
            {
                Cryptocurrencies[i].SupplyString = Cryptocurrencies[i].Supply.ToString("C2", CultureInfo.CreateSpecificCulture("en"));
                Cryptocurrencies[i].MarketCapUsdString = Cryptocurrencies[i].MarketCapUsd.ToString("C2", CultureInfo.CreateSpecificCulture("en"));
                Cryptocurrencies[i].VolumeUsd24HrString = Cryptocurrencies[i].VolumeUsd24Hr.ToString("C2", CultureInfo.CreateSpecificCulture("en"));
                Cryptocurrencies[i].PriceUsdString = Cryptocurrencies[i].PriceUsd.ToString("C2", CultureInfo.CreateSpecificCulture("en"));
                Cryptocurrencies[i].ChangePercent24HrString = Cryptocurrencies[i].ChangePercent24Hr.ToString("P", CultureInfo.InvariantCulture);
                if (Cryptocurrencies[i].ChangePercent24Hr > 0)
                {
                    Cryptocurrencies[i].ChangePercent24HrString = "+" + Cryptocurrencies[i].ChangePercent24HrString;
                }
            }
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
