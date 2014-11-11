namespace ArticleHarbor.DomainModel
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class AuthArticleService : IArticleService
    {
        private readonly IArticleService innerService;

        public AuthArticleService(IArticleService innerService)
        {
            if (innerService == null)
                throw new ArgumentNullException("innerService");

            this.innerService = innerService;
        }

        public IArticleService InnerService
        {
            get { return this.innerService; }
        }

        public Task<IEnumerable<Article>> GetAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Article> AddAsync(Article article)
        {
            if (article == null)
                throw new ArgumentNullException("article");

            throw new NotImplementedException();
        }

        public Task ModifyAsync(string actor, Article article)
        {
            if (actor == null)
                throw new ArgumentNullException("actor");

            if (article == null)
                throw new ArgumentNullException("article");

            throw new NotImplementedException();
        }

        public Task RemoveAsync(string actor, int id)
        {
            if (actor == null)
                throw new ArgumentNullException("actor");

            throw new NotImplementedException();
        }

        public Task<string> GetUserIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Article> SaveAsync(Article article)
        {
            if (article == null)
                throw new ArgumentNullException("article");

            throw new NotSupportedException();
        }

        public Task RemoveAsync(int id)
        {
            throw new NotSupportedException();
        }
    }
}