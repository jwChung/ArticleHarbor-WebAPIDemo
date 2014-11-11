namespace ArticleHarbor.WebApiPresentationModel.Controllers
{
    using System.Web.Http;
    using Xunit;

    public class BookmarksControllerTest : IdiomaticTest<BookmarksController>
    {
        [Test]
        public void SutIsApiController(BookmarksController sut)
        {
            Assert.IsAssignableFrom<ApiController>(sut);
        }

        ////[Test]
        ////public void GetAsyncReturnsCorrectResult(BookmarksController sut)
        ////{
        ////}
    }
}