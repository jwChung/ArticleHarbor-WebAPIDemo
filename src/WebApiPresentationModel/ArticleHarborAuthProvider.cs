namespace ArticleHarbor.WebApiPresentationModel
{
    using System;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using ArticleHarbor.DomainModel;
    using DomainModel.Models;
    using DomainModel.Repositories;
    using DomainModel.Services;
    using Microsoft.Owin.Security.OAuth;

    public class ArticleHarborAuthProvider : OAuthAuthorizationServerProvider
    {
        private readonly Func<IAuthService> authServiceFactory;
        private readonly Func<IUserManager> userManagerFactory;

        public ArticleHarborAuthProvider(
            Func<IAuthService> authServiceFactory,
            Func<IUserManager> userManagerFactory)
        {
            if (authServiceFactory == null)
                throw new ArgumentNullException("authServiceFactory");

            if (userManagerFactory == null)
                throw new ArgumentNullException("userManagerFactory");

            this.authServiceFactory = authServiceFactory;
            this.userManagerFactory = userManagerFactory;
        }

        public Func<IAuthService> AuthServiceFactory
        {
            get { return this.authServiceFactory; }
        }

        public Func<IUserManager> UserManagerFactory
        {
            get { return this.userManagerFactory; }
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
                user = await serviceFactory.FindUserAsync(context.UserName, context.Password);
            }

            if (user == null)
            {
                context.SetError("invalid_grant", "The username or password is incorrect.");
                return;
            }

            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            identity.AddClaim(new Claim(ClaimTypes.Name, user.Id));
            identity.AddClaim(new Claim(ClaimTypes.Role, user.Role.ToString()));

            context.Validated(identity);
        }
    }
}