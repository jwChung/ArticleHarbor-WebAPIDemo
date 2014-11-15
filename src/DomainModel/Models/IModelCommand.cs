namespace ArticleHarbor.DomainModel.Models
{
    public interface IModelCommand<TResult>
    {
        TResult Result { get; }

        IModelCommand<TResult> Execute(IModel model);
    }
}