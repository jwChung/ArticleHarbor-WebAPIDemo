namespace ArticleHarbor.DomainModel.Models
{
    using System;
    using System.Collections.Generic;
    using System.Security.Principal;
    using System.Threading.Tasks;

    public class CanCreateConfirmableCommand : ModelCommand<IEnumerable<Task>>
    {
        private readonly IPrincipal principal;

        public CanCreateConfirmableCommand(IPrincipal principal)
        {
            if (principal == null)
                throw new ArgumentNullException("principal");

            this.principal = principal;
        }

        public override IEnumerable<Task> Result
        {
            get { return null; }
        }

        public IPrincipal Principal
        {
            get { return this.principal; }
        }
    }
}