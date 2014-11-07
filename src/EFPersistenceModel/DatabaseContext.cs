namespace EFPersistenceModel
{
    using System;
    using System.Data.Entity;
    using System.Threading.Tasks;
    using DomainModel;
    using EFDataAccess;

    public sealed class DatabaseContext : IDatabaseContext
    {
        private readonly ArticleRepository articles;
        private readonly ArticleWordRepository articleWords;
        private readonly ArticleHarborDbContext context;
        private bool disposed = false;

        public DatabaseContext(ArticleHarborDbContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            this.context = context;
            this.articles = new ArticleRepository(this.context);
            this.articleWords = new ArticleWordRepository(this.context);
        }

        public ArticleHarborDbContext Context
        {
            get { return this.context; }
        }

        public IArticleRepository Articles
        {
            get
            {
                return this.articles;
            }
        }

        public IArticleWordRepository ArticleWords
        {
            get
            {
                return this.articleWords;
            }
        }

        public async void Dispose()
        {
            if (this.disposed)
                return;

            await this.context.SaveChangesAsync().ConfigureAwait(false);
            this.context.Dispose();
            this.disposed = true;
        }
    }
}