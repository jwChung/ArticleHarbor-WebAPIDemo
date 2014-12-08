namespace ArticleHarbor.DomainModel.Models
{
    using System;
    using System.Collections.Generic;
    using System.Security.Principal;
    using System.Threading.Tasks;
    using Repositories;

    public class UpdateConfirmableCommand : ModelCommand<IEnumerable<IModel>>
    {
        private readonly IRepositories repositories;
        private readonly IPrincipal principal;

        public UpdateConfirmableCommand(IRepositories repositories, IPrincipal principal)
        {
            if (repositories == null)
                throw new ArgumentNullException("repositories");

            if (principal == null)
                throw new ArgumentNullException("principal");

            this.repositories = repositories;
            this.principal = principal;
        }

        public override IEnumerable<IModel> Value
        {
            get { throw new NotImplementedException(); }
        }

        public IPrincipal Principal
        {
            get { return this.principal; }
        }

        public IRepositories Repositories
        {
            get { return this.repositories; }
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
    }
}