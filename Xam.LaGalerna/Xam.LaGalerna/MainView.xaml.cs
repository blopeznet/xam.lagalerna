using Plugin.SharedTransitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xam.LaGalerna.Entities;
using Xam.LaGalerna.ViewModels;
using Xam.LaGalerna.Views;
using Xamarin.Forms;

namespace Xam.LaGalerna
{
	public partial class MainView : ContentPage
	{
		public MainView()
		{
			InitializeComponent();

            if (Device.RuntimePlatform == "Android")
                NavigationPage.SetHasNavigationBar(this, false);

            SharedTransitionNavigationPage.SetBackgroundAnimation(this, BackgroundAnimation.Fade);
            SharedTransitionNavigationPage.SetSharedTransitionDuration(this, 500);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (Device.RuntimePlatform == Device.Android)
                DependencyService.Get<Forms.VideoPlayer.IStatusBar>().HideStatusBar();
        }

        private async void OnHighlightTapped(object sender, System.EventArgs e)
        {
            Xam.Rss.FeedItem context = (Xam.Rss.FeedItem)(sender as View).BindingContext;
            Xam.LaGalerna.Entities.SectionType s = (Xam.LaGalerna.Entities.SectionType)(((Xam.Rss.FeedItem)context).Number);
            SharedTransitionNavigationPage.SetSelectedTagGroup(this, ((int)s) + 1);
            switch (s)
            {
                case Entities.SectionType.Articles:
                    await Navigation.PushAsync(new RssDetailView(context));
                    break;
                case Entities.SectionType.Youtube:
                    await Navigation.PushAsync(new RssDetailVideoView(context));
                    break;
                case Entities.SectionType.Spotify:
                    await Navigation.PushAsync(new SpotifyPlayListView(context));
                    break;
                default:
                    break;
            }
        }

        private void StackLayout_ChildAdded(object sender, ElementEventArgs e)
        {
            base.OnChildAdded(e.Element);            

            uint duration = 750;

            // We are going to create a simple but nice animation. 
            // We will fade in at the same time we translade the cell view from the bottom to the top.
            var animation = new Animation();

            animation.WithConcurrent((f) => ((Grid)e.Element).Opacity = f, 0, 1, Easing.CubicOut);

            animation.WithConcurrent(
              (f) => ((Grid)e.Element).TranslationY = f,
              ((Grid)e.Element).TranslationY + 50, 0,
              Easing.CubicOut, 0, 1);

            ((Grid)e.Element).Animate("FadeIn", animation, 16, Convert.ToUInt32(duration));
        }

        private async void Menu_SelectionChanged(object sender, System.EventArgs e)
        {
            MainViewModel.Instance.IsBusy = true;
            Section context = (Section)(sender as View).BindingContext;
            await MainViewModel.Instance.UpdateSectionsShow(context);
            MainViewModel.Instance.IsBusy = false;
        }
    }
}
