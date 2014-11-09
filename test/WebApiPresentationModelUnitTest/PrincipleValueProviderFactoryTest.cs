namespace ArticleHarbor.WebApiPresentationModel
{
    using System.Security.Principal;
    using System.Web.Http.Controllers;
    using System.Web.Http.ValueProviders;
    using Xunit;

    public class PrincipleValueProviderFactoryTest : IdiomaticTest<PrincipleValueProviderFactory>
    {
        [Test]
        public void SutIsValueProviderFactory(PrincipleValueProviderFactory sut)
        {
            Assert.IsAssignableFrom<ValueProviderFactory>(sut);
        }

        [Test]
        public void SutIsUriValueProviderFactory(PrincipleValueProviderFactory sut)
        {
            Assert.IsAssignableFrom<IUriValueProviderFactory>(sut);
        }

        [Test]
        public void GetValueProviderReturnsCorrectProvider(
            PrincipleValueProviderFactory sut,
            HttpActionContext context,
            IPrincipal principal)
        {
            context.RequestContext.Principal = principal;

            var actual = sut.GetValueProvider(context);

            var provider = Assert.IsAssignableFrom<PrincipleValueProvider>(actual);
            Assert.Equal(principal, provider.Principal);
        }
    }
}