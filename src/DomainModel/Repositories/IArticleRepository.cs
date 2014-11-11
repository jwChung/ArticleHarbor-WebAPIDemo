namespace ArticleHarbor.DomainModel.Repositories
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models;

    public interface IArticleRepository
    {
        Task<Article> FindAsync(int id);

        Task<IEnumerable<Article>> SelectAsync();

        Task<Article> InsertAsync(Article article);

        Task UpdateAsync(Article article);

        Task DeleteAsync(int id);
    }
}