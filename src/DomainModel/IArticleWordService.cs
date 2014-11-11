namespace ArticleHarbor.DomainModel
{
    using System.Threading.Tasks;

    public interface IArticleWordService
    {
        Task AddWordsAsync(int id, string subject);

        Task ModifyWordsAsync(int id, string subject);

        Task RemoveWordsAsync(int id);
    }
}