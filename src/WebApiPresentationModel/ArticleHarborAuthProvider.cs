namespace WebApiPresentationModel
{
    using System;
    using System.Threading.Tasks;
    using DomainModel;
    using Microsoft.Owin.Security.OAuth;

    public class ArticleHarborAuthProvider : OAuthAuthorizationServerProvider
    {
        private readonly IAuthService authService;

        public ArticleHarborAuthProvider(IAuthService authService)
        {
            if (authService == null)
                throw new ArgumentNullException("authService");

            this.authService = authService;
        }

        public IAuthService AuthService
        {
            get { return this.authService; }
        }

        public override Task ValidateClientAuthentication(
            OAuthValidateClientAuthenticationContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");
            
            context.Validated();

            return base.ValidateClientAuthentication(context);
        }
    }
}