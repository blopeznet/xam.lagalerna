using Plugin.SharedTransitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xam.LaGalerna.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Xam.LaGalerna.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SpotifyPlayListView : ContentPage
	{

        HtmlWebViewSource _htmlSource;


        public SpotifyPlayListView(object parameter)
		{
			InitializeComponent ();
            RssDetailViewModel model = new RssDetailViewModel(parameter);
            BindingContext = MainViewModel.Instance;
            this.Title = model.ArtItem.Title;
            UpdateWebView(model);
            SharedTransitionNavigationPage.SetSharedTransitionDuration(this, 500);

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            browser.Source = _htmlSource;
            if (Device.RuntimePlatform == Device.Android)
                DependencyService.Get<Forms.VideoPlayer.IStatusBar>().HideStatusBar();

        }

        private void UpdateWebView(RssDetailViewModel model)
        {
            MainViewModel.Instance.IsBusy = true;
            var htmlSource = new HtmlWebViewSource();
            htmlSource.Html = String.Format("<body style=\"background-color:#000000\"><iframe style=\"background-color:#000000\" src=\"{0}\" width=\"100%\" height=\"380\" frameborder=\"0\" allowtransparency=\"true\" allow=\"encrypted-media\"></iframe></body>",model.ArtItem.Content);
            _htmlSource = htmlSource;
            MainViewModel.Instance.IsBusy = false;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            if (null != browser)            
                browser = null;            
        }
    }
}