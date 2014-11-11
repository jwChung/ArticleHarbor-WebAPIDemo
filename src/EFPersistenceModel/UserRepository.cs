namespace ArticleHarbor.EFPersistenceModel
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using ArticleHarbor.DomainModel;
    using ArticleHarbor.EFDataAccess;
    using User = DomainModel.User;

    public class UserRepository : IUserRepository
    {
        private readonly ArticleHarborDbContext context;

        public UserRepository(ArticleHarborDbContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            this.context = context;
        }

        public ArticleHarborDbContext Context
        {
            get { return this.context; }
        }

        public Task<User> FindAsync(string id, string password)
        {
            if (id == null)
                throw new ArgumentNullException("id");

            if (password == null)
                throw new ArgumentNullException("password");

            return this.FindAsyncImpl(id, password);
        }

        public async Task<User> FindAsync(Guid apiKey)
        {
            var user = this.context.Users.Local.Where(u => u.ApiKey == apiKey).SingleOrDefault();
            if (user == null)
            {
                await this.context.Users.Where(u => u.ApiKey == apiKey).LoadAsync();
                user = this.context.Users.Local.Where(u => u.ApiKey == apiKey).SingleOrDefault();
            }

            if (user == null)
                return null;

            var roleNames = await this.context.UserManager.GetRolesAsync(user.Id);
            return user.ToDomain(roleNames.Single());
        }

        public Task<User> FindAsync(string id)
        {
            if (id == null)
                throw new ArgumentNullException("id");

            return this.FindAsyncImpl(id);
        }

        private async Task<User> FindAsyncImpl(string id, string password)
        {
            var user = await this.context.UserManager.FindAsync(id, password);
            if (user == null)
                return null;

            var roleNames = await this.context.UserManager.GetRolesAsync(user.Id);
            return user.ToDomain(roleNames.Single());
        }

        private async Task<User> FindAsyncImpl(string id)
        {
            var user = await this.context.UserManager.FindByNameAsync(id);
            if (user == null)
                return null;

            var roleNames = await this.context.UserManager.GetRolesAsync(user.Id);
            return user.ToDomain(roleNames.Single());
        }
    }
}