namespace ArticleHarbor.EFPersistenceModel
{
    using System.Linq;
    using DomainModel.Repositories;
    using Xunit;

    public class BookmarkRepositoryTest : IdiomaticTest<BookmarkRepository>
    {
        [Test]
        public void SutIsBookmarkRepository(BookmarkRepository sut)
        {
            Assert.IsAssignableFrom<IBookmarkRepository>(sut);
        }

        [Test]
        public void SelectWithUserIdAsyncReturnsCorrectResult(
            BookmarkRepository sut)
        {
            var expected = new[] { 1, 2 };
            var actual = sut.SelectAsync("user1").Result;
            Assert.Equal(expected, actual.Select(x => x.ArticleId));
        }
    }
}