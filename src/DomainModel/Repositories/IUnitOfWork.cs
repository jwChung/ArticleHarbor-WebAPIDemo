namespace ArticleHarbor.DomainModel.Repositories
{
    using System.Threading.Tasks;

    public interface IUnitOfWork
    {
        Task SaveAsync();
    }
}