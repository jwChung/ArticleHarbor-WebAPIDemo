namespace ArticleHarbor.DomainModel.Models
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IModelElementCollection<TId, TModel> : IEnumerable<IModelElement>
        where TId : IId
    {
        IModelElement this[TId id] { get; }

        IModelElement New(TModel model);

        Task<IEnumerable<IModelElement>> ExecuteQueryAsync(IPredicate predicate);
    }
}