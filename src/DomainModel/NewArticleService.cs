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
            var article = await this.articles.FindAsync(id);
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

            return this.AddAsyncImpl(article);
        }

        public Task ModifyAsync(string actor, Article article)
        {
            if (article == null)
                throw new ArgumentNullException("article");

            return this.ModifyAsyncImpl(article);
        }

        public async Task RemoveAsync(string actor, int id)
        {
            await this.articleWordService.RemoveWordsAsync(id);
            await this.articles.DeleteAsync(id);
        }

        private async Task<Article> AddAsyncImpl(Article article)
        {
            await this.articleWordService.AddWordsAsync(article.Id, article.Subject);
            return await this.articles.InsertAsync(article);
        }

        private async Task ModifyAsyncImpl(Article article)
        {
            await this.articleWordService.ModifyWordsAsync(article.Id, article.Subject);
            await this.articles.UpdateAsync(article);
        }
    }
}