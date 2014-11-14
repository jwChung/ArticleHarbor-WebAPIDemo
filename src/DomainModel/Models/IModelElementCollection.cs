namespace ArticleHarbor.DomainModel.Models
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IModelElementCollection<TId, TModel> : IEnumerable<IModelElement<TModel>>
        where TId : IId
    {
        IModelElement<TModel> this[TId id] { get; }

        IModelElement<TModel> New(TModel model);

        Task<IEnumerable<IModelElement<TModel>>> ExecuteQueryAsync(IPredicate predicate);
    }
}