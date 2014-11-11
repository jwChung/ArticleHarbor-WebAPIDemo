namespace ArticleHarbor.DomainModel.Repositories
{
    using System.Collections.Generic;
    using Models;

    public interface IBookmarkRepository
    {
        IEnumerable<Bookmark> SelectAsync(string userId);
    }
}