namespace ArticleHarbor.DomainModel
{
    using System.Threading.Tasks;

    public interface IArticleWordRepository : IRepository<ArticleWord>
    {
        Task DeleteAsync(int id);
    }
}