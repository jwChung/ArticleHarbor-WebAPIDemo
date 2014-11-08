namespace WebApiPresentationModel
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.Owin.Security.OAuth;

    public class ArticleHarborAuthProvider : OAuthAuthorizationServerProvider
    {
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