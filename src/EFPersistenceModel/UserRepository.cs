namespace EFPersistenceModel
{
    using System;
    using System.Threading.Tasks;
    using DomainModel;

    public class UserRepository : IUserRepository
    {
        public Task<User> SelectAsync(string id, string password)
        {
            if (id == null)
                throw new ArgumentNullException("id");

            if (password == null)
                throw new ArgumentNullException("password");

            throw new NotImplementedException();
        }
    }
}