namespace ArticleHarbor.EFPersistenceModel
{
    using System;
    using System.Threading.Tasks;
    using ArticleHarbor.EFDataAccess;
    using DomainModel.Repositories;

    public class UnitOfWork : IUnitOfWork
    {
        private readonly ArticleHarborDbContext context;

        public UnitOfWork(ArticleHarborDbContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            this.context = context;
        }

        public ArticleHarborDbContext Context
        {
            get { return this.context; }
        }

        public Task SaveAsync()
        {
            return this.Context.SaveChangesAsync();
        }
    }
}