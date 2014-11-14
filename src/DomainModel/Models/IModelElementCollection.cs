namespace ArticleHarbor.DomainModel.Models
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IModelElementCollection<TModel> : IEnumerable<IModelElement<TModel>>
    {
        IModelElement<TModel> this[IIndentity indentity] { get; }

        IModelElement<TModel> New(TModel model);

        Task<IEnumerable<IModelElement<TModel>>> ExecuteSqlQueryAsync(IPredicate predicate);
    }
}