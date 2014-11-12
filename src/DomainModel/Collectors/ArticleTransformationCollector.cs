namespace ArticleHarbor.DomainModel.Collectors
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Models;

    public class ArticleTransformationCollector : IArticleCollector
    {
        private readonly IArticleCollector collector;
        private readonly IArticleTransformation transformation;

        public ArticleTransformationCollector(
            IArticleCollector collector,
            IArticleTransformation transformation)
        {
            if (collector == null)
                throw new ArgumentNullException("collector");

            if (transformation == null)
                throw new ArgumentNullException("transformation");

            this.collector = collector;
            this.transformation = transformation;
        }

        public IArticleCollector Collector
        {
            get { return this.collector; }
        }

        public IArticleTransformation Transformation
        {
            get { return this.transformation; }
        }

        public async Task<IEnumerable<Article>> CollectAsync()
        {
            var articles = await this.collector.CollectAsync();
            return articles.Select(this.transformation.Transform);
        }
    }
}