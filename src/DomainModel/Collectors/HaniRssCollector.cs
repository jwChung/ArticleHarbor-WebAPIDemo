namespace ArticleHarbor.DomainModel.Collectors
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml.Linq;
    using Models;
    using Services;

    /// <summary>
    /// 한겨레 신문 뉴스 피드 수집기
    /// </summary>
    public class HaniRssCollector
    {
        private readonly string actor;
        private readonly IArticleService articleService;

        public HaniRssCollector(string actor, IArticleService articleService)
        {
            if (actor == null)
                throw new ArgumentNullException("actor");

            if (articleService == null)
                throw new ArgumentNullException("articleService");

            this.actor = actor;
            this.articleService = articleService;
        }

        public string Actor
        {
            get { return this.actor; }
        }

        public IArticleService ArticleService
        {
            get { return this.articleService; }
        }

        public async Task CollectAsync()
        {
            var articles = await this.GetArticlesAsync();
            await Task.WhenAll(articles.Select(this.articleService.AddAsync));
        }

        private static async Task<Stream> GetStreamAsync()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://www.hani.co.kr");
                var response = await client.GetAsync("rss");
                return await response.Content.ReadAsStreamAsync();
            }
        }

        private async Task<IEnumerable<Article>> GetArticlesAsync()
        {
            using (var reader = new StreamReader(await GetStreamAsync(), Encoding.UTF8))
            {
                return from item in XDocument.Load(reader).Descendants("item")
                       select new Article(
                           -1,
                           "한겨레",
                           item.Element("guid").Value,
                           item.Element("title").Value,
                           item.Element("description").Value,
                           DateTime.Parse(item.Element("pubDate").Value),
                           item.Element("link").Value,
                           this.actor);
            }
        }
    }
}