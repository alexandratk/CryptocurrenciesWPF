using CryptocurrenciesWPF.Commands;
using CryptocurrenciesWPF.Models;
using CryptocurrenciesWPF.Views;
using System.Net.Http;
using System.Windows.Controls;

namespace CryptocurrenciesWPF.ViewModels
{
    public class NavigatorViewModel : BaseViewModel
    {
        private HttpClient httpClient = new HttpClient();
        public HttpClient HTTPClient {
            get
            {
                return httpClient;
            }
        }

        private RelayCommand backCommand;
        public RelayCommand BackCommand
        {
            get
            {
                return backCommand ??
                  (backCommand = new RelayCommand(obj =>
                  {
                      GoToListPageFromProfilePage();
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

        private CryptocurrencyProfilePage cryptocurrencyProfilePage;
        private CryptocurrencyProfileViewModel cryptocurrencyProfileViewModel;

        public NavigatorViewModel()
        {
            cryptocurrenciesListPage = new CryptocurrenciesListPage();
            cryptocurrenciesListViewModel = new CryptocurrenciesListViewModel(this);
            cryptocurrenciesListPage.DataContext = cryptocurrenciesListViewModel;

            cryptocurrencyConversionPage = new CryptocurrencyConversionPage();
            cryptocurrencyConversionViewModel = new CryptocurrencyConversionViewModel(this);
            cryptocurrencyConversionPage.DataContext = cryptocurrencyConversionViewModel;

            cryptocurrencyProfilePage = new CryptocurrencyProfilePage();
            cryptocurrencyProfileViewModel = new CryptocurrencyProfileViewModel(this);
            cryptocurrencyProfilePage.DataContext = cryptocurrencyProfileViewModel;

            CurrentPage = cryptocurrenciesListPage;
        }

        public void GoToPage(Page page)
        {
            CurrentPage = page;
        }

        public void GoToProfilePageFromListPage(Cryptocurrency cryptocurrency)
        {
            cryptocurrencyProfileViewModel.CurrentCryptocurrency = new Cryptocurrency(cryptocurrency);
            cryptocurrencyProfileViewModel.UpdateMarketsList().ContinueWith(x => { GoToPage(cryptocurrencyProfilePage); });
        }

        public void GoToListPageFromProfilePage()
        {
         //   cryptocurrenciesListViewModel.SelectedCryptocurrency = new Cryptocurrency(cryptocurrency);

            GoToPage(cryptocurrenciesListPage);
        }
    }
}
