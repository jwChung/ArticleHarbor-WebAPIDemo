namespace ArticleHarbor.DomainModel.Repositories
{
    using System.Threading.Tasks;
    using Models;

    public interface IUnitOfWork2
    {
        IRepository<KeyCollection<int>, Article> Articles { get; }

        IRepository<KeyCollection<int, string>, Article> Keywords { get; }

        IRepository<KeyCollection<string>, Article> Users { get; }

        IRepository<KeyCollection<string, int>, Article> Bookmarks { get; }

        Task SaveAsync();
    }
}