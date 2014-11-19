namespace ArticleHarbor.DomainModel.Models
{
    public interface IModel
    {
        IKeys GetKeys();

        IModelCommand<TResult> ExecuteCommand<TResult>(IModelCommand<TResult> command);
    }
}