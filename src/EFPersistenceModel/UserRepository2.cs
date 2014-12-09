namespace ArticleHarbor.EFPersistenceModel
{
    using System;
    using System.Data.Entity;
    using System.Globalization;
    using System.Linq;
    using System.Threading.Tasks;
    using DomainModel.Models;
    using EFDataAccess;
    using User = DomainModel.Models.User;

    public class UserRepository2 : Repository<Keys<string>, User, EFDataAccess.User>
    {
        private readonly ArticleHarborDbContext context;

        public UserRepository2(ArticleHarborDbContext context, DbSet<EFDataAccess.User> dbSet)
            : base(context, dbSet)
        {
            this.context = context;
        }

        public override Task<User> FindAsync(Keys<string> keys)
        {
            if (keys == null)
                throw new ArgumentNullException("keys");

            return this.FinaAsyncWith(keys);
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

        private async Task<User> FinaAsyncWith(Keys<string> keys)
        {
            var users = await this.ExecuteSelectCommandAsync(
                new EqualPredicate("UserName", keys.Single()));
            return users.Single();
        }

        private async Task<User> ConvertToModelAsyncWith(EFDataAccess.User persistence)
        {
            var user = await this.context.UserManager.FindByIdAsync(persistence.Id);
            if (user == null)
                throw new ArgumentException(
                    string.Format(
                        CultureInfo.CurrentCulture,
                        "There is no user matched with the id '{0}'.",
                        persistence.Id),
                    "persistence");

            var roleNames = await this.context.UserManager.GetRolesAsync(persistence.Id);

            return new User(
                user.UserName,
                (Role)Enum.Parse(typeof(Role), roleNames.Single()),
                persistence.ApiKey);
        }
    }
}