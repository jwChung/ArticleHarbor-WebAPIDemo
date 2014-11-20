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

            var model = new Keyword(persistence.ArticleId, persistence.Word);
            return Task.FromResult(model);
        }

        public override Task<EFDataAccess.Keyword> ConvertToPersistenceAsync(Keyword model)
        {
            if (model == null)
                throw new ArgumentNullException("model");

            var persistence = new EFDataAccess.Keyword
            {
                ArticleId = model.ArticleId,
                Word = model.Word
            };
            return Task.FromResult(persistence);
        }
    }
}