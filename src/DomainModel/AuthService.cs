namespace ArticleHarbor.DomainModel
{
    using System;
    using System.Threading.Tasks;

    public class AuthService : IAuthService
    {
        private readonly IUserRepository users;
        private readonly IDisposable owned;
        private bool disposed = false;

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

            return this.users.FindAsync(id, password);
        }

        public Task<User> FindUserAsync(Guid apiKey)
        {
            return this.users.FindAsync(apiKey);
        }

        public Task<bool> HasPermissionsAsync(string userId, Permissions permissions)
        {
            if (userId == null)
                throw new ArgumentNullException("userId");

            throw new NotImplementedException();
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
             if (this.disposed)
                return;

             if (disposing)
            {
                this.owned.Dispose();
                this.disposed = true;
            }
            
             this.disposed = true;
        }
    }
}