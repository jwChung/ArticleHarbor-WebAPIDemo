namespace EFPersistenceModel
{
    using System;
    using System.Data.Entity;
    using DomainModel;
    using EFDataAccess;

    public class DatabaseContext : IDatabaseContext
    {
        private readonly ArticleHarborContext context;

        public DatabaseContext(ArticleHarborContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            this.context = context;
        }

        public IArticleRepository Articles
        {
            get { return null; }
        }

        public ArticleHarborContext Context
        {
            get { return this.context; }
        }

        public int Save()
        {
            throw new System.NotImplementedException();
        }

        public void Dispose()
        {
            throw new System.NotImplementedException();
        }
    }
}