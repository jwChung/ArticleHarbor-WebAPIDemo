namespace ArticleHarbor.EFPersistenceModel
{
    using DomainModel.Repositories;
    using Xunit;

    public class BookmarkRepositoryTest : IdiomaticTest<BookmarkRepository>
    {
        [Test]
        public void SutIsBookmarkRepository(BookmarkRepository sut)
        {
            Assert.IsAssignableFrom<IBookmarkRepository>(sut);
        }
    }
}