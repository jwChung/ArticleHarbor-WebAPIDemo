namespace ArticleHarbor.DomainModel.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models;

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

            return this.ModifyAsyncImpl(actor, article);
        }

        public async Task RemoveAsync(string actor, int id)
        {
            if (!await this.authService.HasPermissionsAsync(actor, Permissions.Delete))
                throw new UnauthorizedException();

            var userId = await this.GetUserIdAsync(id);
            if (!await this.authService.HasPermissionsAsync(actor, Permissions.DeleteAny)
               && actor != userId)
                throw new UnauthorizedException();

            await this.innerService.RemoveAsync(actor, id);
        }

        public Task<string> GetUserIdAsync(int id)
        {
            return this.innerService.GetUserIdAsync(id);
        }

        private async Task<Article> AddAsyncImpl(Article article)
        {
            if (!await this.authService.HasPermissionsAsync(article.UserId, Permissions.Create))
                throw new UnauthorizedException();

            return await this.innerService.AddAsync(article);
        }

        private async Task ModifyAsyncImpl(string actor, Article article)
        {
            if (!await this.authService.HasPermissionsAsync(actor, Permissions.Modify))
                throw new UnauthorizedException();

            if (!await this.authService.HasPermissionsAsync(actor, Permissions.ModifyAny)
                && actor != article.UserId)
                throw new UnauthorizedException();

            await this.innerService.ModifyAsync(actor, article);
        }
    }
}