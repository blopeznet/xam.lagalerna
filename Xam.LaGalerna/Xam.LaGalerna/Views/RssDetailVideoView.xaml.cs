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
	public partial class RssDetailVideoView : ContentPage
	{
		public RssDetailVideoView(object parameter)
		{
			InitializeComponent ();

            if (Device.RuntimePlatform == "Android")
                NavigationPage.SetHasNavigationBar(this, false);

            RssDetailViewModel model = new RssDetailViewModel(parameter);
            BindingContext = model;
            UpdateWebView(model);
            SharedTransitionNavigationPage.SetSharedTransitionDuration(this, 500);
        }

        private void UpdateWebView(RssDetailViewModel model)
        {
            var html = new HtmlWebViewSource();            
            html.Html = model.ArtItem.Content;
            this.webViewVideo.Source = html;
        }
   

        private async void Related_SelectionChanged(object sender, System.EventArgs e)
        {
            MainViewModel.Instance.IsBusy = true;
            await Navigation.PopAsync();
            Rss.FeedItem context = (Rss.FeedItem)(sender as View).BindingContext;
            SharedTransitionNavigationPage.SetSelectedTagGroup(this, (context.Number) + 1);
            MainViewModel.Instance.IsBusy = false;
            await Navigation.PushAsync(new RssDetailView(context));
        }
    }
}