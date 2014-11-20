namespace ArticleHarbor.DomainModel.Models
{
    public interface IModel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "The method is appropriate as property can be serialized.")]
        IKeys GetKeys();

        IModelCommand<TResult> ExecuteCommand<TResult>(IModelCommand<TResult> command);
    }
}