namespace ArticleHarbor.DomainModel.Models
{
    public interface IModelElement
    {
        IModelElementCommand<TResult> Execute<TResult>(IModelElementCommand<TResult> command);
    }

    public interface IModelElement<TModel> : IModelElement
    {
        TModel Model { get; }
    }
}