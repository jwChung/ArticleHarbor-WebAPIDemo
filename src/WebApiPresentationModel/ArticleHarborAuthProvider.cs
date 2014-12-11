namespace ArticleHarbor.WebApiPresentationModel
{
    using System;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using ArticleHarbor.DomainModel;
    using DomainModel.Repositories;
    using Microsoft.Owin.Security.OAuth;

    public class ArticleHarborAuthProvider : OAuthAuthorizationServerProvider
    {
        private readonly Func<IUserManager> userManagerFactory;

        public ArticleHarborAuthProvider(Func<IUserManager> userManagerFactory)
        {
            if (userManagerFactory == null)
                throw new ArgumentNullException("userManagerFactory");

            this.userManagerFactory = userManagerFactory;
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
            using (var userManager = this.userManagerFactory())
            {
                user = await userManager.FindAsync(context.UserName, context.Password);
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