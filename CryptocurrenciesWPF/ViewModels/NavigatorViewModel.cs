using CryptocurrenciesWPF.Models;
using CryptocurrenciesWPF.Views;
using System.Windows.Controls;

namespace CryptocurrenciesWPF.ViewModels
{
    public class NavigatorViewModel : BaseViewModel
    {
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
            cryptocurrencyProfileViewModel.CurrentCryptocurrency = cryptocurrency;
            GoToPage(cryptocurrencyProfilePage);
        }
    }
}
