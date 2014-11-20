namespace ArticleHarbor.EFPersistenceModel
{
    using System;
    using System.Data.Entity;
    using System.Threading.Tasks;
    using DomainModel.Models;

    public class KeywordRepository2 : Repository<Keys<int, string>, Keyword, EFDataAccess.Keyword>
    {
        public KeywordRepository2(DbContext context, DbSet<EFDataAccess.Keyword> dbSet)
            : base(context, dbSet)
        {
        }

        public override Task<Keyword> ConvertToModelAsync(EFDataAccess.Keyword persistence)
        {
            if (persistence == null)
                throw new ArgumentNullException("persistence");

            throw new NotImplementedException();
        }

        public override Task<EFDataAccess.Keyword> ConvertToPersistenceAsync(Keyword model)
        {
            if (model == null)
                throw new ArgumentNullException("model");
            
            throw new NotImplementedException();
        }
    }
}