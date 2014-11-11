namespace ArticleHarbor.WebApiPresentationModel.Controllers
{
    using System.Collections.Generic;
    using System.Reflection;
    using System.Threading.Tasks;
    using System.Web.Http;
    using DomainModel.Models;
    using Ploeh.SemanticComparison.Fluent;
    using Xunit;

    public class BookmarksControllerTest : IdiomaticTest<BookmarksController>
    {
        [Test]
        public void SutIsApiController(BookmarksController sut)
        {
            Assert.IsAssignableFrom<ApiController>(sut);
        }

        [Test]
        public void SutHasAuthorizeAttribute()
        {
            var attribute = typeof(BookmarksController).GetCustomAttribute<AuthorizeAttribute>();
            Assert.NotNull(attribute);
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

        [Test]
        public void PostAsyncAddsBookmark(
            BookmarksController sut,
            string userId,
            int articleId)
        {
            sut.User.Identity.Of(x => x.Name == userId);
            var bookmark = new Bookmark(userId, articleId).AsSource()
                .OfLikeness<Bookmark>().CreateProxy();

            sut.PostAsync(articleId).Wait();

            sut.BookmarkService.ToMock().Verify(x => x.AddAsync(bookmark));
        }
    }
}