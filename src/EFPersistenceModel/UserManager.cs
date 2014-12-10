namespace ArticleHarbor.EFPersistenceModel
{
    using System;
    using System.Threading.Tasks;
    using DomainModel.Models;
    using DomainModel.Repositories;
    
    public class UserManager : IUserManager
    {
        public UserManager()
        {
        }

        public Task<User> FindAsync(string id, string password)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}