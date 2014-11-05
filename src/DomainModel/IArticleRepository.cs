namespace DomainModel
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IArticleRepository
    {
        Task<IEnumerable<Article>> SelectAsync();

        Task<Article> SelectAsync(int id);

        Task<Article> InsertAsync(Article article);
    }
}