namespace ArticleHarbor.DomainModel
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Threading.Tasks;

    public class NewArticleService : IArticleService
    {
        private readonly IRepository<Article> articles;
        private readonly IArticleWordService articleWordService;

        public NewArticleService(
            IRepository<Article> articles,
            IArticleWordService articleWordService)
        {
            if (articles == null)
                throw new ArgumentNullException("articles");

            if (articleWordService == null)
                throw new ArgumentNullException("articleWordService");

            this.articles = articles;
            this.articleWordService = articleWordService;
        }

        public IRepository<Article> Articles
        {
            get { return this.articles; }
        }

        public IArticleWordService ArticleWordService
        {
            get { return this.articleWordService; }
        }

        public async Task<string> GetUserIdAsync(int id)
        {
            var article = await this.articles.FineAsync(id);
            if (article != null)
                return article.UserId;

            throw new ArgumentException(
                string.Format(
                    CultureInfo.CurrentCulture,
                    "There is no id '{0}' in article repository.",
                    id),
                "id");
        }

        public Task<IEnumerable<Article>> GetAsync()
        {
            return this.articles.SelectAsync();
        }

        public Task<Article> AddAsync(Article article)
        {
            if (article == null)
                throw new ArgumentNullException("article");

            this.articleWordService.AddWordsAsync(article.Id, article.Subject);

            return this.articles.InsertAsync(article);
        }

        public Task ModifyAsync(string actor, Article article)
        {
            if (actor == null)
                throw new ArgumentNullException("actor");

            if (article == null)
                throw new ArgumentNullException("article");

            throw new NotImplementedException();
        }

        public Task RemoveAsync(string actor, int id)
        {
            if (actor == null)
                throw new ArgumentNullException("actor");

            throw new NotImplementedException();
        }

        public Task<Article> SaveAsync(Article article)
        {
            if (article == null)
                throw new ArgumentNullException("article");

            throw new NotSupportedException();
        }

        public Task RemoveAsync(int id)
        {
            throw new NotSupportedException();
        }
    }
}