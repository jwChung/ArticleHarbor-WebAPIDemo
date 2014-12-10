namespace ArticleHarbor.WebApiPresentationModel.Controllers
{
    using System.Collections.Generic;
    using System.Reflection;
    using System.Threading.Tasks;
    using System.Web.Http;
    using DomainModel.Models;
    using Moq;
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
            User user,
            IEnumerable<Article> articles)
        {
            sut.User.Identity.Of(x => x.Name == userId);
            sut.Repositories.Users.Of(
                x => x.FindAsync(new Keys<string>(userId)) == Task.FromResult(user));
            sut.SelectArticlesCommand.Of(
                x => x.ExecuteAsync(user) == Task.FromResult<IEnumerable<IModel>>(articles));

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

            sut.InsertCommand.ToMock().Verify(x => x.ExecuteAsync(bookmark));
        }

        [Test]
        public void DeleteAsyncRemovesBookmark(
            BookmarksController sut,
            string userId,
            int articleId)
        {
            sut.User.Identity.Of(x => x.Name == userId);
            var bookmark = new Bookmark(userId, articleId).AsSource()
                .OfLikeness<Bookmark>().CreateProxy();

            sut.DeleteAsync(articleId).Wait();

            sut.DeleteCommand.ToMock().Verify(x => x.ExecuteAsync(bookmark));
        }
    }
}