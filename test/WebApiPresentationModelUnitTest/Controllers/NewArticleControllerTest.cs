namespace ArticleHarbor.WebApiPresentationModel.Controllers
{
    using System.Web.Http;
    using Xunit;

    public class NewArticleControllerTest : IdiomaticTest<NewArticleController>
    {
        [Test]
        public void SutIsApiController(NewArticleController sut)
        {
            Assert.IsAssignableFrom<ApiController>(sut);
        }
    }
}