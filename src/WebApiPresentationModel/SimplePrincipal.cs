namespace ArticleHarbor.WebApiPresentationModel
{
    using System;
    using System.Security.Principal;
    using DomainModel;
    using DomainModel.Models;

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

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1725:ParameterNamesShouldMatchBaseDeclaration", MessageId = "0#", Justification = "This rule is suppressed to prevent same name with a field.")]
        public bool IsInRole(string roleName)
        {
            if (roleName == null)
                throw new ArgumentNullException("roleName");

            return this.role.ToString().Equals(
                roleName, StringComparison.CurrentCultureIgnoreCase);
        }
    }
}