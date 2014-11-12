namespace ArticleHarbor.DomainModel.Repositories
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models;

    public interface IArticleRepository
    {
        Task<Article> FindAsync(int id);

        Task<IEnumerable<Article>> SelectAsync();

        Task<IEnumerable<Article>> SelectAsync(params int[] ids);

        Task<Article> InsertAsync(Article article);

        Task UpdateAsync(Article article);

        Task DeleteAsync(int id);
    }
}