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
            get { throw new NotSupportedException("This command does not have any result."); }
        }

        public override IEnumerable<object> Result2
        {
            get { throw new NotImplementedException(); }
        }

        public IPrincipal Principal
        {
            get { return this.principal; }
        }

        public override IModelCommand<object> Execute(Article article)
        {
            this.ConfirmCanCreate();
            return base.Execute(article);
        }

        public override IModelCommand<object> Execute(Keyword keyword)
        {
            this.ConfirmCanCreate();
            return base.Execute(keyword);
        }

        public override IModelCommand<object> Execute(Bookmark bookmark)
        {
            this.ConfirmCanCreate();
            return base.Execute(bookmark);
        }

        public override IModelCommand<object> Execute(User user)
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