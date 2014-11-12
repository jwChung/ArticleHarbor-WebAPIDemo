namespace ArticleHarbor.DomainModel.Repositories
{
    using System.Threading.Tasks;

    public interface IUnitOfWork
    {
        IArticleRepository Articles { get; }

        IKeywordRepository Keywords { get; }

        IUserRepository Users { get; }

        IBookmarkRepository Bookmarks { get; }

        Task SaveAsync();
    }
}