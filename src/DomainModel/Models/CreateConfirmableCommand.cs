namespace ArticleHarbor.DomainModel.Models
{
    using System;
    using System.Security.Principal;
    using System.Threading.Tasks;

    public class CreateConfirmableCommand : ModelCommand<object>
    {
        private readonly IPrincipal principal;

        public CreateConfirmableCommand(IPrincipal principal)
        {
            if (principal == null)
                throw new ArgumentNullException("principal");

            this.principal = principal;
        }

        public IPrincipal Principal
        {
            get { return this.principal; }
        }

        public override Task<IModelCommand<object>> ExecuteAsync(Article article)
        {
            this.ConfirmCanCreate();
            return base.ExecuteAsync(article);
        }

        public override Task<IModelCommand<object>> ExecuteAsync(Bookmark bookmark)
        {
            this.ConfirmCanCreate();
            return base.ExecuteAsync(bookmark);
        }

        public override Task<IModelCommand<object>> ExecuteAsync(Keyword keywords)
        {
            this.ConfirmCanCreate();
            return base.ExecuteAsync(keywords);
        }

        public override Task<IModelCommand<object>> ExecuteAsync(User user)
        {
            throw new UnauthorizedException();
        }

        private void ConfirmCanCreate()
        {
            if (!this.principal.IsInRole("Author") && !this.principal.IsInRole("Administrator"))
                throw new UnauthorizedException();
        }
    }
}