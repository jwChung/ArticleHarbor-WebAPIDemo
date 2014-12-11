namespace ArticleHarbor.EFPersistenceModel
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using DomainModel;
    using DomainModel.Models;
    using DomainModel.Repositories;
    using EFDataAccess;
    using User = DomainModel.User;

    public sealed class UserManager : IUserManager
    {
        private readonly ArticleHarborDbContext context;

        public UserManager(ArticleHarborDbContext context)
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

            return this.FindAsyncWith(id, password);
        }

        public Task<User> FindAsync(Guid apiKey)
        {
            if (apiKey == Guid.Empty)
                throw new ArgumentException("The api key should not be empty.");

            return this.FindAsyncWith(apiKey);
        }

        public void Dispose()
        {
            this.context.Dispose();
        }

        private async Task<User> FindAsyncWith(string id, string password)
        {
            var user = await this.context.UserManager.FindByIdAsync(id);
            if (user == null)
                return null;

            var persistence = await this.context.UserManager.FindAsync(user.UserName, password);
            if (persistence == null)
                return null;

            return await this.ConvertToModel(persistence);
        }

        private async Task<User> ConvertToModel(EFDataAccess.User persistence)
        {
            var roleNames = await this.context.UserManager.GetRolesAsync(persistence.Id);

            return new User(
                persistence.Id,
                (Role)Enum.Parse(typeof(Role), roleNames.Single()),
                persistence.ApiKey);
        }

        private async Task<User> FindAsyncWith(Guid apiKey)
        {
            var users = await this.context.Users.Where(x => x.ApiKey == apiKey).ToArrayAsync();

            var persistence = users.SingleOrDefault();
            if (persistence == null)
                return null;

            return await this.ConvertToModel(persistence);
        }
    }
}