using CryptocurrenciesWPF.Commands;
using CryptocurrenciesWPF.Models;
using CryptocurrenciesWPF.Views;
using System.Collections.Generic;
using System.Net.Http;
using System.Windows.Controls;

namespace CryptocurrenciesWPF.ViewModels
{
    public class NavigatorViewModel : BaseViewModel
    {
        private HttpClient httpClient = new HttpClient();

        private Stack<(Page page, BaseViewModel context)> pageHistory = new Stack<(Page, BaseViewModel)>();

        public HttpClient HTTPClient
        {
            get
            {
                return httpClient;
            }
        }

        private RelayCommand goToListViewCommand;
        public RelayCommand GoToListViewCommand
        {
            get
            {
                return goToListViewCommand ??
                  (goToListViewCommand = new RelayCommand(obj =>
                  {
                      cryptocurrenciesListPage = new CryptocurrenciesListPage();
                      cryptocurrenciesListPage.DataContext = cryptocurrenciesListViewModel;
                      GoToPage(cryptocurrenciesListPage);
                  }));
            }
        }

        private Page currentPage;
        public Page CurrentPage
        {
            get { return currentPage; }
            set
            {
                currentPage = value;
                OnPropertyChanged("CurrentPage");
            }
        }

        private CryptocurrenciesListPage cryptocurrenciesListPage;
        private CryptocurrenciesListViewModel cryptocurrenciesListViewModel;

        private CryptocurrencyConversionPage cryptocurrencyConversionPage;
        private CryptocurrencyConversionViewModel cryptocurrencyConversionViewModel;

        private CryptocurrencyProfileViewModel cryptocurrencyProfileViewModel;

        public NavigatorViewModel()
        {
            cryptocurrenciesListPage = new CryptocurrenciesListPage();
            cryptocurrenciesListViewModel = new CryptocurrenciesListViewModel(this);
            cryptocurrenciesListPage.DataContext = cryptocurrenciesListViewModel;

            cryptocurrencyConversionPage = new CryptocurrencyConversionPage();
            cryptocurrencyConversionViewModel = new CryptocurrencyConversionViewModel(this);
            cryptocurrencyConversionPage.DataContext = cryptocurrencyConversionViewModel;

            cryptocurrencyProfileViewModel = new CryptocurrencyProfileViewModel(this);

            CurrentPage = cryptocurrenciesListPage;
        }

        public void GoToPage(Page page)
        {
            CurrentPage = page;
        }

        public void GoToProfilePageFromListPage(Cryptocurrency cryptocurrency)
        {
            cryptocurrenciesListPage.NavigationService.Refresh();
            cryptocurrencyProfileViewModel.UpdateCurrentCryptocurrency(cryptocurrency);
            cryptocurrencyProfileViewModel.UpdateMarketsList();
            CryptocurrencyProfilePage cryptocurrencyProfilePage = new CryptocurrencyProfilePage();
            cryptocurrencyProfilePage.DataContext = cryptocurrencyProfileViewModel;
            GoToPage(cryptocurrencyProfilePage);
        }
    }
}
