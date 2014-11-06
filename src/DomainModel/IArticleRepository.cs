namespace DomainModel
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IArticleRepository
    {
        Task<IEnumerable<Article>> SelectAsync();

        Article Select(int id);

        Task<Article> InsertAsync(Article article);

        void Update(Article article);
    }
}