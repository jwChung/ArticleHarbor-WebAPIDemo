namespace ArticleHarbor.DomainModel.Models
{
    using System;
    using System.Collections.Generic;
    using System.Security.Principal;
    using System.Threading.Tasks;
    using Repositories;

    public class CanModifyConfirmableCommand : ModelCommand<Task>
    {
        private readonly IPrincipal principal;
        private readonly IUnitOfWork unitOfWork;

        public CanModifyConfirmableCommand(IPrincipal principal, IUnitOfWork unitOfWork)
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
            get { throw new NotImplementedException(); }
        }

        public IPrincipal Principal
        {
            get { return this.principal; }
        }

        public IUnitOfWork UnitOfWork
        {
            get { return this.unitOfWork; }
        }

        public override IModelCommand<Task> Execute(Article article)
        {
            if (this.Principal.IsInRole(Role.Administrator.ToString()))
                return base.Execute(article);

            if (this.Principal.IsInRole(Role.Author.ToString()) && article.UserId.Equals(this.principal.Identity.Name, StringComparison.CurrentCultureIgnoreCase))
                return base.Execute(article);

            throw new UnauthorizedException();
        }
    }
}