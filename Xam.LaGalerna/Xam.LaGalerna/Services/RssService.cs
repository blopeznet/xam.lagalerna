﻿using System;
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
