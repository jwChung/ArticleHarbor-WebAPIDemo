namespace ArticleHarbor.WebApiPresentationModel
{
    using System;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using ArticleHarbor.DomainModel;
    using Microsoft.Owin.Security.OAuth;
    using Moq;
    using Ploeh.AutoFixture;
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
            sut.UserManagerFactory().Of(
                x => x.FindAsync(context.UserName, context.Password)
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
            IFixture fixture)
        {
            fixture.Inject(Role.Author);
            var user = fixture.Create<User>();
            sut.UserManagerFactory().Of(
                x => x.FindAsync(context.UserName, context.Password)
                    == Task.FromResult<User>(user));

            await sut.GrantResourceOwnerCredentials(context);

            Assert.True(context.IsValidated);
            Assert.Contains(user.Id, context.Ticket.Identity.Name);
            var actualRoles = context.Ticket.Identity.FindAll(ClaimTypes.Role)
                .Select(r => r.Value);
            Assert.Equal("Author", actualRoles.Single());
        }

        [Test]
        public async Task GrantResourceOwnerCredentialsDisposesAutoService(
            ArticleHarborAuthProvider sut,
            OAuthGrantResourceOwnerCredentialsContext context)
        {
            await sut.GrantResourceOwnerCredentials(context);
            sut.UserManagerFactory().ToMock().Verify(x => x.Dispose());
        }

        [Test]
        public async Task GrantResourceOwnerCredentialsDisposesAutoServiceEvenIfFindUserRolesThrows(
            ArticleHarborAuthProvider sut,
            OAuthGrantResourceOwnerCredentialsContext context)
        {
            sut.UserManagerFactory().ToMock().Setup(
                x => x.FindAsync(context.UserName, context.Password))
                .Throws<Exception>();
            try
            {
                await sut.GrantResourceOwnerCredentials(context);
            }
            catch (Exception)
            {
            }

            sut.UserManagerFactory().ToMock().Verify(x => x.Dispose());
        }
    }
}