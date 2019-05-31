using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xam.LaGalerna.ViewModels.Base;

namespace Xam.LaGalerna.ViewModels
{
    public class RssDetailViewModel : ViewModelBase
    {
        private Xam.Rss.FeedItem _artItem;

        public RssDetailViewModel(object parameter)
        {
            if (parameter is Xam.Rss.FeedItem)
            {
                ArtItem = (Xam.Rss.FeedItem)parameter;
            }
        }

        public Xam.Rss.FeedItem ArtItem
        {
            get { return _artItem; }
            set
            {
                _artItem = value;
                OnPropertyChanged();
            }
        }

        public override Task InitializeAsync(object navigationData)
        {
            if (navigationData is Xam.Rss.FeedItem)
            {
                ArtItem = (Xam.Rss.FeedItem)navigationData;
            }
            
            return base.InitializeAsync(navigationData);
        }
    }
}
