namespace ArticleHarbor.EFPersistenceModel
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using DomainModel.Repositories;
    using EFDataAccess;
    using DomainBookmark = DomainModel.Models.Bookmark;

    public class BookmarkRepository : IBookmarkRepository
    {
        private readonly ArticleHarborDbContext context;

        public BookmarkRepository(ArticleHarborDbContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            this.context = context;
        }

        public ArticleHarborDbContext Context
        {
            get { return this.context; }
        }

        public Task<IEnumerable<DomainBookmark>> SelectAsync(string userId)
        {
            if (userId == null)
                throw new ArgumentNullException("userId");

            return this.SelectAsyncWith(userId);
        }

        public Task InsertAsync(DomainBookmark bookmark)
        {
            if (bookmark == null)
                throw new ArgumentNullException("bookmark");

            return this.InsertAsyncWith(bookmark);
        }

        public Task DeleteAsync(DomainBookmark bookmark)
        {
            if (bookmark == null)
                throw new ArgumentNullException("bookmark");

            return this.DeleteAsyncWith(bookmark);
        }

        private async Task<IEnumerable<DomainBookmark>> SelectAsyncWith(string userId)
        {
            var user = await this.context.UserManager.FindByIdAsync(userId);

            var query = from bookmark in this.context.Bookmarks
                        where bookmark.UserId == user.Id
                        select bookmark;
            await query.LoadAsync();

            return this.context.Bookmarks.Local.Select(x => x.ToDomain());
        }

        private async Task InsertAsyncWith(DomainBookmark bookmark)
        {
            var user = await this.context.UserManager.FindByIdAsync(bookmark.UserId);
            this.context.Bookmarks.Add(bookmark.ToPersistence());
        }

        private async Task DeleteAsyncWith(DomainBookmark bookmark)
        {
            var user = await this.context.UserManager.FindByIdAsync(bookmark.UserId);
            var persistenceBookmark = this.context.Bookmarks.Find(user.Id, bookmark.ArticleId);
            this.context.Bookmarks.Remove(persistenceBookmark);
        }
    }
}