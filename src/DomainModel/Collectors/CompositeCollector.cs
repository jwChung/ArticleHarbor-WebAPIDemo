namespace ArticleHarbor.DomainModel.Collectors
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Models;

    public class CompositeCollector : IArticleCollector
    {
        private readonly IEnumerable<IArticleCollector> collectors;

        public CompositeCollector(IEnumerable<IArticleCollector> collectors)
        {
            if (collectors == null)
                throw new ArgumentNullException("collectors");

            this.collectors = collectors;
        }

        public CompositeCollector(params IArticleCollector[] collectors)
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