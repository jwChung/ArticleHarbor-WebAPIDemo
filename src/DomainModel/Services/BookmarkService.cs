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

        public BookmarkService(IBookmarkRepository bookmarks)
        {
            if (bookmarks == null)
                throw new ArgumentNullException("bookmarks");

            this.bookmarks = bookmarks;
        }

        public IBookmarkRepository Bookmarks
        {
            get { return this.bookmarks; }
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