using System;
using System.Threading.Tasks;
using Xam.LaGalerna.Services;
using Xam.LaGalerna.ViewModels;
using Xam.LaGalerna.ViewModels.Base;
using Xam.LaGalerna.Views;
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
            InitNavigation();
        }

        void BuildDependencies()
        {
            Locator.Instance.Build();
        }

        async Task<bool> InitNavigation()
        {
            MainView page = new MainView();
            page.BindingContext = MainViewModel.Instance;
            MainPage = new CustomNavigationPage(page);
            return true;
        }

        protected override void OnStart ()
		{
			// Handle when your app starts
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
