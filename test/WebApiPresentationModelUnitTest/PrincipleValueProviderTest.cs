namespace ArticleHarbor.WebApiPresentationModel
{
    using System.Web.Http.ValueProviders;
    using Xunit;

    public class PrincipleValueProviderTest : IdiomaticTest<PrincipleValueProvider>
    {
        [Test]
        public void SutIsValueProvider(
            PrincipleValueProvider sut)
        {
            Assert.IsAssignableFrom<IValueProvider>(sut);
        }
    }
}