namespace ArticleHarbor.DomainModel.Services
{
    using System.Threading.Tasks;

    public interface IKeywordService
    {
        Task AddWordsAsync(int id, string subject);

        Task ModifyWordsAsync(int id, string subject);

        Task RemoveWordsAsync(int id);
    }
}