namespace ArticleHarbor.DomainModel
{
    using System.Threading.Tasks;

    public interface IArticleWordRepository
    {
        Task<ArticleWord> InsertAsync(ArticleWord article);

        Task<ArticleWord> FindAsync(int id, string word);

        Task DeleteAsync(int id);
    }
}