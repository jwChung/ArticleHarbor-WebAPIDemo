namespace ArticleHarbor.DomainModel.Repositories
{
    using System.Threading.Tasks;
    using Models;

    public interface IArticleWordRepository
    {
        Task<ArticleWord> InsertAsync(ArticleWord article);

        Task<ArticleWord> FindAsync(int id, string word);

        Task DeleteAsync(int id);
    }
}