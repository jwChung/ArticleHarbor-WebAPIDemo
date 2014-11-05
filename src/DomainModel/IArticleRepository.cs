namespace DomainModel
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IArticleRepository
    {
        Task<IEnumerable<Article>> SelectAsync();

        Task<Article> SelectAsync(int id);

        Article Insert(Article article);

        Task<Article> InsertAsync(Article article);
    }
}