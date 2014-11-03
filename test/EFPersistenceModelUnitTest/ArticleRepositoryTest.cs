namespace EFPersistenceModelUnitTest
{
    using DomainModel;
    using EFPersistenceModel;
    using Xunit;

    public class ArticleRepositoryTest
    {
        [Test]
        public void SutIsArticleRepository(ArticleRepository sut)
        {
            Assert.IsAssignableFrom<IArticleRepository>(sut);
        } 
    }
}