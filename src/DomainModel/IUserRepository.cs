namespace DomainModel
{
    using System;
    using System.Threading.Tasks;

    public interface IUserRepository : IDisposable
    {
        Task<User> SelectAsync(string id, string password);
    }
}