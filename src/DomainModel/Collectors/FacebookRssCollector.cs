namespace ArticleHarbor.DomainModel.Collectors
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml.Linq;
    using Models;

    public class FacebookRssCollector : IArticleCollector
    {
        private readonly string author;
        private readonly string facebookId;
        private readonly string facebookName;

        public FacebookRssCollector(string author, string facebookId, string facebookName)
        {
            if (author == null)
                throw new ArgumentNullException("author");

            if (facebookId == null)
                throw new ArgumentNullException("facebookId");

            if (facebookName == null)
                throw new ArgumentNullException("facebookName");

            this.author = author;
            this.facebookId = facebookId;
            this.facebookName = facebookName;
        }

        public string Author
        {
            get { return this.author; }
        }

        public string FacebookId
        {
            get { return this.facebookId; }
        }

        public string FacebookName
        {
            get { return this.facebookName; }
        }

        public async Task<IEnumerable<Article>> CollectAsync()
        {
            using (var reader = new StreamReader(await this.GetStreamAsync(), Encoding.UTF8))
            {
                return from item in XDocument.Load(reader).Descendants("item")
                       select this.ConvertToArticle(item);
            }
        }

        private async Task<Stream> GetStreamAsync()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://www.wallflux.com/feed/");
                var response = await client.GetAsync(this.facebookId);
                return await response.Content.ReadAsStreamAsync();
            }
        }

        private Article ConvertToArticle(XElement item)
        {
            return new Article(
                id: -1,
                provider: "페이스북",
                guid: item.Element("guid").Value,
                subject: item.Element("title").Value,
                body: item.Element("description").Value,
                date: DateTime.Parse(item.Element("pubDate").Value, CultureInfo.CurrentCulture),
                url: item.Element("link").Value,
                userId: this.author);
        }
    }
}