namespace ArticleHarbor.DomainModel.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
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

            return this.GetAsyncWith(userId);
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

        private async Task<IEnumerable<Article>> GetAsyncWith(string userId)
        {
            var bookmarks = await this.bookmarks.SelectAsync(userId);
            var articleIds = bookmarks.Select(x => x.ArticleId).ToArray();
            return await this.articles.SelectAsync(articleIds);
        }
    }
}