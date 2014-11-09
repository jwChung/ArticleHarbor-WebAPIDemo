namespace ArticleHarbor.WebApiPresentationModel
{
    using System.Web.Http.Filters;
    using Xunit;

    public class PermissionAuthorizationFilterAttributeTest
        : IdiomaticTest<PermissionAuthorizationFilterAttribute>
    {
        [Test]
        public void SutIsAuthorizationAttribute(
            PermissionAuthorizationFilterAttribute sut)
        {
            Assert.IsAssignableFrom<AuthorizationFilterAttribute>(sut);
        }
    }
}