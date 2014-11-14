namespace ArticleHarbor.DomainModel.Repositories
{
    using System.Threading.Tasks;
    using Models;

    public interface IUnitOfWork2
    {
        IRepository<Id<int>, Article> Articles { get; }

        IRepository<Id<int, string>, Article> Keywords { get; }

        IRepository<Id<string>, Article> Users { get; }

        IRepository<Id<string, int>, Article> Bookmarks { get; }

        Task SaveAsync();
    }
}