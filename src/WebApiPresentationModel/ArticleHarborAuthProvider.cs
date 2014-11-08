namespace WebApiPresentationModel
{
    using System;
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
            var userRoles = await this.authServiceFactory().FindUserRolesAsync(
                context.UserName, context.Password);

            if (userRoles == null)
            {
                context.SetError("invalid_grant", "The username or password is incorrect.");
                return;
            }

            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            identity.AddClaim(new Claim(ClaimTypes.Name, userRoles.Id));
            foreach (var role in userRoles.Roles)
                identity.AddClaim(new Claim(ClaimTypes.Role, role));

            context.Validated(identity);
        }
    }
}