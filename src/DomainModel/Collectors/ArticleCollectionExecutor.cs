namespace ArticleHarbor.DomainModel.Collectors
{
    using System;
    using Services;

    public class ArticleCollectionExecutor
    {
        private readonly IArticleCollector collector;
        private readonly IArticleService service;

        public ArticleCollectionExecutor(IArticleCollector collector, IArticleService service)
        {
            if (collector == null)
                throw new ArgumentNullException("collector");

            if (service == null)
                throw new ArgumentNullException("service");

            this.collector = collector;
            this.service = service;
        }

        public IArticleCollector Collector
        {
            get { return this.collector; }
        }

        public IArticleService Service
        {
            get { return this.service; }
        }
    }
}