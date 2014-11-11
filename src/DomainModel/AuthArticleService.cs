namespace ArticleHarbor.DomainModel
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class AuthArticleService : IArticleService
    {
        public Task<IEnumerable<Article>> GetAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Article> AddAsync(Article article)
        {
            throw new NotImplementedException();
        }

        public Task ModifyAsync(string actor, Article article)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(string actor, int id)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetUserIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Article> SaveAsync(Article article)
        {
            throw new NotSupportedException();
        }

        public Task RemoveAsync(int id)
        {
            throw new NotSupportedException();
        }
    }
}