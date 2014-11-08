namespace DomainModel
{
    using System;
    using System.Threading.Tasks;

    public interface IAuthService : IDisposable
    {
        Task<UserRoles> FindUserRolesAsync(string id, string password);
    }
}