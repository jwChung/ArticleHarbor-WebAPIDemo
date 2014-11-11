namespace ArticleHarbor.EFPersistenceModel
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using DomainModel.Models;
    using DomainModel.Repositories;

    public class BookmarkRepository : IBookmarkRepository
    {
        public Task<IEnumerable<Bookmark>> SelectAsync(string userId)
        {
            if (userId == null)
                throw new ArgumentNullException("userId");

            throw new NotImplementedException();
        }

        public Task InsertAsync(Bookmark bookmark)
        {
            if (bookmark == null)
                throw new ArgumentNullException("bookmark");

            throw new NotImplementedException();
        }

        public Task DeleteAsync(Bookmark bookmark)
        {
            if (bookmark == null)
                throw new ArgumentNullException("bookmark");

            throw new NotImplementedException();
        }
    }
}