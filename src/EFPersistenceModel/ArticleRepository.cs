namespace ArticleHarbor.EFPersistenceModel
{
    using System;
    using System.Data.Entity;
    using System.Threading.Tasks;
    using DomainModel.Queries;
    using EFDataAccess;
    using Article = DomainModel.Models.Article;

    public class ArticleRepository : Repository<Keys<int>, Article, EFDataAccess.Article>
    {
        public ArticleRepository(
            ArticleHarborDbContext context, DbSet<EFDataAccess.Article> dbSet)
            : base(context, dbSet)
        {
        }

        public override Task<Article> ConvertToModelAsync(EFDataAccess.Article persistence)
        {
            if (persistence == null)
                throw new ArgumentNullException("persistence");

            return ConvertToModelAsyncWith(persistence);
        }

        public override Task<EFDataAccess.Article> ConvertToPersistenceAsync(Article model)
        {
            if (model == null)
                throw new ArgumentNullException("model");

            return ConvertToPersistenceAsyncWith(model);
        }

        private static Task<Article> ConvertToModelAsyncWith(EFDataAccess.Article persistence)
        {
            var article = new Article(
                persistence.Id,
                persistence.Provider,
                persistence.Guid,
                persistence.Subject,
                persistence.Body,
                persistence.Date,
                persistence.Url,
                persistence.UserId);

            return Task.FromResult(article);
        }

        private static Task<EFDataAccess.Article> ConvertToPersistenceAsyncWith(Article model)
        {
            var article = new EFDataAccess.Article
            {
                Id = model.Id,
                Provider = model.Provider,
                Guid = model.Guid,
                Subject = model.Subject,
                Body = model.Body,
                Date = model.Date,
                Url = model.Url,
                UserId = model.UserId
            };

            return Task.FromResult(article);
        }
    }
}