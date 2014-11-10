namespace ArticleHarbor.DomainModel
{
    public interface IAuthRepositoryProvider<T>
    {
        IRepository<T> GetRepository(string userId);
    }
}