using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xam.LaGalerna.Entities;
using Xam.LaGalerna.Services;
using Xam.LaGalerna.ViewModels.Base;
using System.Linq;

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
            list.Add(new Section() { Title = "Portanálisis", SourceUrl = "https://www.lagalerna.com/category/portanalisis/feed/", Type = SectionType.Articles,LoadOnInit=true,IsSelected=true });
            list.Add(new Section() { Title = "Opinión", SourceUrl = "https://www.lagalerna.com/category/opinion/feed/", Type = SectionType.Articles, LoadOnInit = false, IsSelected = false });
            list.Add(new Section() { Title = "Escohotado", SourceUrl = "https://www.lagalerna.com/category/escohotado/feed/", Type = SectionType.Articles, LoadOnInit = false, IsSelected = false });
            list.Add(new Section() { Title = "Vídeos", SourceUrl = "https://www.youtube.com/feeds/videos.xml?channel_id=UC-8OuYpE_uo3SmW9xTQPGOA", Type= SectionType.Youtube, LoadOnInit = false, IsSelected = false });            
            list.Add(new Section() { Title = "Entrevistas", SourceUrl = "https://www.lagalerna.com/category/entrevistas/feed/", Type = SectionType.Articles, LoadOnInit = false, IsSelected = false });
            list.Add(new Section() { Title = "Históricos", SourceUrl = "https://www.lagalerna.com/category/historicos/feed/", Type = SectionType.Articles, LoadOnInit = false, IsSelected = false });
            list.Add(new Section() { Title = "Crónicas", SourceUrl = "https://www.lagalerna.com/category/cronicas/feed/", Type = SectionType.Articles, LoadOnInit = false, IsSelected = false });
            list.Add(new Section() { Title = "Así viví", SourceUrl = "https://www.lagalerna.com/category/asivivi/feed/", Type = SectionType.Articles, LoadOnInit = false, IsSelected = false });
            SectionItems = list;
            SectionDisplayItems = list.Where(s=>s.LoadOnInit).ToList();
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

        List<Section> _SectionDisplayItems;
        /// <summary>
        /// List of sections to display
        /// </summary>
        public List<Section> SectionDisplayItems
        {
            get
            {

                if (_SectionDisplayItems == null)
                    _SectionDisplayItems = new List<Section>();
                return _SectionDisplayItems;
            }
            set
            {
                _SectionDisplayItems = value;
                OnPropertyChanged("SectionDisplayItems");
            }
        }


        public async Task UpdateSectionsShow(Section s)
        {            
            IsBusy = true;
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


            List<Section> l = new List<Section>();
            l.Add(s);
            SectionDisplayItems = l;
            ResetSectionsSelection(s);
            IsBusy = false;
        }

        private void ResetSectionsSelection(Section s)
        {
            foreach (Section sec in SectionItems)
                sec.IsSelected = false;
            SectionItems.Where(sec => sec.Title == s.Title).FirstOrDefault().IsSelected = true;
        }

        public async Task UpdateSections()
        {
            IsBusy = true;
            foreach (Section s in SectionItems)
            {
                switch (s.Type)
                {
                    case SectionType.Articles:
                        if (s.LoadOnInit)
                            s.RssItems = await RssService.Instance.GetArticles(s.SourceUrl, (int)s.Type);
                        break;
                    case SectionType.Youtube:
                        if (s.LoadOnInit)
                            s.RssItems = await RssService.Instance.GetYoutubeArticles(s.SourceUrl, (int)s.Type, 400, 300);
                        break;
                    default:
                        break;
                }

            }
            IsBusy = false;
        }

    }
}
