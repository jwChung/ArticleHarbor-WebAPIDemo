namespace ArticleHarbor.DomainModel.Models
{
    using System.Threading.Tasks;

    public interface IModel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "The method is appropriate as property can be serialized.")]
        IKeys GetKeys();

        IModelCommand<TValue> Execute<TValue>(IModelCommand<TValue> command);

        Task<IModelCommand<TValue>> ExecuteAsync<TValue>(IModelCommand<TValue> command);
    }
}