namespace ArticleHarbor.DomainModel.Repositories
{
    using System;
    using System.Threading.Tasks;
    using Models;

    public interface IUserManager : IDisposable
    {
        Task<User> FindAsync(string id, string password);
    }
}