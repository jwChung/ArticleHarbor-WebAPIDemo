namespace ArticleHarbor.DomainModel.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models;
    using Repositories;

    public class BookmarkService : IBookmarkService
    {
        private readonly IBookmarkRepository bookmarks;
        private readonly IArticleRepository articles;

        public BookmarkService(IBookmarkRepository bookmarks, IArticleRepository articles)
        {
            if (bookmarks == null)
                throw new ArgumentNullException("bookmarks");

            if (articles == null)
                throw new ArgumentNullException("articles");

            this.bookmarks = bookmarks;
            this.articles = articles;
        }

        public IBookmarkRepository Bookmarks
        {
            get { return this.bookmarks; }
        }

        public IArticleRepository Articles
        {
            get { return this.articles; }
        }

        public Task<IEnumerable<Article>> GetAsync(string userId)
        {
            if (userId == null)
                throw new ArgumentNullException("userId");

            throw new NotImplementedException();
        }

        public Task AddAsync(Bookmark bookmark)
        {
            if (bookmark == null)
                throw new ArgumentNullException("bookmark");

            throw new NotImplementedException();
        }

        public Task RemoveAsync(Bookmark bookmark)
        {
            if (bookmark == null)
                throw new ArgumentNullException("bookmark");

            throw new NotImplementedException();
        }
    }
}