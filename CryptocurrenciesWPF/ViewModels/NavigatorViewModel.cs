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
        private CryptocurrencyConversionPage cryptocurrencyConversionPage;
        private CryptocurrencyConversionViewModel cryptocurrencyConversionViewModel;

        private CryptocurrenciesListPage cryptocurrenciesListPage;
        private CryptocurrenciesListViewModel cryptocurrenciesListViewModel;

        private CryptocurrencyProfileViewModel cryptocurrencyProfileViewModel;

        private Page currentPage;

        private bool enabledGoToListViewButton;
        private bool enabledGoToConversionViewButton;

        private RelayCommand goToConversionViewCommand;
        private RelayCommand goToListViewCommand;

        private HttpClient httpClient = new HttpClient();

        private Stack<(Page page, BaseViewModel context)> pageHistory = new Stack<(Page, BaseViewModel)>();

        public HttpClient HTTPClient
        {
            get
            {
                return httpClient;
            }
        }

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
                      EnabledGoToListViewButton = false;
                  }));
            }
        }

        public RelayCommand GoToConversionViewCommand
        {
            get
            {
                return goToConversionViewCommand ??
                  (goToConversionViewCommand = new RelayCommand(obj =>
                  {
                      GoToPage(cryptocurrencyConversionPage);
                      EnabledGoToConversionViewButton = false;
                  }));
            }
        }

        public Page CurrentPage
        {
            get { return currentPage; }
            set
            {
                currentPage = value;
                OnPropertyChanged("CurrentPage");
            }
        }

        public bool EnabledGoToListViewButton
        {
            get { return enabledGoToListViewButton; }
            set
            {
                enabledGoToListViewButton = value;
                OnPropertyChanged("EnabledGoToListViewButton");
            }
        }

        public bool EnabledGoToConversionViewButton
        {
            get { return enabledGoToConversionViewButton; }
            set
            {
                enabledGoToConversionViewButton = value;
                OnPropertyChanged("EnabledGoToConversionViewButton");
            }
        }

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
            EnabledGoToConversionViewButton = true;
        }

        public void GoToPage(Page page)
        {
            if (!EnabledGoToListViewButton)
            {
                EnabledGoToListViewButton = true;
            }
            if (!EnabledGoToConversionViewButton)
            {
                EnabledGoToConversionViewButton = true;
            }
            CurrentPage = page;
        }

        public void GoToProfilePageFromListPage(Cryptocurrency cryptocurrency)
        {
            cryptocurrencyProfileViewModel.UpdateCurrentCryptocurrency(cryptocurrency);
            cryptocurrencyProfileViewModel.UpdateMarketsList();
            CryptocurrencyProfilePage cryptocurrencyProfilePage = new CryptocurrencyProfilePage();
            cryptocurrencyProfilePage.DataContext = cryptocurrencyProfileViewModel;
            GoToPage(cryptocurrencyProfilePage);
        }
    }
}
