namespace ArticleHarbor.DomainModel
{
    using System;
    using System.Threading.Tasks;

    public interface IAuthService : IDisposable
    {
        Task<User> FindUserAsync(string id, string password);

        Task<User> FindUserAsync(string apiKey);
    }
}