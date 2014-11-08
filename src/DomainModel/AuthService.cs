namespace DomainModel
{
    using System;
    using System.Threading.Tasks;

    public class AuthService : IAuthService
    {
        private readonly IUserRepository users;
        private readonly IDisposable owned;

        public AuthService(IUserRepository users, IDisposable owned)
        {
            if (users == null)
                throw new ArgumentNullException("users");

            if (owned == null)
                throw new ArgumentNullException("owned");

            this.users = users;
            this.owned = owned;
        }

        public IUserRepository Users
        {
            get { return this.users; }
        }

        public IDisposable Owned
        {
            get { return this.owned; }
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