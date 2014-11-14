namespace ArticleHarbor.DomainModel.Repositories
{
    using System.Threading.Tasks;
    using Models;
    using IIdentity = Models.IIdentity;

    public interface IUnitOfWork2
    {
        IRepository<Identity<int>, Article> Articles { get; }

        IRepository<Identity<int, string>, Article> Keywords { get; }

        IRepository<Identity<string>, Article> Users { get; }

        IRepository<Identity<string, int>, Article> Bookmarks { get; }

        Task SaveAsync();
    }
}