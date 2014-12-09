namespace ArticleHarbor.DomainModel.Models
{
    using System;
    using System.Collections.Generic;
    using System.Security.Principal;
    using System.Threading.Tasks;
    using Repositories;

    public class DeleteConfirmableCommand : ModelCommand<IEnumerable<IModel>>
    {
        private readonly IPrincipal principal;
        private readonly IRepositories repositories;

        public DeleteConfirmableCommand(IPrincipal principal, IRepositories repositories)
        {
            if (principal == null)
                throw new ArgumentNullException("principal");

            if (repositories == null)
                throw new ArgumentNullException("repositories");

            this.principal = principal;
            this.repositories = repositories;
        }

        public override IEnumerable<IModel> Value
        {
            get { yield break; }
        }

        public IPrincipal Principal
        {
            get { return this.principal; }
        }

        public IRepositories Repositories
        {
            get { return this.repositories; }
        }

        public override Task<IModelCommand<IEnumerable<IModel>>> ExecuteAsync(User user)
        {
            throw new NotSupportedException();
        }

        public override Task<IModelCommand<IEnumerable<IModel>>> ExecuteAsync(Article article)
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

        public override Task<IModelCommand<IEnumerable<IModel>>> ExecuteAsync(Keyword keyword)
        {
            if (keyword == null)
                throw new ArgumentNullException("keyword");

            return this.ExecuteAsyncWith(keyword);
        }

        private async Task<IModelCommand<IEnumerable<IModel>>> ExecuteAsyncWith(Keyword keyword)
        {
            var article = await this.repositories.Articles.FindAsync(new Keys<int>(keyword.ArticleId));
            return await this.ExecuteAsync(article);
        }
    }
}