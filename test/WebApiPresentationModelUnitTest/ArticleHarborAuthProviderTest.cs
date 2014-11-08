namespace WebApiPresentationModelUnitTest
{
    using Microsoft.Owin.Security.OAuth;
    using WebApiPresentationModel;
    using Xunit;

    public class ArticleHarborAuthProviderTest : IdiomaticTest<ArticleHarborAuthProvider>
    {
        [Test]
        public void SutIsOAuthAuthorizationServerProvider(
            ArticleHarborAuthProvider sut)
        {
            Assert.IsAssignableFrom<OAuthAuthorizationServerProvider>(sut);
        }
    }
}