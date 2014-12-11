namespace ArticleHarbor.DomainModel
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models;

    public interface IModel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "The method is appropriate because properties can be serialized.")]
        IKeys GetKeys();

        Task<IEnumerable<TReturn>> ExecuteAsync<TReturn>(IModelCommand<TReturn> command);
    }
}