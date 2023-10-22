using CryptocurrenciesWPF.Models;
using LiveCharts.Wpf;
using LiveCharts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Media;
using Newtonsoft.Json;

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

        public SeriesCollection SeriesCollection { get; set; }

        public ObservableCollection<Market> Markets { get; set; } = new ObservableCollection<Market>();
        public ObservableCollection<PriceHistory> PricesHistory { get; set; } = new ObservableCollection<PriceHistory>();

        public CryptocurrencyProfileViewModel(NavigatorViewModel navigatorViewModel)
        {
            this.navigatorViewModel = navigatorViewModel;
            currentCryptocurrency = new Cryptocurrency();
        }

        public Func<double, string> YFormatter { get; set; }
        public string[] Labels { get; set; }

        public async Task UpdateMarketsList()
        {
            string currentId = currentCryptocurrency.Id;
            Markets.Clear();
            PricesHistory.Clear();
            try
            {

                string queryStringMarkets = $"https://api.coincap.io/v2/markets?baseId={currentId}&limit=10";
                string queryStringHistory = $"https://api.coincap.io/v2/assets/{currentId}/history?interval=d1";
                var tMarkets = navigatorViewModel.HTTPClient.GetAsync(queryStringMarkets);
                var tHistory = navigatorViewModel.HTTPClient.GetAsync(queryStringHistory);

                await Task.WhenAll<HttpResponseMessage>(tMarkets, tHistory);



                var responseMarketsValue = await tMarkets.Result.Content.ReadAsStringAsync();
                var responsePricesValue = await tHistory.Result.Content.ReadAsStringAsync();

                var responseMarkets = JsonConvert.DeserializeObject<JsonData<Market>>(responseMarketsValue);
                var responsePrices = JsonConvert.DeserializeObject<JsonData<PriceHistory>>(responsePricesValue);

                responseMarkets?.Data?.ForEach(x =>
                {
                    Markets.Add(x);
                });
                responsePrices?.Data?.ForEach(x =>
                {
                    PricesHistory.Add(x);
                });

                List<double> prices = PricesHistory.Select<PriceHistory, double>(x => Math.Round(x.PriceUsd,2)).ToList();
                List<string> times = new List<string>();
                for (int i = 0; i < PricesHistory.Count; i++)
                {
                    var date = (new DateTime(1970, 1, 1)).AddMilliseconds(PricesHistory[i].Time);
                    times.Add(date.ToString("dd.MM.yyyy"));
                }

                SeriesCollection = new SeriesCollection
                {
                    new LineSeries
                    {
                        Title = currentCryptocurrency.Name,
                        Values = new ChartValues<double>(prices),
                        LineSmoothness = 0, //0: straight lines, 1: really smooth lines
                        PointGeometry = null,
                        PointForeground = Brushes.Gray
                    }
                };

                Labels = times.ToArray();
                YFormatter = value => value.ToString("C", CultureInfo.CreateSpecificCulture("en"));

            }
            catch (Exception ex)
            {

            }
            OnPropertyChanged("SeriesCollection");
            OnPropertyChanged("Labels");
            OnPropertyChanged("YFormatter");
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
