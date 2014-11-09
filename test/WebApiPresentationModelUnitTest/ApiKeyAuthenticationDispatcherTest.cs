namespace ArticleHarbor.WebApiPresentationModel
{
    using System.Net.Http;
    using Xunit;

    public class ApiKeyAuthenticationDispatcherTest : IdiomaticTest<ApiKeyAuthenticationDispatcher>
    {
        [Test]
        public void SutIsDelegatingHandler(ApiKeyAuthenticationDispatcher sut)
        {
            Assert.IsAssignableFrom<DelegatingHandler>(sut);
        }
    }
}