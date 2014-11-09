namespace ArticleHarbor.EFPersistenceModel
{
    using System;
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

        public Task<User> SelectAsync(string id, string password)
        {
            if (id == null)
                throw new ArgumentNullException("id");

            if (password == null)
                throw new ArgumentNullException("password");

            return this.SelectAsyncImpl(id, password);
        }

        private async Task<User> SelectAsyncImpl(string id, string password)
        {
            var user = await this.context.UserManager.FindAsync(id, password);
            var roles = await this.context.UserManager.GetRolesAsync(user.Id);

            return user.ToDomain(roles);
        }
    }
}