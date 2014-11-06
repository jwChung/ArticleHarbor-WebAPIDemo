namespace DomainModel
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IArticleRepository
    {
        Task<IEnumerable<Article>> SelectAsync();

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Select", Justification = "The 'Select' word is appropriate to represent repository operation.")]
        Article Select(int id);

        Task<Article> InsertAsync(Article article);

        void Update(Article article);
    }
}