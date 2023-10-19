using CryptocurrenciesWPF.Models;
using CryptocurrenciesWPF.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace CryptocurrenciesWPF.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        static private HttpClient client = new HttpClient();

        public List<Cryptocurrency> Cryptocurrencies { get; set; }

        private Cryptocurrency selectedCryptocurrency;
        public Cryptocurrency SelectedCryptocurrency
        {
            get { return selectedCryptocurrency; }
            set
            {
                selectedCryptocurrency = value;
                MessageBox.Show(value.Name);
                CurrentPage = cryptocurrenciesProfilePage;
                cryptocurrencyProfileViewModel.CurrentCryptocurrency = selectedCryptocurrency;
                OnPropertyChanged("SelectedCryptocurrency");
            }
        }

        private Page cryptocurrenciesListPage;
        private Page cryptocurrenciesProfilePage;

        private CryptocurrencyProfileViewModel cryptocurrencyProfileViewModel;

        public MainWindowViewModel()
        {
            var response = client.GetFromJsonAsync<JsonData>("https://api.coincap.io/v2/assets").Result;
            Cryptocurrencies = response.Data;


            cryptocurrenciesListPage = new CryptocurrenciesListPage();
            cryptocurrenciesListPage.DataContext = this;

            cryptocurrenciesProfilePage = new CryptocurrencyProfilePage();
            cryptocurrencyProfileViewModel = new CryptocurrencyProfileViewModel();
            cryptocurrenciesProfilePage.DataContext = cryptocurrencyProfileViewModel;

            CurrentPage = cryptocurrenciesListPage;

        }
    }
}
