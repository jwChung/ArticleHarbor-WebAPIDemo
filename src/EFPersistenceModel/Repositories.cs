namespace ArticleHarbor.EFPersistenceModel
{
    using System;
    using System.Data.Entity;
    using DomainModel.Queries;
    using DomainModel.Repositories;
    using EFDataAccess;
    using Article = DomainModel.Article;
    using Bookmark = DomainModel.Bookmark;
    using Keyword = DomainModel.Keyword;
    using User = DomainModel.User;

    public class Repositories : IRepositories
    {
        private readonly ArticleHarborDbContext context;

        public Repositories(ArticleHarborDbContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            this.context = context;
        }

        public ArticleHarborDbContext Context
        {
            get { return this.context; }
        }

        public IRepository<Keys<int>, Article> Articles
        {
            get { return new ArticleRepository(this.context, this.context.Articles); }
        }

        public IRepository<Keys<int, string>, Keyword> Keywords
        {
            get { return new KeywordRepository(this.context, this.context.Keywords); }
        }

        public IRepository<Keys<string, int>, Bookmark> Bookmarks
        {
            get { return new BookmarkRepository(this.context, this.context.Bookmarks); }
        }

        public IRepository<Keys<string>, User> Users
        {
            get
            {
                return new UserRepository(
                    this.context,
                    (DbSet<EFDataAccess.User>)this.context.Users);
            }
        }
    }
}