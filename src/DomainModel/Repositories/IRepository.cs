namespace ArticleHarbor.DomainModel.Repositories
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models;

    public interface IRepository<TKeys, TModel>
        where TKeys : IKeys
        where TModel : IModel
    {
        Task<TModel> FindAsync(TKeys keys);

        Task<IEnumerable<TModel>> SelectAsync();

        Task<TModel> InsertAsync(TModel model);

        Task UpdateAsync(TModel model);

        Task DeleteAsync(TKeys keys);

        Task<IEnumerable<TModel>> ExecuteSelectCommandAsync(IPredicate predicate);

        Task ExecuteDeleteCommandAsync(IPredicate predicate);
    }
}