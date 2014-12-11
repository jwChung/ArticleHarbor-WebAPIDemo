namespace ArticleHarbor.DomainModel.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Security.Principal;
    using System.Threading.Tasks;
    using Queries;
    using Repositories;

    public class UpdateConfirmableCommand : ModelCommand<IModel>
    {
        private readonly IRepositories repositories;
        private readonly IPrincipal principal;

        public UpdateConfirmableCommand(IPrincipal principal, IRepositories repositories)
        {
            if (repositories == null)
                throw new ArgumentNullException("repositories");

            if (principal == null)
                throw new ArgumentNullException("principal");

            this.repositories = repositories;
            this.principal = principal;
        }

        public IPrincipal Principal
        {
            get { return this.principal; }
        }

        public IRepositories Repositories
        {
            get { return this.repositories; }
        }

        public override Task<IEnumerable<IModel>> ExecuteAsync(User user)
        {
            throw new NotSupportedException();
        }

        public override Task<IEnumerable<IModel>> ExecuteAsync(Article article)
        {
            if (article == null)
                throw new ArgumentNullException("article");

            if (this.principal.IsInRole("Administrator"))
                return base.ExecuteAsync(article);

            if (this.principal.IsInRole("Author") && article.UserId.Equals(
                this.principal.Identity.Name, StringComparison.CurrentCultureIgnoreCase))
                return base.ExecuteAsync(article);

            throw new UnauthorizedException();
        }

        public override Task<IEnumerable<IModel>> ExecuteAsync(Keyword keyword)
        {
            if (keyword == null)
                throw new ArgumentNullException("keyword");

            return this.ExecuteAsyncWith(keyword);
        }

        public override Task<IEnumerable<IModel>> ExecuteAsync(Bookmark bookmark)
        {
            throw new NotSupportedException();
        }

        private async Task<IEnumerable<IModel>> ExecuteAsyncWith(Keyword keyword)
        {
            var article = await this.repositories.Articles.FindAsync(new Keys<int>(keyword.ArticleId));
            return await this.ExecuteAsync(article);
        }
    }
}