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

        public async void Dispose()
        {
            if (this.context == null)
                return;

            await this.context.SaveChangesAsync()
                .ThrowIfFaulted().ConfigureAwait(false);

            this.context.Dispose();
            this.context = null;
        }
    }
}