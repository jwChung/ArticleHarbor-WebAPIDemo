namespace ArticleHarbor.DomainModel.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models;

    public interface IArticleService
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "The method form is more appropriate than property form.")]
        Task<IEnumerable<Article>> GetAsync();

        Task<Article> AddAsync(Article article);

        Task ModifyAsync(string actor, Article article);

        Task RemoveAsync(string actor, int id);

        Task<string> GetUserIdAsync(int id);
    }
}