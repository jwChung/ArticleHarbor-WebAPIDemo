namespace ArticleHarbor.DomainModel.Services
{
    using Xunit;

    public class BookmarkServiceTest : IdiomaticTest<BookmarkService>
    {
        [Test]
        public void SutIsBookmarkService(BookmarkService sut)
        {
            Assert.IsAssignableFrom<IBookmarkService>(sut);
        }
    }
}