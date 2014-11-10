namespace ArticleHarbor.DomainModel
{
    using System;
    using System.Collections.Generic;
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

        public Task<IEnumerable<Article>> GetAsync()
        {
            return this.articles.SelectAsync();
        }

        public Task<Article> SaveAsync(Article article)
        {
            if (article == null)
                throw new ArgumentNullException("article");

            return this.SaveAsyncImpl(article);
        }

        public Task RemoveAsync(int id)
        {
            throw new NotImplementedException();
        }

        private async Task<Article> SaveAsyncImpl(Article article)
        {
            var oldArticle = await this.articles.FineAsync(article.Id);
            await this.ArticleWordService.RenewAsync(article.Id, article.Subject);

            if (oldArticle == null)
                return await this.Articles.InsertAsync(article);

            await this.articles.UpdateAsync(article);
            return article;
        }
    }
}