namespace ArticleHarbor.DomainModel.Models
{
    public interface IModelElement<TModel>
    {
        TModel Model { get; }

        IModelElementCommand<T> Execute<T>(IModelElementCommand<T> command);
    }
}