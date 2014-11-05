namespace DomainModel
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IArticleService
    {
        Task<IEnumerable<Article>> GetAsync();

        Article AddOrModify(Article article);

        void Remove(Article article);
    }
}