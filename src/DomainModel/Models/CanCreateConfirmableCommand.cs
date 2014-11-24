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
            get { yield break; }
        }

        public IPrincipal Principal
        {
            get { return this.principal; }
        }

        public override IModelCommand<IEnumerable<Task>> Execute(Article article)
        {
            this.ConfirmCanCreate();
            return base.Execute(article);
        }

        public override IModelCommand<IEnumerable<Task>> Execute(Keyword keyword)
        {
            this.ConfirmCanCreate();
            return base.Execute(keyword);
        }

        public override IModelCommand<IEnumerable<Task>> Execute(Bookmark bookmark)
        {
            this.ConfirmCanCreate();
            return base.Execute(bookmark);
        }

        public override IModelCommand<IEnumerable<Task>> Execute(User user)
        {
            throw new UnauthorizedException();
        }

        private void ConfirmCanCreate()
        {
            if (!this.principal.IsInRole(Role.Author.ToString())
                && !this.principal.IsInRole(Role.Administrator.ToString()))
                throw new UnauthorizedException();
        }
    }
}