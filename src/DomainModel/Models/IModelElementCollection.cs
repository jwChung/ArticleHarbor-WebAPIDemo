namespace ArticleHarbor.DomainModel.Models
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IModelElementCollection<TIndentity, TModel> : IEnumerable<IModelElement<TModel>>
        where TIndentity : IIdentity
    {
        IModelElement<TModel> this[TIndentity indentity] { get; }

        IModelElement<TModel> New(TModel model);

        Task<IEnumerable<IModelElement<TModel>>> ExecuteSqlQueryAsync(IPredicate predicate);
    }
}