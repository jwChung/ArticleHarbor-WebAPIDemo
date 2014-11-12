namespace ArticleHarbor.DomainModel.Services
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Threading.Tasks;
    using Models;
    using Repositories;

    public class ArticleService : IArticleService
    {
        private readonly IArticleRepository articles;
        private readonly IKeywordService keywordService;

        public ArticleService(
            IArticleRepository articles,
            IKeywordService keywordService)
        {
            if (articles == null)
                throw new ArgumentNullException("articles");

            if (keywordService == null)
                throw new ArgumentNullException("keywordService");

            this.articles = articles;
            this.keywordService = keywordService;
        }

        public IArticleRepository Articles
        {
            get { return this.articles; }
        }

        public IKeywordService KeywordService
        {
            get { return this.keywordService; }
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
            await this.keywordService.RemoveWordsAsync(id);
            await this.articles.DeleteAsync(id);
        }

        private async Task<Article> AddAsyncImpl(Article article)
        {
            var addedArticle = await this.articles.InsertAsync(article);
            await this.keywordService.AddWordsAsync(addedArticle.Id, addedArticle.Subject);
            return addedArticle;
        }

        private async Task ModifyAsyncImpl(Article article)
        {
            await this.keywordService.ModifyWordsAsync(article.Id, article.Subject);
            await this.articles.UpdateAsync(article);
        }
    }
}