using ArticleHarbor.Website;

[assembly: Microsoft.Owin.OwinStartup(typeof(IdentityConfig))]

namespace ArticleHarbor.Website
{
    using System;
    using ArticleHarbor.DomainModel;
    using ArticleHarbor.EFDataAccess;
    using ArticleHarbor.EFPersistenceModel;
    using ArticleHarbor.WebApiPresentationModel;
    using DomainModel.Services;
    using Microsoft.Owin;
    using Microsoft.Owin.Security.OAuth;
    using Owin;

    public class IdentityConfig
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = "The instance of ArticleHarborDbContext is owend by AuthService, which will dispose it.")]
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