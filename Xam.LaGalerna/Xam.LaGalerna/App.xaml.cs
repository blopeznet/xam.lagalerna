using System;
using System.Threading.Tasks;
using Xam.LaGalerna.Services;
using Xam.LaGalerna.ViewModels;
using Xam.LaGalerna.ViewModels.Base;
using Xam.LaGalerna.Views;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation (XamlCompilationOptions.Compile)]
namespace Xam.LaGalerna
{
	public partial class App : Application
	{
		public App ()
		{
			InitializeComponent();
            BuildDependencies();
            Xamarin.Essentials.Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;
            UpdateStatusNet();
            InitNavigation();
        }

        void BuildDependencies()
        {
            Locator.Instance.Build();
        }

        async Task<bool> InitNavigation()
        { 
            MainView page = new MainView();
            //SpotifyPlayListView page = new SpotifyPlayListView(null);
            page.BindingContext = MainViewModel.Instance;
            MainPage = new CustomNavigationPage(page);
            return true;
        }

        protected override void OnStart ()
		{
			// Handle when your app starts
		}

        private void Connectivity_ConnectivityChanged(object sender, Xamarin.Essentials.ConnectivityChangedEventArgs e)
        {
            UpdateStatusNet();
        }

        private void UpdateStatusNet()
        {
            var current = Connectivity.NetworkAccess;
            switch (current)
            {
                case NetworkAccess.Internet:
                    // Connected to internet
                    MainViewModel.Instance.IsNetworkOk = true;
                    break;
                case NetworkAccess.Local:
                    MainViewModel.Instance.IsNetworkOk = false;
                    // Only local network access
                    break;
                case NetworkAccess.ConstrainedInternet:
                    // Connected, but limited internet access such as behind a network login page
                    MainViewModel.Instance.IsNetworkOk = false;
                    break;
                case NetworkAccess.None:
                    // No internet available
                    MainViewModel.Instance.IsNetworkOk = false;
                    break;
                case NetworkAccess.Unknown:
                    // Internet access is unknown
                    MainViewModel.Instance.IsNetworkOk = false;
                    break;
            }
        }

        protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
