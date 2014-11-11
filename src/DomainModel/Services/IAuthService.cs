namespace ArticleHarbor.DomainModel.Services
{
    using System;
    using System.Threading.Tasks;
    using Models;

    public interface IAuthService : IDisposable
    {
        Task<User> FindUserAsync(string id, string password);

        Task<User> FindUserAsync(Guid apiKey);

        Task<bool> HasPermissionsAsync(string actor, Permissions permissions);
    }
}