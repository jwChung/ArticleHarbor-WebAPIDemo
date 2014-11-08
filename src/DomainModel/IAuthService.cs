namespace DomainModel
{
    using System;
    using System.Threading.Tasks;

    public interface IAuthService : IDisposable
    {
        Task<User> FindUserAsync(string id, string password);
    }
}