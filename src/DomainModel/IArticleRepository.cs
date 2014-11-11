namespace ArticleHarbor.DomainModel
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IArticleRepository
    {
        Task<Article> FindAsync(int id);

        Task<IEnumerable<Article>> SelectAsync();

        Task<Article> InsertAsync(Article article);

        Task UpdateAsync(Article article);

        Task DeleteAsync(int id);
    }
}