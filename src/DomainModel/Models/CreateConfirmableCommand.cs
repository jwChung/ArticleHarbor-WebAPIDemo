namespace ArticleHarbor.DomainModel.Models
{
    using System;
    using System.Security.Principal;

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
    }
}