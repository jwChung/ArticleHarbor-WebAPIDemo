namespace ArticleHarbor.DomainModel.Repositories
{
    using System.Threading.Tasks;
    using Models;

    public interface IKeywordRepository
    {
        Task<Keyword> InsertAsync(Keyword article);

        Task<Keyword> FindAsync(int id, string word);

        Task DeleteAsync(int id);
    }
}