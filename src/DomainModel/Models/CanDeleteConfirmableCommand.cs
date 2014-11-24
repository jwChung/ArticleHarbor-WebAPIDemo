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

        public override IModelCommand<Task> Execute(User user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            if (this.IsAdministrator())
                return base.Execute(user);

            if (this.IsAuthorOwner(user.Id))
                return base.Execute(user);

            throw new UnauthorizedException();
        }

        public override IModelCommand<Task> Execute(Article article)
        {
            if (article == null)
                throw new ArgumentNullException("article");

            if (this.IsAdministrator())
                return base.Execute(article);

            if (this.IsAuthorOwner(article.UserId))
                return base.Execute(article);

            throw new UnauthorizedException();
        }
        
        public override IModelCommand<Task> Execute(Bookmark bookmark)
        {
            if (bookmark == null)
                throw new ArgumentNullException("bookmark");

            if (this.IsAdministrator())
                return base.Execute(bookmark);

            if (this.IsAuthorOwner(bookmark.UserId))
                return base.Execute(bookmark);

            throw new UnauthorizedException();
        }

        private bool IsAdministrator()
        {
            return this.principal.IsInRole(Role.Administrator.ToString());
        }

        private bool IsAuthorOwner(string userId)
        {
            bool isOwner = userId.Equals(
                this.principal.Identity.Name, StringComparison.CurrentCultureIgnoreCase);

            return this.principal.IsInRole(Role.Author.ToString()) && isOwner;
        }
    }
}