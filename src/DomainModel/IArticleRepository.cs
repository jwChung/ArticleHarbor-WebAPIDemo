namespace DomainModel
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IArticleRepository
    {
        Task<IEnumerable<Article>> SelectAsync();

        Article Insert(Article article);
    }
}