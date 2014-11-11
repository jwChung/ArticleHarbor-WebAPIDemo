namespace ArticleHarbor.DomainModel.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models;

    public interface IBookmarkService
    {
        Task<IEnumerable<Article>> GetAsync(string actor);

        Task AddAsync(Bookmark bookmark);

        Task RemoveAsync(Bookmark bookmark);
    }
}