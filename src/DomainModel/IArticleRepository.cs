namespace ArticleHarbor.DomainModel
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IArticleRepository
    {
        Task<IEnumerable<Article>> SelectAsync();

        Task<Article> FineAsync(int id);

        Task<Article> InsertAsync(Article article);

        Task UpdateAsync(Article article);

        Task DeleteAsync(int id);
    }
}