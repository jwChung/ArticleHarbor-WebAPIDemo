namespace ArticleHarbor.EFPersistenceModel
{
    using DomainModel.Models;
    using Xunit;

    public class BookmarkRepository2Test : IdiomaticTest<BookmarkRepository2>
    {
        [Test]
        public void SutIsRepository(BookmarkRepository2 sut)
        {
            Assert.IsAssignableFrom<Repository<Keys<string, int>, Bookmark, EFDataAccess.Bookmark>>(sut);
        }
    }
}