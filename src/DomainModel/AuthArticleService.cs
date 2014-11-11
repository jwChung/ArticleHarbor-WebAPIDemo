namespace ArticleHarbor.DomainModel
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class AuthArticleService : IArticleService
    {
        private readonly IAuthService authService;
        private readonly IArticleService innerService;

        public AuthArticleService(IAuthService authService, IArticleService innerService)
        {
            if (authService == null)
                throw new ArgumentNullException("authService");

            if (innerService == null)
                throw new ArgumentNullException("innerService");

            this.authService = authService;
            this.innerService = innerService;
        }

        public IArticleService InnerService
        {
            get { return this.innerService; }
        }

        public IAuthService AuthService
        {
            get { return this.authService; }
        }

        public Task<IEnumerable<Article>> GetAsync()
        {
            return this.innerService.GetAsync();
        }

        public Task<Article> AddAsync(Article article)
        {
            if (article == null)
                throw new ArgumentNullException("article");

            return this.AddAsyncImpl(article);
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

        private async Task<Article> AddAsyncImpl(Article article)
        {
            if (!await this.authService.HasPermissionsAsync(article.UserId, Permissions.Create))
                throw new UnauthorizedException();

            return await this.innerService.AddAsync(article);
        }
    }
}