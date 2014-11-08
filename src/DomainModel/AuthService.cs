namespace DomainModel
{
    using System;
    using System.Threading.Tasks;

    public class AuthService : IAuthService
    {
        public Task<User> FindUserAsync(string id, string password)
        {
            if (id == null)
                throw new ArgumentNullException("id");

            if (password == null)
                throw new ArgumentNullException("password");

            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}