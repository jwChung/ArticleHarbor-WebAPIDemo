namespace ArticleHarbor.DomainModel.Collectors
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Models;

    public class CompositeArticleCollector : IArticleCollector
    {
        private readonly IEnumerable<IArticleCollector> collectors;

        public CompositeArticleCollector(IEnumerable<IArticleCollector> collectors)
        {
            if (collectors == null)
                throw new ArgumentNullException("collectors");

            this.collectors = collectors;
        }

        public CompositeArticleCollector(params IArticleCollector[] collectors)
        {
            if (collectors == null)
                throw new ArgumentNullException("collectors");

            this.collectors = collectors;
        }

        public IEnumerable<IArticleCollector> Collectors
        {
            get { return this.collectors; }
        }

        public async Task<IEnumerable<Article>> CollectAsync()
        {
            var articles = await Task.WhenAll(this.collectors.Select(c => c.CollectAsync()));
            return articles.SelectMany(x => x);
        }
    }
}