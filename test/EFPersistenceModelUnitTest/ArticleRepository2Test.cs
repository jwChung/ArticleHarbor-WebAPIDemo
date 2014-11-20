namespace ArticleHarbor.EFPersistenceModel
{
    using ArticleHarbor.DomainModel.Models;
    using Xunit;

    public class ArticleRepository2Test : IdiomaticTest<ArticleRepository2>
    {
        [Test]
        public void SutIsRepository(ArticleRepository2 sut)
        {
            Assert.IsAssignableFrom<Repository<Keys<int>, Article, EFDataAccess.Article>>(sut);
        }
    }
}