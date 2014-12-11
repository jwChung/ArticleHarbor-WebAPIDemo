namespace ArticleHarbor.EFPersistenceModel
{
    using System;
    using System.Data.Entity;
    using System.Globalization;
    using System.Linq;
    using System.Threading.Tasks;
    using DomainModel;
    using DomainModel.Models;
    using EFDataAccess;
    using User = DomainModel.User;

    public class UserRepository : Repository<Keys<string>, User, EFDataAccess.User>
    {
        private readonly ArticleHarborDbContext context;

        public UserRepository(ArticleHarborDbContext context, DbSet<EFDataAccess.User> dbSet)
            : base(context, dbSet)
        {
            this.context = context;
        }

        public override Task<User> ConvertToModelAsync(EFDataAccess.User persistence)
        {
            if (persistence == null)
                throw new ArgumentNullException("persistence");

            return this.ConvertToModelAsyncWith(persistence);
        }

        public override Task<EFDataAccess.User> ConvertToPersistenceAsync(User model)
        {
            if (model == null)
                throw new ArgumentNullException("model");

            throw new NotImplementedException();
        }

        private async Task<User> ConvertToModelAsyncWith(EFDataAccess.User persistence)
        {
            var roleNames = await this.context.UserManager.GetRolesAsync(persistence.Id);

            return new User(
                persistence.Id,
                (Role)Enum.Parse(typeof(Role), roleNames.Single()),
                persistence.ApiKey);
        }
    }
}