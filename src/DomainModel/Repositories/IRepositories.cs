namespace ArticleHarbor.DomainModel.Repositories
{
    using Models;

    public interface IRepositories
    {
        IRepository<Keys<int>, Article> Articles { get; }

        IRepository<Keys<int, string>, Keyword> Keywords { get; }

        IRepository<Keys<string, int>, Bookmark> Bookmarks { get; }

        IRepository<Keys<string>, User> Users { get; }
    }
}