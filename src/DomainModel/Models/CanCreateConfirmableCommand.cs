namespace ArticleHarbor.DomainModel.Models
{
    using System;
    using System.Collections.Generic;
    using System.Security.Principal;
    using System.Threading.Tasks;

    public class CanCreateConfirmableCommand : ModelCommand<object>
    {
        private readonly IPrincipal principal;

        public CanCreateConfirmableCommand(IPrincipal principal)
        {
            if (principal == null)
                throw new ArgumentNullException("principal");

            this.principal = principal;
        }

        public override object Result
        {
            get { throw new NotSupportedException("The command does not have any result."); }
        }

        public IPrincipal Principal
        {
            get { return this.principal; }
        }

        public override IModelCommand<object> Execute(Article article)
        {
            if (!this.principal.IsInRole(Role.Author.ToString())
                && !this.principal.IsInRole(Role.Administrator.ToString()))
                throw new UnauthorizedException();

            return base.Execute(article);
        }
    }
}