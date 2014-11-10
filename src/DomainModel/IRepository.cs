namespace ArticleHarbor.DomainModel
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IRepository<T>
    {
        Task<T> FineAsync(params object[] identity);

        Task<IEnumerable<T>> SelectAsync();

        Task<T> InsertAsync(T item);

        Task UpdateAsync(T item);

        Task DeleteAsync(params object[] identity);
    }
}