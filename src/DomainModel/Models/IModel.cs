namespace ArticleHarbor.DomainModel.Models
{
    public interface IModel
    {
        IModelCommand<TResult> ExecuteCommand<TResult>(IModelCommand<TResult> command);
    }
}