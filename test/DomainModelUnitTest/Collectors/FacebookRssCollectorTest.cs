namespace ArticleHarbor.DomainModel.Collectors
{
    using Xunit;

    public class FacebookRssCollectorTest : IdiomaticTest<FacebookRssCollector>
    {
        [Test]
        public void SutIsArticleCollector(FacebookRssCollector sut)
        {
            Assert.IsAssignableFrom<IArticleCollector>(sut);
        }
    }
}