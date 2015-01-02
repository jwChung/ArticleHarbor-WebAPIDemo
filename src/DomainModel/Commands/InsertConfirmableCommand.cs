namespace ArticleHarbor.DomainModel.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Principal;
    using System.Threading.Tasks;
    using Models;

    public class InsertConfirmableCommand : EmptyCommand<IModel>
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

        public override Task<IEnumerable<IModel>> ExecuteAsync(User user)
        {
            throw new NotSupportedException();
        }

        public override Task<IEnumerable<IModel>> ExecuteAsync(Article article)
        {
            this.ConfirmCanCreate("Administrator", "Author");
            return base.ExecuteAsync(article);
        }

        public override Task<IEnumerable<IModel>> ExecuteAsync(Bookmark bookmark)
        {
            this.ConfirmCanCreate("Administrator", "Author", "User");
            return base.ExecuteAsync(bookmark);
        }

        public override Task<IEnumerable<IModel>> ExecuteAsync(Keyword keyword)
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