using Plugin.SharedTransitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xam.LaGalerna.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xam.Forms.VideoPlayer;

namespace Xam.LaGalerna.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RssDetailVideoView : ContentPage
	{

        private VideoSource _videoSource;


        public RssDetailVideoView(object parameter)
		{
			InitializeComponent ();
            RssDetailViewModel model = new RssDetailViewModel(parameter);
            BindingContext = model;
            String urlvideo = YoutubeUrlResolverHelper.ResolveUrl(model.ArtItem.UrlVideo);
            this.Title = model.ArtItem.Title;            
            UpdateVideoPlayer(urlvideo);
            SharedTransitionNavigationPage.SetSharedTransitionDuration(this, 500);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            videoPlayer.Source = _videoSource;
            if (Device.RuntimePlatform == Device.Android)            
                DependencyService.Get<IStatusBar>().HideStatusBar();
            
        }     

        private void UpdateVideoPlayer(String url)
        {
            _videoSource = new UriVideoSource()
            {
                Uri = url
            };
        }
         

        private async void VideoPlayer_PlayCompletion(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private async void VideoPlayer_PlayError(object sender, VideoPlayer.PlayErrorEventArgs e)
        {
            await Navigation.PopAsync();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            videoPlayer.Stop();
        }
    }
}