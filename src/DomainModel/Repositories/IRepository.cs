namespace ArticleHarbor.DomainModel.Repositories
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models;
    using Queries;

    public interface IRepository<TModel>
        where TModel : IModel
    {
        Task<IEnumerable<TModel>> SelectAsync();

        Task<IEnumerable<TModel>> SelectAsync(ISqlQuery sqlQuery);

        Task<IEnumerable<TModel>> SelectAsync(IPredicate predicate);

        Task<TModel> InsertAsync(TModel model);

        Task UpdateAsync(TModel model);

        Task DeleteAsync(IPredicate predicate);
    }

    public interface IRepository<TKeys, TModel> : IRepository<TModel>
        where TKeys : IKeys
        where TModel : IModel
    {
        Task<TModel> FindAsync(TKeys keys);

        Task DeleteAsync(TKeys keys);
    }
}