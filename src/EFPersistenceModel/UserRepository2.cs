namespace ArticleHarbor.EFPersistenceModel
{
    using System;
    using System.Data.Entity;
    using System.Threading.Tasks;
    using DomainModel.Models;

    public class UserRepository2 : Repository<Keys<string>, User, EFDataAccess.User>
    {
        public UserRepository2(DbContext context, DbSet<EFDataAccess.User> dbSet)
            : base(context, dbSet)
        {
        }

        public override Task<User> ConvertToModelAsync(EFDataAccess.User persistence)
        {
            if (persistence == null)
                throw new ArgumentNullException("persistence");

            throw new NotImplementedException();
        }

        public override Task<EFDataAccess.User> ConvertToPersistenceAsync(User model)
        {
            if (model == null)
                throw new ArgumentNullException("model");

            throw new NotImplementedException();
        }
    }
}