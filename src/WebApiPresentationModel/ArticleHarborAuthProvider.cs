namespace WebApiPresentationModel
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using DomainModel;
    using Microsoft.Owin.Security.OAuth;

    public class ArticleHarborAuthProvider : OAuthAuthorizationServerProvider
    {
        private readonly Func<IAuthService> authServiceFactory;

        public ArticleHarborAuthProvider(Func<IAuthService> authServiceFactory)
        {
            if (authServiceFactory == null)
                throw new ArgumentNullException("authServiceFactory");

            this.authServiceFactory = authServiceFactory;
        }

        public Func<IAuthService> AuthServiceFactory
        {
            get { return this.authServiceFactory; }
        }

        public override Task ValidateClientAuthentication(
            OAuthValidateClientAuthenticationContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");
            
            context.Validated();

            return base.ValidateClientAuthentication(context);
        }

        public override Task GrantResourceOwnerCredentials(
            OAuthGrantResourceOwnerCredentialsContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            return this.GrantResourceOwnerCredentialsImpl(context);
        }

        private async Task GrantResourceOwnerCredentialsImpl(
            OAuthGrantResourceOwnerCredentialsContext context)
        {
            User user = null;
            using (var serviceFactory = this.authServiceFactory())
            {
                user = await serviceFactory.FindUserAsync(
                context.UserName, context.Password);
            }

            if (user == null)
            {
                context.SetError("invalid_grant", "The username or password is incorrect.");
                return;
            }

            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            identity.AddClaim(new Claim(ClaimTypes.Name, user.Id));

            foreach (string roleName in GetRoleNames(user))
                identity.AddClaim(new Claim(ClaimTypes.Role, roleName));

            context.Validated(identity);
        }

        private static IEnumerable<string> GetRoleNames(User user)
        {
            return Enum.GetValues(typeof(RoleTypes)).Cast<Enum>()
                .Where(x => x.GetHashCode() != 0 && user.Roles.HasFlag(x))
                .Select(x => x.ToString());
        }
    }
}