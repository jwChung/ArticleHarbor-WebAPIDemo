namespace ArticleHarbor.DomainModel.Repositories
{
    using System;
    using System.Threading.Tasks;
    using Models;

    public interface IUserRepository
    {
        Task<User> FindAsync(string id, string password);

        Task<User> FindAsync(Guid apiKey);

        Task<User> FindAsync(string id);
    }
}