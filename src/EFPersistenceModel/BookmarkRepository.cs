namespace ArticleHarbor.EFPersistenceModel
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using DomainModel.Models;
    using DomainModel.Repositories;
    using DomainBookmark = DomainModel.Models.Bookmark;
    using PersistenceBookmark = EFDataAccess.Bookmark;

    public class BookmarkRepository : IBookmarkRepository
    {
        public Task<IEnumerable<DomainBookmark>> SelectAsync(string userId)
        {
            if (userId == null)
                throw new ArgumentNullException("userId");

            throw new NotImplementedException();
        }

        public Task InsertAsync(DomainBookmark bookmark)
        {
            if (bookmark == null)
                throw new ArgumentNullException("bookmark");

            throw new NotImplementedException();
        }

        public Task DeleteAsync(DomainBookmark bookmark)
        {
            if (bookmark == null)
                throw new ArgumentNullException("bookmark");

            throw new NotImplementedException();
        }
    }
}