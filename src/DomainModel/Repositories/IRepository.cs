namespace ArticleHarbor.DomainModel.Repositories
{
    using System.Collections.Generic;
    using System.Security.Principal;
    using System.Threading.Tasks;
    using Models;

    public interface IRepository<TId, TModel> where TId : IId
    {
        Task<TModel> Find(TId id);

        Task<IEnumerable<TModel>> SelectAsync();

        Task<TModel> InsertAsync(TModel model);

        Task UpdateAsync(TModel model);

        Task DeleteAsync(TId id);

        Task<IEnumerable<TModel>> ExecuteQueryAsync(IPredicate predicate);
    }
}