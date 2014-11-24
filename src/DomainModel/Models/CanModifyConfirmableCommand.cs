namespace ArticleHarbor.DomainModel.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Principal;
    using System.Threading.Tasks;
    using Repositories;

    public class CanModifyConfirmableCommand : ModelCommand<Task>
    {
        private readonly IPrincipal principal;
        private readonly IUnitOfWork unitOfWork;
        private readonly IEnumerable<Task> result;

        public CanModifyConfirmableCommand(IPrincipal principal, IUnitOfWork unitOfWork)
            : this(principal, unitOfWork, Enumerable.Empty<Task>())
        {
        }

        public CanModifyConfirmableCommand(IPrincipal principal, IUnitOfWork unitOfWork, IEnumerable<Task> result)
        {
            if (principal == null)
                throw new ArgumentNullException("principal");

            if (unitOfWork == null)
                throw new ArgumentNullException("unitOfWork");

            if (result == null)
                throw new ArgumentNullException("result");

            this.principal = principal;
            this.unitOfWork = unitOfWork;
            this.result = result;
        }

        public override IEnumerable<Task> Result
        {
            get { return this.result; }
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