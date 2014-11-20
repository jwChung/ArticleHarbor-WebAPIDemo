namespace ArticleHarbor.WebApiPresentationModel
{
    using System.Web.Http.Controllers;
    using Xunit;

    public class DependencyParameterBindingTest : IdiomaticTest<DependencyParameterBinding>
    {
        [Test]
        public void SutIsHttpParameterBinding(DependencyParameterBinding sut)
        {
            Assert.IsAssignableFrom<HttpParameterBinding>(sut);
        }
    }
}