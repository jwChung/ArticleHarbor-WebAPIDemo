namespace ArticleHarbor.WebApiPresentationModel
{
    using System;
    using System.Security.Principal;
    using DomainModel;

    public class SimplePrincipal : IPrincipal, IIdentity
    {
        private readonly string userId;
        private readonly Role role;

        public SimplePrincipal(string userId, Role role)
        {
            if (userId == null)
                throw new ArgumentNullException("userId");

            this.userId = userId;
            this.role = role;
        }

        public string UserId
        {
            get { return this.userId; }
        }

        public Role Role
        {
            get { return this.role; }
        }

        public IIdentity Identity
        {
            get { return this; }
        }

        public string AuthenticationType
        {
            get { return "ApiKey"; }
        }

        public bool IsAuthenticated
        {
            get { return true; }
        }

        public string Name
        {
            get { return this.userId; }
        }

        public bool IsInRole(string role)
        {
            if (role == null)
                throw new ArgumentNullException("role");

            return this.role.ToString().Equals(
                role, StringComparison.CurrentCultureIgnoreCase);
        }
    }
}