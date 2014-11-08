namespace DomainModel
{
    using System.Threading.Tasks;

    public interface IArticleWordRepository
    {
        void Delete(int articleId);

        Task InsertAsync(ArticleWord articleWord);

        Task DeleteAsync(int articleId);
    }
}