namespace ArticleHarbor.EFPersistenceModel
{
    using System;
    using System.Threading.Tasks;
    using ArticleHarbor.DomainModel;
    using ArticleHarbor.EFDataAccess;
    using DomainModel.Repositories;
    using Article = DomainModel.Models.Article;
    using Keyword = DomainModel.Models.Keyword;

    public class UnitOfWork : IUnitOfWork
    {
        private readonly ArticleHarborDbContext context;
        private readonly ArticleRepository articles;
        private readonly KeywordRepository keywords;
        private readonly UserRepository users;
        private readonly BookmarkRepository bookmarks;

        public UnitOfWork(ArticleHarborDbContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            this.context = context;
            this.articles = new ArticleRepository(this.context);
            this.keywords = new KeywordRepository(this.context);
            this.users = new UserRepository(this.context);
            this.bookmarks = new BookmarkRepository(this.context);
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