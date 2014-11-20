namespace ArticleHarbor.EFPersistenceModel
{
    using System;
    using System.Linq;
    using DomainModel.Models;
    using EFDataAccess;
    using Ploeh.SemanticComparison.Fluent;
    using Xunit;
    using Article = DomainModel.Models.Article;
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

            Assert.Equal("user1", actual.UserId);
            persistence.AsSource().OfLikeness<Bookmark>().Without(x => x.UserId)
                .ShouldEqual(actual);
        }

        [Test]
        public void ConvertToModelAsyncWithIncorrectUserIdThrows(
            BookmarkRepository2 sut,
            EFDataAccess.Bookmark bookmark)
        {
            var e = Assert.Throws<AggregateException>(
                () => sut.ConvertToModelAsync(bookmark).Result);
            Assert.IsType<ArgumentException>(e.InnerException);
        }

        [Test]
        public void ConvertToPersistenceAsyncReturnsCorrectPersistence(
            ArticleHarborDbContext context,
            BookmarkRepository2 sut)
        {
            var user = context.UserManager.FindByNameAsync("user1").Result;
            var bookmark = new Bookmark("user1", 1);

            var actual = sut.ConvertToPersistenceAsync(bookmark).Result;

            Assert.Equal(user.Id, actual.UserId);
            actual.AsSource().OfLikeness<Bookmark>().Without(x => x.UserId).ShouldEqual(bookmark);
        }

        [Test]
        public void ConvertToPersistenceAsyncWithIncorrectUserIdThrows(
            BookmarkRepository2 sut,
            Bookmark bookmark)
        {
            var e = Assert.Throws<AggregateException>(
                () => sut.ConvertToPersistenceAsync(bookmark).Result);
            Assert.IsType<ArgumentException>(e.InnerException);
        }
    }
}