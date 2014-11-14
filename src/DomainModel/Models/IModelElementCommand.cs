namespace ArticleHarbor.DomainModel.Models
{
    public interface IModelElementCommand<TResult>
    {
        TResult Result { get; }

        IModelElementCommand<TResult> Execute(IModelElement<TResult> element);
    }
}