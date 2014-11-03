namespace WebApiPresentationModel
{
    using System.Web.Http;
    using Xunit;

    public class ArticlesControllerTest
    {
        [Test]
        public void SutIsApiController(ArticlesController sut)
        {
            Assert.IsAssignableFrom<ApiController>(sut);
        }
    }
}