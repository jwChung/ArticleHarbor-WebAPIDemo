namespace ArticleHarbor.DomainModel
{
    using System.Threading.Tasks;

    public interface IArticleWordService
    {
        Task RenewAsync(int id, string subject);
    }
}