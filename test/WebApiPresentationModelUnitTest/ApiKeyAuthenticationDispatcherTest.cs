namespace ArticleHarbor.WebApiPresentationModel
{
    using System;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web.Http.Controllers;
    using System.Web.Http.Hosting;
    using DomainModel;
    using DomainModel.Models;
    using Moq;
    using Moq.Protected;
    using Ploeh.AutoFixture;
    using Xunit;

    public class ApiKeyAuthenticationDispatcherTest : IdiomaticTest<ApiKeyAuthenticationDispatcher>
    {
        [Test]
        public void SutIsDelegatingHandler(ApiKeyAuthenticationDispatcher sut)
        {
            Assert.IsAssignableFrom<DelegatingHandler>(sut);
        }

        [Test]
        public async Task ExecuteAsyncWithValidAuthenticationCorrectlySetsPrincipal(
            ApiKeyAuthenticationDispatcher sut,
            HttpRequestMessage request,
            HttpRequestContext requestContext,
            Guid apiKey,
            User user)
        {
            request.Headers.Authorization = new AuthenticationHeaderValue(
                "apikey", apiKey.ToString("N"));
            request.Properties[HttpPropertyKeys.RequestContextKey] = requestContext;
            sut.AuthServiceFactory().Of(x => x.FindUserAsync(apiKey) == Task.FromResult(user));

            await sut.ExecuteAsync(request, CancellationToken.None);

            var principal = Assert.IsType<SimplePrincipal>(requestContext.Principal);
            Assert.Equal(user.Id, principal.UserId);
            Assert.Equal(user.Role, principal.Role);
        }

        [Test]
        public async Task ExecuteAsyncWithNullAuthenticationDoesNotSetPrincipal(
            ApiKeyAuthenticationDispatcher sut,
            HttpRequestMessage request,
            HttpRequestContext requestContext)
        {
            Assert.Null(request.Headers.Authorization);
            request.Properties[HttpPropertyKeys.RequestContextKey] = requestContext;
            var expected = requestContext.Principal;

            await sut.ExecuteAsync(request, CancellationToken.None);

            Assert.Equal(expected, requestContext.Principal);
        }

        [Test]
        public async Task ExecuteAsyncWithInvalidSchemeDoesNotSetPrincipal(
            ApiKeyAuthenticationDispatcher sut,
            HttpRequestMessage request,
            HttpRequestContext requestContext,
            string scheme)
        {
            request.Headers.Authorization = new AuthenticationHeaderValue(scheme);
            request.Properties[HttpPropertyKeys.RequestContextKey] = requestContext;
            var expected = requestContext.Principal;

            await sut.ExecuteAsync(request, CancellationToken.None);

            Assert.Equal(expected, requestContext.Principal);
        }

        [Test]
        public async Task ExecuteAsyncWithNullUserDoesNotSetPrincipal(
            ApiKeyAuthenticationDispatcher sut,
            HttpRequestMessage request,
            Guid apiKey,
            HttpRequestContext requestContext)
        {
            request.Headers.Authorization = new AuthenticationHeaderValue(
                "apikey", apiKey.ToString("N"));
            request.Properties[HttpPropertyKeys.RequestContextKey] = requestContext;
            sut.AuthServiceFactory().Of(
                x => x.FindUserAsync(It.IsAny<Guid>()) == Task.FromResult<User>(null));
            var expected = requestContext.Principal;

            await sut.ExecuteAsync(request, CancellationToken.None);

            Assert.Equal(expected, requestContext.Principal);
        }

        [Test]
        public async Task ExecuteAsyncWithValidAuthenticationDisposesAuthService(
            ApiKeyAuthenticationDispatcher sut,
            HttpRequestMessage request,
            HttpRequestContext requestContext,
             Guid apiKey,
             User user)
        {
            request.Headers.Authorization = new AuthenticationHeaderValue(
                "apikey", apiKey.ToString("N"));
            request.Properties[HttpPropertyKeys.RequestContextKey] = requestContext;
            sut.AuthServiceFactory().Of(
                x => x.FindUserAsync(It.IsAny<Guid>()) == Task.FromResult(user));

            await sut.ExecuteAsync(request, CancellationToken.None);

            sut.AuthServiceFactory().ToMock().Verify(x => x.Dispose());
        }

        [Test]
        public async Task ExecuteAsyncDisposesAuthServiceEvenIfExceptionThrows(
            ApiKeyAuthenticationDispatcher sut,
            HttpRequestMessage request,
            Guid apiKey)
        {
            request.Headers.Authorization = new AuthenticationHeaderValue(
                "apikey", apiKey.ToString("N"));
            sut.AuthServiceFactory().ToMock().Setup(x => x.FindUserAsync(Guid.Empty))
                .Throws<Exception>();
            try
            {
                await sut.ExecuteAsync(request, CancellationToken.None);
            }
            catch (Exception)
            {
            }

            sut.AuthServiceFactory().ToMock().Verify(x => x.Dispose());
        }

        [Test]
        public void ExecuteAsyncWithIncorrectFormattedApiKeyThrows(
            ApiKeyAuthenticationDispatcher sut,
            HttpRequestMessage request,
             string apiKey,
             User user)
        {
            request.Headers.Authorization = new AuthenticationHeaderValue(
                "apikey", apiKey);
            sut.AuthServiceFactory().Of(
                x => x.FindUserAsync(It.IsAny<Guid>()) == Task.FromResult(user));

            var e = Assert.Throws<AggregateException>(
                () => sut.ExecuteAsync(request, CancellationToken.None).Wait());
            Assert.IsType<FormatException>(e.InnerException);
        }
    }
}