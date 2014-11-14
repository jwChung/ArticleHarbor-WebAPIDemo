namespace ArticleHarbor.DomainModel.Models
{
    public interface IModelElement<TModel>
    {
        TModel Model { get; }

        IModelElementCommand<TResult> Execute<TResult>(IModelElementCommand<TResult> command);
    }
}