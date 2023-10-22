using CryptocurrenciesWPF.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

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
        public ObservableCollection<PriceHistory> PricesHistory { get; set; }

        public CryptocurrencyProfileViewModel(NavigatorViewModel navigatorViewModel)
        {
            this.navigatorViewModel = navigatorViewModel;
            currentCryptocurrency = new Cryptocurrency();
        }

        public async Task UpdateMarketsList()
        {
            string currentId = currentCryptocurrency.Id;
            try
            {
                string queryStringMarkets = $"https://api.coincap.io/v2/markets?baseId={currentId}&limit=10";
                string queryStringHistory = $"https://api.coincap.io/v2/assets/{currentId}/history?interval=d1";
                var tMarkets = navigatorViewModel.HTTPClient.GetAsync(queryStringMarkets);
                var tHistory = navigatorViewModel.HTTPClient.GetAsync(queryStringHistory);

                await Task.WhenAll<HttpResponseMessage>(tMarkets, tHistory);
                var responseMarkets = await tMarkets.Result.Content.ReadFromJsonAsync<JsonData<Market>>();
                var responsePrices = await tHistory.Result.Content.ReadFromJsonAsync<JsonData<PriceHistory>>();
                Markets = new ObservableCollection<Market>(responseMarkets.Data);
                PricesHistory = new ObservableCollection<PriceHistory>(responsePrices.Data);
            } catch (Exception ex)
            {

            }
            OnPropertyChanged("Markets");
        }

        public void UpdateCurrentCryptocurrency(Cryptocurrency newCryptocurrency)
        {
            currentCryptocurrency.Id = newCryptocurrency.Id;
            currentCryptocurrency.Rank = newCryptocurrency.Rank;
            currentCryptocurrency.Symbol = newCryptocurrency.Symbol;
            currentCryptocurrency.Name = newCryptocurrency.Name;
            currentCryptocurrency.Supply = newCryptocurrency.Supply;
            currentCryptocurrency.SupplyString = newCryptocurrency.SupplyString;
            currentCryptocurrency.MaxSupply = newCryptocurrency.MaxSupply;
            currentCryptocurrency.MarketCapUsd = newCryptocurrency.MarketCapUsd;
            currentCryptocurrency.MarketCapUsdString = newCryptocurrency.MarketCapUsdString;
            currentCryptocurrency.VolumeUsd24Hr = newCryptocurrency.VolumeUsd24Hr;
            currentCryptocurrency.VolumeUsd24HrString = newCryptocurrency.VolumeUsd24HrString;
            currentCryptocurrency.PriceUsd = newCryptocurrency.PriceUsd;
            currentCryptocurrency.PriceUsdString = newCryptocurrency.PriceUsdString;
            currentCryptocurrency.ChangePercent24Hr = newCryptocurrency.ChangePercent24Hr;
            currentCryptocurrency.ChangePercent24HrString = newCryptocurrency.ChangePercent24HrString;
            currentCryptocurrency.Vwap24Hr = newCryptocurrency.Vwap24Hr;
            OnPropertyChanged("CurrentCryptocurrency");
        }
    }
}
