using CryptocurrenciesWPF.Commands;
using CryptocurrenciesWPF.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptocurrenciesWPF.ViewModels
{
    public class CryptocurrencyConversionViewModel : BaseViewModel
    {
        private const string HTTP_PATH = "https://api.coincap.io/v2/assets";
        private double conversionResult;

        private RelayCommand convertCommand;

        private NavigatorViewModel navigatorViewModel;
        private double numberValue;

        private Cryptocurrency selectedCryptocurrencyFrom;
        private Cryptocurrency selectedCryptocurrencyTo;

        public Cryptocurrency SelectedCryptocurrencyFrom
        {
            get { return selectedCryptocurrencyFrom; }
            set
            {
                if (value != null)
                {
                    selectedCryptocurrencyFrom = value;
                    OnPropertyChanged(nameof(SelectedCryptocurrencyFrom));
                }
            }
        }

        public Cryptocurrency SelectedCryptocurrencyTo
        {
            get { return selectedCryptocurrencyTo; }
            set
            {
                if (value != null)
                {
                    selectedCryptocurrencyTo = value;
                    OnPropertyChanged(nameof(SelectedCryptocurrencyTo));
                }
            }
        }

        public double NumberValue
        {
            get { return numberValue; }
            set
            {
                if (value != null)
                {
                    numberValue = value;
                    OnPropertyChanged(nameof(NumberValue));
                }
            }
        }

        public double ConversionResult
        {
            get { return conversionResult; }
            set
            {
                if (value != null)
                {
                    conversionResult = value;
                    OnPropertyChanged(nameof(ConversionResult));
                }
            }
        }

        public RelayCommand ConvertCommand
        {
            get
            {
                return convertCommand ??
                  (convertCommand = new RelayCommand(obj =>
                  {
                      if (selectedCryptocurrencyFrom != null 
                      && selectedCryptocurrencyTo != null)
                      {
                          ConversionResult =
                            selectedCryptocurrencyFrom.PriceUsd * NumberValue / selectedCryptocurrencyTo.PriceUsd;
                      }
                  }));
            }
        }

        public ObservableCollection<Cryptocurrency> Cryptocurrencies { get; set; } 
            = new ObservableCollection<Cryptocurrency>();

        public CryptocurrencyConversionViewModel(NavigatorViewModel navigatorViewModel)
        {
            this.navigatorViewModel = navigatorViewModel;
            UpdateCryptocurrenciesList().ContinueWith(x => { });
        }

        public async Task UpdateCryptocurrenciesList()
        {
            var responseQuery = await navigatorViewModel.Client.GetInfoForConversionHTTP();

            if (!responseQuery.IsSuccessStatusCode) { return; }

            var responseValue = await responseQuery.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<JsonData<Cryptocurrency>>(responseValue);

            Cryptocurrencies.Clear();
            response?.Data?.ForEach(x =>
            {
                Cryptocurrencies.Add(x);
            });

            OnPropertyChanged("Cryptocurrencies");
        }
    }
}
