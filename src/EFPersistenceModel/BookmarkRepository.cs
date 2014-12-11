namespace ArticleHarbor.EFPersistenceModel
{
    using System;
    using System.Data.Entity;
    using System.Threading.Tasks;
    using DomainModel.Queries;
    using EFDataAccess;
    using Bookmark = DomainModel.Bookmark;

    public class BookmarkRepository : Repository<Keys<string, int>, Bookmark, EFDataAccess.Bookmark>
    {
        public BookmarkRepository(
            ArticleHarborDbContext context,
            DbSet<EFDataAccess.Bookmark> dbSet)
            : base(context, dbSet)
        {
        }

        public override Task<Bookmark> ConvertToModelAsync(EFDataAccess.Bookmark persistence)
        {
            if (persistence == null)
                throw new ArgumentNullException("persistence");

            var bookmark = new Bookmark(persistence.UserId, persistence.ArticleId);
            return Task.FromResult(bookmark);
        }

        public override Task<EFDataAccess.Bookmark> ConvertToPersistenceAsync(Bookmark model)
        {
            if (model == null)
                throw new ArgumentNullException("model");

            var bookmark = new EFDataAccess.Bookmark
            {
                ArticleId = model.ArticleId,
                UserId = model.UserId
            };
            return Task.FromResult(bookmark);
        }
    }
}