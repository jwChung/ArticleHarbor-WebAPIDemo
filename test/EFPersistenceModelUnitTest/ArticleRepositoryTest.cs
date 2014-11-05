namespace EFPersistenceModelUnitTest
{
    using DomainModel;
    using EFPersistenceModel;
    using Xunit;

    public class ArticleRepositoryTest : IdiomaticTest<ArticleRepository>
    {
        [Test]
        public void SutIsArticleRepository(ArticleRepository sut)
        {
            Assert.IsAssignableFrom<IArticleRepository>(sut);
        }
    }
}