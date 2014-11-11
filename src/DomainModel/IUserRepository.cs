namespace ArticleHarbor.DomainModel
{
    using System;
    using System.Threading.Tasks;

    public interface IUserRepository : IRepository<User>
    {
        Task<User> FindAsync(string id, string password);

        Task<User> FindAsync(Guid apiKey);
    }
}