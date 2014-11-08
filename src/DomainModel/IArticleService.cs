namespace DomainModel
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IArticleService
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "The method form is more appropriate than property form.")]
        Task<IEnumerable<Article>> GetAsync();

        Task<Article> SaveAsync(Article article);

        Task RemoveAsync(int id);
    }
}