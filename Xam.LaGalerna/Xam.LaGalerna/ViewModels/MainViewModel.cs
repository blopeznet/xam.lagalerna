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
            list = Utils.LoadDataFromApp<List<Section>>("Xam.LaGalerna.Assets.data.sections.json");
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

        /// <summary>
        /// Flag for know if internet is ON
        /// </summary>
        private bool _IsNetworkOk = false;
        public bool IsNetworkOk
        {
            get
            {
                return _IsNetworkOk;
            }

            set
            {
                _IsNetworkOk = value;
                OnPropertyChanged("IsNetworkOk");
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
                    case SectionType.Spotify:
                        s.RssItems = RssService.Instance.GetSpotifyArticles();
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
                    case SectionType.Spotify:
                        if (s.LoadOnInit)
                            s.RssItems = RssService.Instance.GetSpotifyArticles();
                        break;
                    default:
                        break;
                }

            }
            IsBusy = false;
        }

    }
}
