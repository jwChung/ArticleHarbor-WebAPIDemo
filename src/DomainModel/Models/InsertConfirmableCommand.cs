namespace ArticleHarbor.DomainModel.Models
{
    using System;
    using System.Collections.Generic;
    using System.Security.Principal;
    using System.Threading.Tasks;

    public class InsertConfirmableCommand : ModelCommand<IEnumerable<IModel>>
    {
        private readonly IPrincipal principal;

        public InsertConfirmableCommand(IPrincipal principal)
        {
            if (principal == null)
                throw new ArgumentNullException("principal");

            this.principal = principal;
        }

        public IPrincipal Principal
        {
            get { return this.principal; }
        }

        public override Task<IModelCommand<IEnumerable<IModel>>> ExecuteAsync(Article article)
        {
            this.ConfirmCanCreate();
            return base.ExecuteAsync(article);
        }

        public override Task<IModelCommand<IEnumerable<IModel>>> ExecuteAsync(Bookmark bookmark)
        {
            this.ConfirmCanCreate();
            return base.ExecuteAsync(bookmark);
        }

        public override Task<IModelCommand<IEnumerable<IModel>>> ExecuteAsync(Keyword keyword)
        {
            this.ConfirmCanCreate();
            return base.ExecuteAsync(keyword);
        }

        public override Task<IModelCommand<IEnumerable<IModel>>> ExecuteAsync(User user)
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