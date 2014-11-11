namespace ArticleHarbor.EFPersistenceModel
{
    using System.Data.Entity;
    using System.Linq;
    using DomainModel.Models;
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

        [Test]
        public void InsertAsyncAddsBookmark(
            DbContextTransaction transaction,
            BookmarkRepository sut)
        {
            try
            {
                var bookmark = new Bookmark("user3", 1);

                sut.InsertAsync(bookmark).Wait();

                sut.Context.SaveChanges();
                var count = sut.Context.Bookmarks.Count();
                Assert.Equal(4, count);
            }
            finally
            {
                transaction.Rollback();
                transaction.Dispose();
            }
        }

        [Test]
        public void DeleteAsyncRemovesBookmark(
            DbContextTransaction transaction,
            BookmarkRepository sut)
        {
            try
            {
                var bookmark = new Bookmark("user1", 1);

                sut.DeleteAsync(bookmark).Wait();

                sut.Context.SaveChanges();
                var count = sut.Context.Bookmarks.Count();
                Assert.Equal(2, count);
            }
            finally
            {
                transaction.Rollback();
                transaction.Dispose();
            }
        }
    }
}