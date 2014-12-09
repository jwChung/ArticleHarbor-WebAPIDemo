namespace ArticleHarbor.EFPersistenceModel
{
    using System.Linq;
    using DomainModel.Models;
    using EFDataAccess;
    using Ploeh.SemanticComparison.Fluent;
    using Xunit;
    using Bookmark = DomainModel.Models.Bookmark;

    public class BookmarkRepository2Test : IdiomaticTest<BookmarkRepository2>
    {
        [Test]
        public void SutIsRepository(BookmarkRepository2 sut)
        {
            Assert.IsAssignableFrom<Repository<Keys<string, int>, Bookmark, EFDataAccess.Bookmark>>(sut);
        }

        [Test]
        public void ConvertToModelAsyncReturnsCorrectModel(
            ArticleHarborDbContext context,
            BookmarkRepository2 sut)
        {
            var persistence = context.Bookmarks.AsNoTracking().Single(x => x.ArticleId == 1);
            var actual = sut.ConvertToModelAsync(persistence).Result;
            persistence.AsSource().OfLikeness<Bookmark>().ShouldEqual(actual);
        }

        [Test]
        public void ConvertToPersistenceAsyncReturnsCorrectPersistence(
            BookmarkRepository2 sut)
        {
            var bookmark = new Bookmark("user1", 1);
            var actual = sut.ConvertToPersistenceAsync(bookmark).Result;
            actual.AsSource().OfLikeness<Bookmark>().ShouldEqual(bookmark);
        }
    }
}