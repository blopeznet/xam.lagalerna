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
		public RssDetailView (object parameter)
		{
			InitializeComponent ();

            if (Device.RuntimePlatform == "Android")
                NavigationPage.SetHasNavigationBar(this, false);

            BindingContext = new RssDetailViewModel(parameter);

            SharedTransitionNavigationPage.SetSharedTransitionDuration(this, 500);
        }

        private async void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MainViewModel.Instance.IsBusy = true;
            await Navigation.PopAsync();
            SharedTransitionNavigationPage.SetSelectedTagGroup(this, ((int)((Rss.FeedItem)e.CurrentSelection[0]).Number) + 1);
            MainViewModel.Instance.IsBusy = false;
            await Navigation.PushAsync(new RssDetailView(e.CurrentSelection[0]));             
        }
        
    }
}