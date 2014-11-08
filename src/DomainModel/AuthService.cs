namespace DomainModel
{
    using System;
    using System.Threading.Tasks;

    public class AuthService : IAuthService
    {
        private readonly IUserRepository users;

        public AuthService(IUserRepository users)
        {
            if (users == null)
                throw new ArgumentNullException("users");

            this.users = users;
        }

        public IUserRepository Users
        {
            get { return this.users; }
        }

        public Task<User> FindUserAsync(string id, string password)
        {
            if (id == null)
                throw new ArgumentNullException("id");

            if (password == null)
                throw new ArgumentNullException("password");

            return this.users.SelectAsync(id, password);
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}