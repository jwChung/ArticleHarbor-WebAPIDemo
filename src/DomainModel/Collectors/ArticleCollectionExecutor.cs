namespace ArticleHarbor.DomainModel.Collectors
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Models;
    using Services;

    public class ArticleCollectionExecutor
    {
        private readonly IArticleCollector collector;
        private readonly IArticleService service;
        private readonly int delay;
        private readonly Action<Article> callback;

        /// <summary>
        /// Initializes a new instance of the <see cref="ArticleCollectionExecutor"/> class.
        /// </summary>
        /// <param name="collector">
        /// The collector.
        /// </param>
        /// <param name="service">
        /// The service.
        /// </param>
        /// <param name="delay">
        /// The delay for adding between each article (milliseconds).
        /// </param>
        /// <param name="callback">
        /// The callback called when article is added.
        /// </param>
        public ArticleCollectionExecutor(
            IArticleCollector collector,
            IArticleService service,
            int delay,
            Action<Article> callback)
        {
            if (collector == null)
                throw new ArgumentNullException("collector");

            if (service == null)
                throw new ArgumentNullException("service");

            if (callback == null)
                throw new ArgumentNullException("callback");

            this.collector = collector;
            this.service = service;
            this.delay = delay;
            this.callback = callback;
        }

        public IArticleCollector Collector
        {
            get { return this.collector; }
        }

        public IArticleService Service
        {
            get { return this.service; }
        }

        public int Delay
        {
            get { return this.delay; }
        }

        public Action<Article> Callback
        {
            get { return this.callback; }
        }

        /// <summary>
        /// Executes the asynchronous.
        /// </summary>
        /// <returns>
        /// The number of collected articles.
        /// </returns>
        public async Task<int> ExecuteAsync()
        {
            var articles = await this.collector.CollectAsync();

            int count = 0;
            foreach (var article in articles)
            {
                Thread.Sleep(this.delay);
                count++;
                await this.service.AddAsync(article);
                this.callback(article);
            }

            return count;
        }
    }
}