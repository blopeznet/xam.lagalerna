﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xam.LaGalerna.Entities;
using Xam.LaGalerna.Services;
using Xam.LaGalerna.ViewModels.Base;

namespace Xam.LaGalerna.ViewModels
{
    public class MainViewModel: ViewModelBase
    {
        private static MainViewModel _Instance;
        /// <summary>
        /// MainViewModel variable
        /// </summary>
        public static MainViewModel Instance
        {
            get
            {
                if (_Instance == null)
                    _Instance = new MainViewModel();
                return _Instance;
            }
        }


        public MainViewModel()
        {


            List<Section> list = new List<Section>();
            list.Add(new Section() { Title = "Portanálisis", SourceUrl = "https://www.lagalerna.com/category/portanalisis/feed/", Type = SectionType.Articles });
            list.Add(new Section() { Title = "Opinión", SourceUrl = "https://www.lagalerna.com/category/opinion/feed/", Type = SectionType.Articles });
            list.Add(new Section() { Title = "Escohotado", SourceUrl = "https://www.lagalerna.com/category/escohotado/feed/", Type = SectionType.Articles });
            //list.Add(new Section() { Title = "Video", SourceUrl = "https://www.lagalerna.com/galerna-video/feed/feed/", Type = SectionType.Articles });
            list.Add(new Section() { Title = "Video", SourceUrl = "https://www.youtube.com/feeds/videos.xml?channel_id=UC-8OuYpE_uo3SmW9xTQPGOA", Type= SectionType.Youtube });            
            list.Add(new Section() { Title = "Entrevistas", SourceUrl = "https://www.lagalerna.com/category/entrevistas/feed/", Type = SectionType.Articles });
            list.Add(new Section() { Title = "Históricos", SourceUrl = "https://www.lagalerna.com/category/historicos/feed/", Type = SectionType.Articles });
            list.Add(new Section() { Title = "Crónicas", SourceUrl = "https://www.lagalerna.com/category/cronicas/feed/", Type = SectionType.Articles });
            list.Add(new Section() { Title = "Así viví", SourceUrl = "https://www.lagalerna.com/category/asivivi/feed/", Type = SectionType.Articles });

            SectionItems = list;
            UpdateSections();
        }


        /// <summary>
        /// Flag for know if app is busy
        /// </summary>
        private bool _IsBusy = false;
        public bool IsBusy
        {
            get
            {
                return _IsBusy;
            }

            set
            {
                _IsBusy = value;
                OnPropertyChanged("IsBusy");
            }
        }

        List<Section> _SectionItems;
        /// <summary>
        /// List of sections
        /// </summary>
        public List<Section> SectionItems
        {
            get
            {

                if (_SectionItems == null)
                    _SectionItems = new List<Section>();
                return _SectionItems;
            }
            set
            {
                _SectionItems = value;
                OnPropertyChanged("SectionItems");
            }
        }


        public async Task UpdateSections()
        {
            IsBusy = true;
            foreach (Section s in SectionItems) {
                switch (s.Type)
                {
                    case SectionType.Articles:
                        s.RssItems = await RssService.Instance.GetArticles(s.SourceUrl, (int)s.Type);
                        break;
                    case SectionType.Youtube:
                        s.RssItems = await RssService.Instance.GetYoutubeArticles(s.SourceUrl, (int)s.Type, 400,300);
                        break;
                    default:
                        break;
                }
                
            }
            IsBusy = false;
        }

    }
}
