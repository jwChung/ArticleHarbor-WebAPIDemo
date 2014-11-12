namespace ArticleHarbor.DomainModel.Repositories
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models;

    public interface IBookmarkRepository
    {
        Task<IEnumerable<Bookmark>> SelectAsync(string userId);

        Task InsertAsync(Bookmark bookmark);

        Task DeleteAsync(Bookmark bookmark);
    }
}