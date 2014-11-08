[assembly: Microsoft.Owin.OwinStartup(typeof(Website.IdentityConfig))]

namespace Website
{
    using System;
    using DomainModel;
    using EFDataAccess;
    using EFPersistenceModel;
    using Microsoft.Owin;
    using Microsoft.Owin.Security.OAuth;
    using Owin;
    using WebApiPresentationModel;

    public class IdentityConfig
    {
        public void Configuration(IAppBuilder app)
        {
            Func<IAuthService> authServiceFactory = () =>
            {
                var context = new ArticleHarborDbContext(
                    new ArticleHarborDbContextTestInitializer());
                return new AuthService(new UserRepository(context), context);
            };

            app.UseOAuthBearerTokens(new OAuthAuthorizationServerOptions
            {
                Provider = new ArticleHarborAuthProvider(authServiceFactory),
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/Authenticate")
            });
        }
    }
}