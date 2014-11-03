namespace EFPersistenceModel
{
    using System;
    using System.Data.Entity;
    using DomainModel;
    using EFDataAccess;

    public sealed class DatabaseContext : IDatabaseContext
    {
        private readonly ArticleRepository articles;
        private ArticleHarborContext context;

        public DatabaseContext(ArticleHarborContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            this.context = context;
            this.articles = new ArticleRepository(this.context.Articles);
        }

        public IArticleRepository Articles
        {
            get
            {
                return this.articles;
            }
        }

        public ArticleHarborContext Context
        {
            get { return this.context; }
        }

        public int Save()
        {
            return this.context.SaveChanges();
        }

        public void Dispose()
        {
            if (this.context == null)
                return;

            this.context.Dispose();
            this.context = null;
        }
    }
}