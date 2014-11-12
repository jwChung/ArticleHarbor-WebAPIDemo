namespace ArticleHarbor.DomainModel.Collectors
{
    using System;
    using System.Threading.Tasks;
    using Services;

    /// <summary>
    /// 한겨레 신문 뉴스 피드 수집기
    /// </summary>
    public class HaniCollector
    {
        private readonly string actor;
        private readonly IArticleService articleService;

        public HaniCollector(string actor, IArticleService articleService)
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

        public Task CollectAsync()
        {
            return null;
        }
    }
}