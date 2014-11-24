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

        public CanModifyConfirmableCommand(
            IPrincipal principal, IUnitOfWork unitOfWork, IEnumerable<Task> result)
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
            if (article == null)
                throw new ArgumentNullException("article");

            if (this.IsAdministrator())
                return base.Execute(article);

            if (this.IsAuthorOwner(article.UserId))
                return base.Execute(article);

            throw new UnauthorizedException();
        }

        public override IModelCommand<Task> Execute(Keyword keyword)
        {
            if (keyword == null)
                throw new ArgumentNullException("keyword");

            var task = Task.Run(async () =>
            {
                var article = await this.unitOfWork.Articles.FindAsync(
                    new Keys<int>(keyword.ArticleId));

                this.Execute(article);
            });

            return new CanModifyConfirmableCommand(
                this.principal,
                this.unitOfWork,
                this.result.Concat(new[] { task }));
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