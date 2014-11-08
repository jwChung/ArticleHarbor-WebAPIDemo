namespace WebApiPresentationModelUnitTest
{
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using DomainModel;
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

        [Test]
        public async Task GrantIncorrectResourceOwnerCredentialsSetsError(
            ArticleHarborAuthProvider sut,
            OAuthGrantResourceOwnerCredentialsContext context)
        {
            sut.AuthServiceFactory().Of(
                x => x.FindUserRolesAsync(context.UserName, context.Password)
                    == Task.FromResult<UserRoles>(null));

            await sut.GrantResourceOwnerCredentials(context);

            Assert.True(context.HasError);
            Assert.NotNull(context.Error);
            Assert.NotNull(context.ErrorDescription);
        }

        [Test]
        public async Task GrantCorrectResourceOwnerCredentialsValidates(
            ArticleHarborAuthProvider sut,
            OAuthGrantResourceOwnerCredentialsContext context,
            UserRoles userRoles)
        {
            sut.AuthServiceFactory().Of(
                x => x.FindUserRolesAsync(context.UserName, context.Password)
                    == Task.FromResult<UserRoles>(userRoles));

            await sut.GrantResourceOwnerCredentials(context);

            Assert.True(context.IsValidated);
            Assert.Contains(userRoles.Id, context.Ticket.Identity.Name);
            Assert.Equal(
                userRoles.Roles,
                context.Ticket.Identity.FindAll(ClaimTypes.Role).Select(r => r.Value));
        }
    }
}