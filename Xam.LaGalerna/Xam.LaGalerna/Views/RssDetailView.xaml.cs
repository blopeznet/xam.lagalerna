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
	public partial class RssDetailView : ContentPage
	{

        #region private vars

        private const int ParallaxSpeed = 5;

        private double _lastScroll;

        #endregion

        public RssDetailView (object parameter)
		{
			InitializeComponent ();

            if (Device.RuntimePlatform == "Android")
                NavigationPage.SetHasNavigationBar(this, false);

            BindingContext = new RssDetailViewModel(parameter);

            SharedTransitionNavigationPage.SetSharedTransitionDuration(this, 500);
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

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ParallaxScroll.Scrolled += OnParallaxScrollScrolled;
        }

        /// <summary>
        /// Event appear to unsuscribe scroll event
        /// </summary>
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            ParallaxScroll.Scrolled -= OnParallaxScrollScrolled;
        }

        /// <summary>
        /// Effect parallax when scroll
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnParallaxScrollScrolled(object sender, ScrolledEventArgs e)
        {
            double translation = 0;

            if (_lastScroll < e.ScrollY)
            {
                translation = 0 - ((e.ScrollY / 2));
                if (translation > 0) translation = 0;
            }
            else
            {
                translation = 0 + ((e.ScrollY / 2));
                if (translation > 0) translation = 0;
            }

            HeaderPanel.TranslateTo(HeaderPanel.TranslationX, translation);
            _lastScroll = e.ScrollY;
        }

        private async void ImageButtonWeb_Clicked(object sender, EventArgs e)
        {
            RssDetailViewModel rs = (RssDetailViewModel)BindingContext;

            await Xamarin.Essentials.Launcher.OpenAsync(rs.ArtItem.Link);

        }

        private async void ImageButtonShare_Clicked(object sender, EventArgs e)
        {

            RssDetailViewModel rs = (RssDetailViewModel)BindingContext;

            await Xamarin.Essentials.Share.RequestAsync(new Xamarin.Essentials.ShareTextRequest
            {
                Uri = rs.ArtItem.Link,
                Title = rs.ArtItem.Title
            });
        }

        
    }
}