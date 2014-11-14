namespace ArticleHarbor.DomainModel.Repositories
{
    using System.Collections.Generic;
    using System.Security.Principal;
    using System.Threading.Tasks;
    using IIdentity = Models.IIdentity;

    public interface IRepository<TIdentity, TModel> where TIdentity : IIdentity
    {
        Task<TModel> Find(TIdentity identity);

        Task<IEnumerable<TModel>> SelectAsync();

        Task<TModel> InsertAsync(TModel model);

        Task UpdateAsync(TModel model);

        Task DeleteAsync(TModel model);
    }
}