using CryptocurrenciesWPF.Models;
using CryptocurrenciesWPF.Views;
using System;
using System.CodeDom;
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

        private Cryptocurrency selectedCryptocurrency;
        private int selectedNumber;

        public Cryptocurrency SelectedCryptocurrency
        {
            get { return selectedCryptocurrency; }
            set
            {
                if (value != null)
                {
                    selectedCryptocurrency = value;
                    navigatorViewModel.GoToProfilePageFromListPage(selectedCryptocurrency);
                    OnPropertyChanged("SelectedCryptocurrency");
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
            var response = await navigatorViewModel.HTTPClient.GetFromJsonAsync<JsonData<Cryptocurrency>>("https://api.coincap.io/v2/assets?limit="+selectedNumber.ToString());
            Cryptocurrencies = new ObservableCollection<Cryptocurrency>(response.Data);
            OnPropertyChanged("Cryptocurrencies");
        }


        public void CreateNumberList()
        {
            for (int i = 1; i <= 200; i++)
            {
                Numbers.Add(i);
            }
            OnPropertyChanged(nameof(Numbers));
        }
    }
}
