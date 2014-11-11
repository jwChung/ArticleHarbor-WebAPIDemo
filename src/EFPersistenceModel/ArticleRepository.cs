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
    using EFDataAccess;
    using DomainArticle = DomainModel.Article;
    using DomainUser = DomainModel.User;
    using PersistenceArticle = EFDataAccess.Article;
    using PersistenceUser = EFDataAccess.User;

    public class ArticleRepository : IRepository<DomainArticle>
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

        public Task<DomainArticle> FindAsync(params object[] identity)
        {
            if (identity == null)
                throw new ArgumentNullException("identity");

            var article = this.context.Articles.Find((int)identity[0]);
            return Task.FromResult(
                article == null ? null : article.ToDomain());
        }

        public Task<DomainArticle> InsertAsync(DomainArticle item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            return this.InsertAsyncImpl(item);
        }

        public Task UpdateAsync(DomainArticle item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            var persistenceArticle = this.context.Articles.Find(item.Id);
            if (persistenceArticle != null)
            {
                ((IObjectContextAdapter)this.context).ObjectContext.Detach(persistenceArticle);
                this.context.Entry(item.ToPersistence(persistenceArticle.UserId)).State
                    = EntityState.Modified;
            }

            return Task.FromResult<object>(null);
        }

        public Task DeleteAsync(params object[] identity)
        {
            if (identity == null)
                throw new ArgumentNullException("identity");

            var id = (int)identity[0];
            var article = this.context.Articles.Find(id);
            if (article != null)
                this.context.Articles.Remove(article);

            return Task.FromResult<object>(null);
        }

        private async Task<DomainArticle> InsertAsyncImpl(DomainArticle item)
        {
            if ((await this.FindAsync(new object[] { item.Id })) != null)
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