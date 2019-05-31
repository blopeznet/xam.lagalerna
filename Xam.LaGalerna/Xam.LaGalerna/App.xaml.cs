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
            var viewModel = Locator.Instance.Resolve(typeof(MainViewModel)) as ViewModelBase;
            page.BindingContext = viewModel;
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
