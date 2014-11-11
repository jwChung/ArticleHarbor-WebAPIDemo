namespace ArticleHarbor.EFPersistenceModel
{
    using System;
    using System.Threading.Tasks;
    using ArticleHarbor.DomainModel;
    using ArticleHarbor.EFDataAccess;
    using DomainModel.Repositories;
    using Article = DomainModel.Models.Article;
    using ArticleWord = DomainModel.Models.ArticleWord;

    public class UnitOfWork : IUnitOfWork
    {
        private readonly ArticleHarborDbContext context;
        private readonly ArticleRepository articles;
        private readonly ArticleWordRepository articleWords;
        private readonly UserRepository users;
        private readonly BookmarkRepository bookmarks;

        public UnitOfWork(ArticleHarborDbContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            this.context = context;
            this.articles = new ArticleRepository(this.context);
            this.articleWords = new ArticleWordRepository(this.context);
            this.users = new UserRepository(this.context);
            this.bookmarks = new BookmarkRepository(this.context);
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

        public IUserRepository Users
        {
            get { return this.users; }
        }

        public IBookmarkRepository Bookmarks
        {
            get { return this.bookmarks; }
        }

        public Task SaveAsync()
        {
            return this.Context.SaveChangesAsync();
        }
    }
}