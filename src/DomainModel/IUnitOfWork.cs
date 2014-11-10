namespace ArticleHarbor.DomainModel
{
    using System.Threading.Tasks;

    public interface IUnitOfWork
    {
        IRepository<Article> Articles { get; }

        IArticleWordRepository ArticleWords { get; }

        IUserRepository Users { get; }

        Task SaveAsync();
    }
}