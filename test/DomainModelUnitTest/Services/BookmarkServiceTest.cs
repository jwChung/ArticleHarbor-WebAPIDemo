namespace ArticleHarbor.DomainModel.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Models;
    using Moq;
    using Xunit;

    public class BookmarkServiceTest : IdiomaticTest<BookmarkService>
    {
        [Test]
        public void SutIsBookmarkService(BookmarkService sut)
        {
            Assert.IsAssignableFrom<IBookmarkService>(sut);
        }

        [Test]
        public void GetAsyncWithUserIdReturnsCorrectResult(
            BookmarkService sut,
            string userId,
            IEnumerable<Bookmark> bookmarks,
            IEnumerable<Article> articles)
        {
            sut.Bookmarks.Of(x => x.SelectAsync(userId) == Task.FromResult(bookmarks));
            var articleIds = bookmarks.Select(x => x.ArticleId).ToArray();
            sut.Articles.Of(x => x.SelectAsync(It.Is<int[]>(p => p.SequenceEqual(articleIds)))
                == Task.FromResult(articles));

            var actual = sut.GetAsync(userId).Result;

            Assert.Equal(articles, actual);
        }
    }
}