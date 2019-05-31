namespace Xam.Rss.Feeds
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Linq;

    /// <summary>
    /// Atom 1.0 feed item object according to specification: https://validator.w3.org/feed/docs/atom.html
    /// </summary>
    public class AtomFeedItem : BaseFeedItem
    {

        /// <summary>
        /// The url of image
        /// </summary>
        public string UrlImg { get; set; }

        /// <summary>
        /// The description of the feed item
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The "author" element
        /// </summary>
        public AtomPerson Author { get; set; }

        /// <summary>
        /// All "category" elements
        /// </summary>
        public ICollection<string> Categories { get; set; }

        /// <summary>
        /// The "content" element
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// The "contributor" element
        /// </summary>
        public AtomPerson Contributor { get; set; }

        /// <summary>
        /// The "id" element
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The "published" date as string
        /// </summary>
        public string PublishedDateString { get; set; }

        /// <summary>
        /// The "published" element as DateTime. Null if parsing failed or published is empty.
        /// </summary>
        public DateTime? PublishedDate { get; set; }

        /// <summary>
        /// The "rights" element
        /// </summary>
        public string Rights { get; set; }

        /// <summary>
        /// The "source" element
        /// </summary>
        public string Source { get; set; }

        /// <summary>
        /// The "summary" element
        /// </summary>
        public string Summary { get; set; }

        /// <summary>
        /// The "updated" element
        /// </summary>
        public string UpdatedDateString { get; set; }

        /// <summary>
        /// The "updated" element as DateTime. Null if parsing failed or updated is empty
        /// </summary>
        public DateTime? UpdatedDate { get; set; }

        /// <summary>
        /// All "link" elements
        /// </summary>
        public ICollection<AtomLink> Links { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AtomFeedItem"/> class.
        /// default constructor (for serialization)
        /// </summary>
        public AtomFeedItem()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AtomFeedItem"/> class.
        /// Reads an atom feed based on the xml given in item
        /// </summary>
        /// <param name="item">feed item as xml</param>
        public AtomFeedItem(XElement item)
            : base(item)
        {
            this.Link = item.GetElement("link").Attribute("href")?.Value;

            this.Author = new AtomPerson(item.GetElement("author"));

            var categories = item.GetElements("category");
            this.Categories = categories.Select(x => x.GetValue()).ToList();

            this.Content = item.GetValue("content").HtmlDecode();
            this.Contributor = new AtomPerson(item.GetElement("contributor"));
            this.Id = item.GetValue("id");

            this.PublishedDateString = item.GetValue("published");
            this.PublishedDate = Helpers.TryParseDateTime(this.PublishedDateString);
            this.Links = item.GetElements("link").Select(x => new AtomLink(x)).ToList();

            this.Rights = item.GetValue("rights");
            this.Source = item.GetValue("source");
            this.Summary = item.GetValue("summary");
            
            this.UpdatedDateString = item.GetValue("updated");            
            this.UpdatedDate = Helpers.TryParseDateTime(this.UpdatedDateString);

            AddYoutubeInfo(item);
        }

        /// <inheritdoc/>
        internal override FeedItem ToFeedItem()
        {
            FeedItem fi = new FeedItem(this)
            {
                Author = this.Author?.ToString(),
                Categories = this.Categories,
                Content = this.Content,                
                Id = this.Id,
                PublishingDate = this.PublishedDate,
                PublishingDateString = this.PublishedDateString,
                Title = this.Title,
                UrlImg = this.UrlImg,
            };

            if (String.IsNullOrEmpty(this.Description))
                fi.Description = this.Summary;

            return fi;
        }

        private void AddYoutubeInfo(XElement item)
        {
            var subitem = item.GetElement("media:group");
            if (subitem != null)
            {
                this.Description = subitem.GetValue("media:description");
                this.Title = subitem.GetValue("media:title");
                this.UrlImg = subitem.GetElement("media:thumbnail").GetAttributeValue("url");
                String videoId = this.Id.Split(':').LastOrDefault();
                this.Content = "<html><body style=\"background-color:#000000\"><div style=\"text -align:center\"><iframe style=\"background-color:#000000\" class=\"youtube -player\" type =\"text /html\" width =\"{w}\"  height =\"{h}\" src =\"http://www.youtube.com/embed/{i}\" frameborder =\"0\" ></div></body></html>";
                this.Content = this.Content.Replace("{i}", videoId); 
            }
        }
    }
}
