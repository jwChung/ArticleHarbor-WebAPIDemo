namespace ArticleHarbor.EFPersistenceModel
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Globalization;
    using System.Linq;
    using System.Threading.Tasks;
    using DomainModel;
    using DomainModel.Repositories;
    using EFDataAccess;
    using DomainArticle = DomainModel.Models.Article;
    using DomainUser = DomainModel.Models.User;
    using PersistenceArticle = EFDataAccess.Article;
    using PersistenceUser = EFDataAccess.User;

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

        public async Task<IEnumerable<DomainArticle>> SelectAsync()
        {
            var articles = await this.context.Articles.Take(50).ToArrayAsync();
            return articles.Select(x => x.ToDomain());
        }

        public Task<DomainArticle> InsertAsync(DomainArticle article)
        {
            if (article == null)
                throw new ArgumentNullException("article");

            return this.InsertAsyncImpl(article);
        }

        public Task UpdateAsync(DomainArticle article)
        {
            if (article == null)
                throw new ArgumentNullException("article");

            var persistenceArticle = this.context.Articles.Find(article.Id);
            if (persistenceArticle != null)
            {
                ((IObjectContextAdapter)this.context).ObjectContext.Detach(persistenceArticle);
                this.context.Entry(article.ToPersistence(persistenceArticle.UserId)).State
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

        public Task<DomainArticle> FindAsync(int id)
        {
            var article = this.context.Articles.Find(id);
            return Task.FromResult(
                article == null ? null : article.ToDomain());
        }

        private async Task<DomainArticle> InsertAsyncImpl(DomainArticle item)
        {
            if ((await this.FindAsync(item.Id)) != null)
                return item;

            var persistenceArticle = item.ToPersistence(await this.GetUserId(item));
            var newPersistenceArticle = this.context.Articles.Add(persistenceArticle);
            await this.context.SaveChangesAsync();
            return newPersistenceArticle.ToDomain();
        }

        private async Task<string> GetUserId(DomainArticle article)
        {
            var persistenceUser = await this.context.UserManager.FindByNameAsync(article.UserId);
            if (persistenceUser == null)
                throw new ArgumentException(string.Format(
                    CultureInfo.CurrentCulture,
                    "The user id '{0}' is invalid.",
                    article.UserId));

            return persistenceUser.Id;
        }
    }
}