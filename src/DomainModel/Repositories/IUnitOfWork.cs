namespace ArticleHarbor.DomainModel.Repositories
{
    using System.Threading.Tasks;
    using Models;

    public interface IUnitOfWork
    {
        IRepository<Keys<int>, Article> Articles { get; }

        IRepository<Keys<int, string>, Keyword> Keywords { get; }

        IRepository<Keys<string, int>, Bookmark> Bookmarks { get; }

        IRepository<Keys<string>, User> Users { get; }

        Task SaveAsync();
    }
}