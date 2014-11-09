namespace ArticleHarbor.DomainModel
{
    using System.Threading.Tasks;

    public interface IArticleWordRepository
    {
        Task InsertAsync(ArticleWord articleWord);

        Task DeleteAsync(int articleId);
    }
}