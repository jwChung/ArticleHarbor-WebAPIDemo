namespace WebApiPresentationModelUnitTest
{
    using System.Threading.Tasks;
    using Microsoft.Owin.Security.OAuth;
using Moq;
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

        [Test]
        public async Task ValidateClientAuthenticationAlwaysValidates(
            ArticleHarborAuthProvider sut,
            Mock<OAuthValidateClientAuthenticationContext> context)
        {
            context.CallBase = false;
            await sut.ValidateClientAuthentication(context.Object);
            context.Verify(x => x.Validated());
        }
    }
}