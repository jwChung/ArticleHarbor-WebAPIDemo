namespace ArticleHarbor.EFPersistenceModel
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Globalization;
    using System.Linq;
    using System.Threading.Tasks;
    using ArticleHarbor.DomainModel;
    using ArticleHarbor.EFDataAccess;
    using Article = DomainModel.Article;

    public class ArticleRepository : IArticleRepository
    {
        private readonly ArticleHarborDbContext context;

        public ArticleRepository(ArticleHarborDbContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            this.context = context;
        }

        public ArticleHarborDbContext Context
        {
            get { return this.context; }
        }

        public async Task<IEnumerable<Article>> SelectAsync()
        {
            var articles = await this.context.Articles.Take(50).ToArrayAsync();
            return articles.Select(x => x.ToDomain());
        }

        public Task<Article> FineAsync(int id)
        {
            var article = this.context.Articles.Find(id);
            return Task.FromResult<Article>(
                article == null ? null : article.ToDomain());
        }

        public Task<Article> InsertAsync(Article article)
        {
            if (article == null)
                throw new ArgumentNullException("article");

            return this.InsertAsyncImpl(article);
        }

        public Task UpdateAsync(Article article)
        {
            if (article == null)
                throw new ArgumentNullException("article");

            var persistence = this.context.Articles.Find(article.Id);
            if (persistence != null)
            {
                ((IObjectContextAdapter)this.context).ObjectContext.Detach(persistence);
                this.context.Entry(article.ToPersistence(persistence.UserId)).State
                    = EntityState.Modified;
            }

            return Task.FromResult<object>(null);
        }

        public Task DeleteAsync(int id)
        {
            var article = this.context.Articles.Find(id);
            if (article != null)
                this.context.Articles.Remove(article);

            return Task.FromResult<object>(null);
        }

        private async Task<Article> InsertAsyncImpl(Article article)
        {
            if ((await this.FineAsync(article.Id)) != null)
                return article;

            var user = await this.context.UserManager.FindByNameAsync(article.UserId);
            if (user == null)
                throw new ArgumentException(string.Format(
                    CultureInfo.CurrentCulture,
                    "The user id '{0}' is invalid.",
                    article.UserId));

            var persistence = this.context.Articles.Add(article.ToPersistence(user.Id));
            await this.context.SaveChangesAsync();
            return persistence.ToDomain();
        }
    }
}