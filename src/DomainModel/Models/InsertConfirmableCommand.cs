namespace ArticleHarbor.DomainModel.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
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

        public override IEnumerable<IModel> Value
        {
            get { yield break; }
        }

        public IPrincipal Principal
        {
            get { return this.principal; }
        }

        public override Task<IModelCommand<IEnumerable<IModel>>> ExecuteAsync(User user)
        {
            throw new NotSupportedException();
        }

        public override Task<IModelCommand<IEnumerable<IModel>>> ExecuteAsync(Article article)
        {
            this.ConfirmCanCreate("Administrator", "Author");
            return base.ExecuteAsync(article);
        }

        public override Task<IModelCommand<IEnumerable<IModel>>> ExecuteAsync(Bookmark bookmark)
        {
            this.ConfirmCanCreate("Administrator", "Author", "User");
            return base.ExecuteAsync(bookmark);
        }

        public override Task<IModelCommand<IEnumerable<IModel>>> ExecuteAsync(Keyword keyword)
        {
            this.ConfirmCanCreate("Administrator", "Author");
            return base.ExecuteAsync(keyword);
        }

        private void ConfirmCanCreate(params string[] roleNames)
        {
            if (roleNames.All(r => !this.principal.IsInRole(r)))
                throw new UnauthorizedException();
        }
    }
}