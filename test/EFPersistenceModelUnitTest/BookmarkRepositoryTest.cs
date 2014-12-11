namespace ArticleHarbor.EFPersistenceModel
{
    using System.Linq;
    using DomainModel.Models;
    using EFDataAccess;
    using Ploeh.SemanticComparison.Fluent;
    using Xunit;
    using Bookmark = DomainModel.Bookmark;

    public class BookmarkRepositoryTest : IdiomaticTest<BookmarkRepository>
    {
        [Test]
        public void SutIsRepository(BookmarkRepository sut)
        {
            Assert.IsAssignableFrom<Repository<Keys<string, int>, Bookmark, EFDataAccess.Bookmark>>(sut);
        }

        [Test]
        public void ConvertToModelAsyncReturnsCorrectModel(
            ArticleHarborDbContext context,
            BookmarkRepository sut)
        {
            var persistence = context.Bookmarks.AsNoTracking().Single(x => x.ArticleId == 1);
            var actual = sut.ConvertToModelAsync(persistence).Result;
            persistence.AsSource().OfLikeness<Bookmark>().ShouldEqual(actual);
        }

        [Test]
        public void ConvertToPersistenceAsyncReturnsCorrectPersistence(
            BookmarkRepository sut)
        {
            var bookmark = new Bookmark("user1", 1);
            var actual = sut.ConvertToPersistenceAsync(bookmark).Result;
            actual.AsSource().OfLikeness<Bookmark>().ShouldEqual(bookmark);
        }
    }
}