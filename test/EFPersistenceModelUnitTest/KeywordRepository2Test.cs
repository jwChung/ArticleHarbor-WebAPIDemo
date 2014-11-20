namespace ArticleHarbor.EFPersistenceModel
{
    using DomainModel.Models;
    using Xunit;

    public class KeywordRepository2Test : IdiomaticTest<KeywordRepository2>
    {
        [Test]
        public void SutIsRepository(KeywordRepository2 sut)
        {
            Assert.IsAssignableFrom<Repository<Keys<int, string>, Keyword, EFDataAccess.Keyword>>(sut);
        }
    }
}