namespace ArticleHarbor.WebApiPresentationModel.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Web.Http;
    using DomainModel.Models;
    using Xunit;

    public class BookmarksControllerTest : IdiomaticTest<BookmarksController>
    {
        [Test]
        public void SutIsApiController(BookmarksController sut)
        {
            Assert.IsAssignableFrom<ApiController>(sut);
        }

        [Test]
        public void GetAsyncReturnsCorrectResult(
            BookmarksController sut,
            string userId,
            IEnumerable<Article> articles)
        {
            sut.User.Identity.Of(x => x.Name == userId);
            sut.BookmarkService.Of(x => x.GetAsync(userId) == Task.FromResult(articles));

            var actual = sut.GetAsync().Result;

            Assert.Equal(articles, actual);
        }
    }
}