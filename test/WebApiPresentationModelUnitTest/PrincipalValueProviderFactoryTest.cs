namespace ArticleHarbor.WebApiPresentationModel
{
    using System.Security.Principal;
    using System.Web.Http.Controllers;
    using System.Web.Http.ValueProviders;
    using Xunit;

    public class PrincipalValueProviderFactoryTest : IdiomaticTest<PrincipalValueProviderFactory>
    {
        [Test]
        public void SutIsValueProviderFactory(PrincipalValueProviderFactory sut)
        {
            Assert.IsAssignableFrom<ValueProviderFactory>(sut);
        }

        [Test]
        public void SutIsUriValueProviderFactory(PrincipalValueProviderFactory sut)
        {
            Assert.IsAssignableFrom<IUriValueProviderFactory>(sut);
        }

        [Test]
        public void GetValueProviderReturnsCorrectProvider(
            PrincipalValueProviderFactory sut,
            HttpActionContext context,
            IPrincipal principal)
        {
            context.RequestContext.Principal = principal;

            var actual = sut.GetValueProvider(context);

            var provider = Assert.IsAssignableFrom<PrincipalValueProvider>(actual);
            Assert.Equal(principal, provider.Principal);
        }
    }
}