namespace ArticleHarbor.EFPersistenceModel
{
    using System;
    using System.Data.Entity;
    using System.Threading.Tasks;
    using DomainModel.Models;

    public class BookmarkRepository2 : Repository<Keys<string, int>, Bookmark, EFDataAccess.Bookmark>
    {
        public BookmarkRepository2(DbContext context, DbSet<EFDataAccess.Bookmark> dbSet)
            : base(context, dbSet)
        {
        }

        public override Task<Bookmark> ConvertToModelAsync(EFDataAccess.Bookmark persistence)
        {
            if (persistence == null)
                throw new ArgumentNullException("persistence");

            throw new NotImplementedException();
        }

        public override Task<EFDataAccess.Bookmark> ConvertToPersistenceAsync(Bookmark model)
        {
            if (model == null)
                throw new ArgumentNullException("model");

            throw new NotImplementedException();
        }
    }
}