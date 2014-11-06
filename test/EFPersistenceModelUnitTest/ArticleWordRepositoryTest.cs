namespace EFPersistenceModelUnitTest
{
    using DomainModel;
    using EFPersistenceModel;
    using Xunit;

    public class ArticleWordRepositoryTest : IdiomaticTest<ArticleWordRepository>
    {
        [Test]
        public void SutIsArticleWordRepository(ArticleWordRepository sut)
        {
            Assert.IsAssignableFrom<IArticleWordRepository>(sut);
        }
    }
}