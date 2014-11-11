namespace ArticleHarbor.DomainModel.Repositories
{
    using System.Threading.Tasks;

    public interface IUnitOfWork
    {
        IArticleRepository Articles { get; }

        IArticleWordRepository ArticleWords { get; }

        IUserRepository Users { get; }

        Task SaveAsync();
    }
}