namespace DomainModel
{
    using System.Threading.Tasks;

    public interface IAuthService
    {
        Task<UserRoles> FindUserRolesAsync(string id, string password);
    }
}