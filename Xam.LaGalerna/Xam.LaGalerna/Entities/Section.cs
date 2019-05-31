using System;
using System.Collections.Generic;
using System.Text;
using Xam.LaGalerna.ViewModels.Base;

namespace Xam.LaGalerna.Entities
{
    public class Section: ViewModelBase
    {

        String  _Title;
        /// <summary>
        /// Title
        /// </summary>
        public String Title
        {
            get
            {               
                return _Title;
            }
            set
            {
                _Title = value;
                OnPropertyChanged("Title");
            }
        }

        SectionType _Type;
        /// <summary>
        /// Type
        /// </summary>
        public SectionType Type
        {
            get
            {
                return _Type;
            }
            set
            {
                _Type = value;
                OnPropertyChanged("Type");
            }
        }

        String _SourceUrl;
        /// <summary>
        /// Source Url
        /// </summary>
        public String SourceUrl
        {
            get
            {
                return _SourceUrl;
            }
            set
            {
                _SourceUrl = value;
                OnPropertyChanged("SourceUrl");
            }
        }

        List<Xam.Rss.FeedItem> _RssItems;
        /// <summary>
        /// List of Rss items
        /// </summary>
        public List<Xam.Rss.FeedItem> RssItems
        {
            get
            {

                if (_RssItems == null)
                    _RssItems = new List<Xam.Rss.FeedItem>();
                return _RssItems;
            }
            set
            {
                _RssItems = value;
                OnPropertyChanged("RssItems");
            }
        }
    }
}
