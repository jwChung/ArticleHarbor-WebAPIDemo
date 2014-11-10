namespace ArticleHarbor.DomainModel
{
    using System;

    public class AuthRepositoryProvider<T> : IAuthRepositoryProvider<T>
    {
        public IRepository<T> GetRepository(string userId)
        {
            if (userId == null)
                throw new ArgumentNullException("userId");

            throw new System.NotImplementedException();
        }
    }
}