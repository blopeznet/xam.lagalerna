using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xam.Rss;
using System.Linq;

namespace Xam.LaGalerna.Services
{
    /// <summary>
    /// Class for manage tumblr API service
    /// </summary>
    public class RssService
    {
       

        private static RssService _Instance;
        public static RssService Instance
        {
            get
            {
                if (_Instance == null)
                    _Instance = new RssService();
                return _Instance;
            }
            set => _Instance = value;
        }

        public async Task<List<Xam.Rss.FeedItem>> GetArticles(String url,int source)
        {                        
            var feed = await Xam.Rss.FeedReader.ReadAsync(url);
            List<Xam.Rss.FeedItem> items = feed.Items.ToList();
            foreach (Xam.Rss.FeedItem item in items)
            {
                List<string> imgs = Xam.Rss.Helpers.GetImgSrcs(item.Content);
                item.Description = Xam.Rss.Helpers.GetClearDescription(item.Content);
                if (imgs != null && imgs.Count > 0)
                    item.UrlImg = imgs.FirstOrDefault();
                item.Related = items;
                item.Number = source;
                item.PublishingDateString = ConvertDate(item.PublishingDateString);
                
            }
            return items;
        }

        public async Task<List<Xam.Rss.FeedItem>> GetYoutubeArticles(String url, int source, double w, double h)
        {
            var feed = await Xam.Rss.FeedReader.ReadAsync(url);
            List<Xam.Rss.FeedItem> items = feed.Items.ToList();
            foreach (Xam.Rss.FeedItem item in items)
            {
                item.Content = item.Content.Replace("{w}", w.ToString());
                item.Content = item.Content.Replace("{h}", h.ToString());
                item.Related = items;
                item.Number = source;
                item.PublishingDateString = ConvertDate(item.PublishingDateString);
            }
            return items;
        }

        /// <summary>
        /// Local spotify playlist
        /// </summary>
        /// <returns></returns>
        public async Task<List<Xam.Rss.FeedItem>> GetSpotifyArticles()
        {
            List<Xam.Rss.FeedItem> items = new List<FeedItem>();

            Xam.Rss.FeedItem i = new FeedItem();
            i.Title = "Camino del Bernabéu ";
            i.Content = "https://open.spotify.com/embed/user/9rk5herb3o3xno3te2dlpx8ga/playlist/5atmXQ3TIrVWBJLRl1RSyz";
            i.Number = 2;            
            items.Add(i);

            i = new FeedItem();
            i.Title = "El madrid siempre vuelve";
            i.Content = "https://open.spotify.com/embed/user/9rk5herb3o3xno3te2dlpx8ga/playlist/4bR7VWFWYijLYFFSbR1WHx";
            i.Number = 2;            
            items.Add(i);

            i = new FeedItem();
            i.Title = "Gramola portanalista";
            i.Content = "https://open.spotify.com/embed/user/9rk5herb3o3xno3te2dlpx8ga/playlist/6be8PI9vWH9OX5BM7SyIVt";
            i.Number = 2;
            items.Add(i);

            i = new FeedItem();
            i.Title = "Himnos oficiosos del Real Madrid";
            i.Content = "https://open.spotify.com/embed/user/9rk5herb3o3xno3te2dlpx8ga/playlist/4Bqd0NoHPcGxtoFOHrUTBg";
            i.Number = 2;            
            items.Add(i);

            i = new FeedItem();
            i.Title = "Perpetuum Real Madrid";
            i.Content = "https://open.spotify.com/embed/user/9rk5herb3o3xno3te2dlpx8ga/playlist/7L7T1BD5SLNUoOnmsnxAjY";
            i.Number = 2;
            items.Add(i);

            i = new FeedItem();            
            i.Title = "Los años de la Quinta";
            i.Content = "https://open.spotify.com/embed/user/9rk5herb3o3xno3te2dlpx8ga/playlist/4T7ImtDYmnbdVzG5P4Y0vj";
            i.Number = 2;            
            items.Add(i);

            return items;
        }


        private String ConvertUrlToText(String url)
        {
            try
            {
                url = url.Substring(url.LastIndexOf("/") + 1).Replace("-", " ");
                url = url.First().ToString().ToUpper() + url.Substring(1);
                return url;
            }catch { return url; }
        }

        private String ConvertDate(String value)
        {
            // Create a DateTime from a String
            string dateString = value;
            DateTime dateFromString =
            DateTime.Parse(dateString, System.Globalization.CultureInfo.InvariantCulture);
            return dateFromString.ToString();
        }
    }
}
