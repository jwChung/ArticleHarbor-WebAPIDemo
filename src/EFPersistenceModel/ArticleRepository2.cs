namespace ArticleHarbor.EFPersistenceModel
{
    using System;
    using System.Data.Entity;
    using DomainModel.Models;

    public class ArticleRepository2 : Repository<Keys<int>, Article, EFDataAccess.Article>
    {
        public ArticleRepository2(DbContext context, DbSet<EFDataAccess.Article> dbSet)
            : base(context, dbSet)
        {
        }

        public override Article ConvertToModel(EFDataAccess.Article persistence)
        {
            if (persistence == null)
                throw new ArgumentNullException("persistence");

            throw new NotImplementedException();
        }

        public override EFDataAccess.Article ConvertToPersistence(Article model)
        {
            if (model == null)
                throw new ArgumentNullException("model");

            throw new NotImplementedException();
        }
    }
}