namespace ArticleHarbor.WebApiPresentationModel
{
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
    }
}