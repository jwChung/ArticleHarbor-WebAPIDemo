namespace ArticleHarbor.DomainModel.Models
{
    using System;
    using System.Collections.Generic;
    using System.Security.Principal;
    using System.Threading.Tasks;
    using Repositories;

    public class CanDeleteConfirmableCommand : ModelCommand<Task>
    {
        private readonly IPrincipal principal;
        private readonly IUnitOfWork unitOfWork;

        public CanDeleteConfirmableCommand(IPrincipal principal, IUnitOfWork unitOfWork)
        {
            if (principal == null)
                throw new ArgumentNullException("principal");

            if (unitOfWork == null)
                throw new ArgumentNullException("unitOfWork");
            
            this.principal = principal;
            this.unitOfWork = unitOfWork;
        }

        public override IEnumerable<Task> Result
        {
            get { yield break; }
        }

        public IPrincipal Principal
        {
            get { return this.principal; }
        }

        public IUnitOfWork UnitOfWork
        {
            get { return this.unitOfWork; }
        }
    }
}