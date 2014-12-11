namespace ArticleHarbor.EFPersistenceModel
{
    using System;
    using System.Data.Entity;
    using System.Threading.Tasks;
    using ArticleHarbor.EFDataAccess;
    using DomainModel.Repositories;

    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ArticleHarborDbContext context;
        private readonly DbContextTransaction transaction;
        private bool disposed;

        public UnitOfWork(ArticleHarborDbContext context, DbContextTransaction transaction)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            if (transaction == null)
                throw new ArgumentNullException("transaction");

            this.context = context;
            this.transaction = transaction;
        }

        public ArticleHarborDbContext Context
        {
            get { return this.context; }
        }

        public DbContextTransaction Transaction
        {
            get { return this.transaction; }
        }

        public Task SaveAsync()
        {
            return this.context.SaveChangesAsync();
        }

        public Task CommitTransactionAsync()
        {
            throw new NotImplementedException();
        }

        public Task RollbackTransactionAsync()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (this.disposed)
                return;

            if (disposing)
            {
                this.context.Dispose();
            }

            this.disposed = true;
        }
    }
}