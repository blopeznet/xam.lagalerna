using Plugin.SharedTransitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                default:
                    break;
            }
        }
    }
}
