namespace WebApiPresentationModelUnitTest
{
    using System;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using DomainModel;
    using Microsoft.Owin.Security.OAuth;
    using Moq;
    using Ploeh.AutoFixture;
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
                x => x.FindUserAsync(context.UserName, context.Password)
                    == Task.FromResult<User>(null));

            await sut.GrantResourceOwnerCredentials(context);

            Assert.True(context.HasError);
            Assert.NotNull(context.Error);
            Assert.NotNull(context.ErrorDescription);
        }

        [Test]
        public async Task GrantCorrectResourceOwnerCredentialsValidates(
            ArticleHarborAuthProvider sut,
            OAuthGrantResourceOwnerCredentialsContext context,
            IFixture fixture,
            RoleTypes roleTypes)
        {
            fixture.Inject(RoleTypes.Administrator);
            var expected = new[] { "User", "Author", "Administrator" };
            var user = fixture.Create<User>();
            sut.AuthServiceFactory().Of(
                x => x.FindUserAsync(context.UserName, context.Password)
                    == Task.FromResult<User>(user));

            await sut.GrantResourceOwnerCredentials(context);

            Assert.True(context.IsValidated);
            Assert.Contains(user.Id, context.Ticket.Identity.Name);
            var roles = context.Ticket.Identity.FindAll(ClaimTypes.Role)
                .Select(r => r.Value).ToArray();
            Assert.Equal(expected.Length, roles.Length);
            Assert.Empty(expected.Except(roles));
        }

        [Test]
        public async Task GrantResourceOwnerCredentialsDisposesAutoService(
            ArticleHarborAuthProvider sut,
            OAuthGrantResourceOwnerCredentialsContext context)
        {
            await sut.GrantResourceOwnerCredentials(context);
            sut.AuthServiceFactory().ToMock().Verify(x => x.Dispose());
        }

        [Test]
        public async Task GrantResourceOwnerCredentialsDisposesAutoServiceEvenIfFindUserRolesThrows(
            ArticleHarborAuthProvider sut,
            OAuthGrantResourceOwnerCredentialsContext context)
        {
            sut.AuthServiceFactory().ToMock().Setup(
                x => x.FindUserAsync(context.UserName, context.Password))
                .Throws<Exception>();
            try
            {
                await sut.GrantResourceOwnerCredentials(context);
            }
            catch (Exception)
            {
            }

            sut.AuthServiceFactory().ToMock().Verify(x => x.Dispose());
        }
    }
}