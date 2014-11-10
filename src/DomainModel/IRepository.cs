namespace ArticleHarbor.DomainModel
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IRepository<T>
    {
        Task<Article> FineAsync(params object[] identity);

        Task<IEnumerable<T>> SelectAsync();

        Task<Article> InsertAsync(T article);

        Task UpdateAsync(T article);

        Task DeleteAsync(params object[] identity);
    }
}