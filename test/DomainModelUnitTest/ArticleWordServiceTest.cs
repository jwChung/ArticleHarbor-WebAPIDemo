namespace ArticleHarbor.DomainModel
{
    using Xunit;

    public class ArticleWordServiceTest : IdiomaticTest<ArticleWordService>
    {
        [Test]
        public void SutIsArticleWordService(ArticleWordService sut)
        {
            Assert.IsAssignableFrom<IArticleWordService>(sut);
        }
    }
}