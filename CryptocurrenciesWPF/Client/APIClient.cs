using CryptocurrenciesWPF.Models;
using CryptocurrenciesWPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CryptocurrenciesWPF.Client
{
    public class APIClient
    {
        private string currentId = "";
        private const string INFO_FOR_CONVERSION_HTTP_PATH = "https://api.coincap.io/v2/assets";
        private const string CRYPTOCURRENCIES_LIST_HTTP_PATH = $"https://api.coincap.io/v2/assets?limit=";
        private const string MARKETS_LIST_HTTP_PATH_FIRST_PART = $"https://api.coincap.io/v2/markets?baseId=";
        private const string MARKETS_LIST_HTTP_PATH_SECOND_PART = $"&limit=10";
        private const string HISTORY_HTTP_PATH_FIRST_PART = $"https://api.coincap.io/v2/assets/";
        private const string HISTORY_HTTP_PATH_SECOND_PART = $"/history?interval=d1";

        private HttpClient httpClient = new HttpClient();

        public async Task<HttpResponseMessage> GetInfoForConversionHTTP()
        {
            HttpResponseMessage responseQuery = await httpClient.GetAsync(INFO_FOR_CONVERSION_HTTP_PATH);
            return responseQuery;
        }

        public async Task<HttpResponseMessage> GetCryptocurrenciesListHTTP(string selectedNumber, string searchText)
        {
            HttpResponseMessage responseQuery = await httpClient
                .GetAsync(CRYPTOCURRENCIES_LIST_HTTP_PATH + selectedNumber.ToString() + "&search=" + searchText);
            return responseQuery;
        }

        public async Task<CryptocurrencyInfoModel> GetInfoForCryptocurrencyHTTP(string currentId)
        {
            string queryStringMarkets = MARKETS_LIST_HTTP_PATH_FIRST_PART + 
                currentId + MARKETS_LIST_HTTP_PATH_SECOND_PART;
            string queryStringHistory = HISTORY_HTTP_PATH_FIRST_PART +
                currentId + HISTORY_HTTP_PATH_SECOND_PART;

            var tMarkets = httpClient.GetAsync(queryStringMarkets);
            var tHistory = httpClient.GetAsync(queryStringHistory);

            await Task.WhenAll<HttpResponseMessage>(tMarkets, tHistory);

            CryptocurrencyInfoModel cryptocurrencyInfoModel = new CryptocurrencyInfoModel();

            cryptocurrencyInfoModel.HTTPResponseMarkets = await (await tMarkets).Content.ReadAsStringAsync();
            cryptocurrencyInfoModel.HTTPResponsePrices = await (await tHistory).Content.ReadAsStringAsync();

            return cryptocurrencyInfoModel;
        }
    }
}
