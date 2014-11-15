namespace ArticleHarbor.DomainModel.Models
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IModelElementCollection<TKeys, TModel> : IEnumerable<IModelElement>
        where TKeys : IKeyCollection
    {
        IModelElement this[TKeys keys] { get; }

        IModelElement New(TModel model);

        Task<IEnumerable<IModelElement>> ExecuteQueryAsync(IPredicate predicate);
    }
}