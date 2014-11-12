namespace ArticleHarbor.DomainModel.Collectors
{
    using System;
    using System.Collections.Generic;
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

        public IEnumerable<IArticleCollector> Collectors
        {
            get { return this.collectors; }
        }

        public Task<IEnumerable<Article>> CollectAsync()
        {
            throw new NotImplementedException();
        }
    }
}