namespace ArticleHarbor.DomainModel.Collectors
{
    using Xunit;

    public class CompositeArticleCollectorTest : IdiomaticTest<CompositeArticleCollector>
    {
        [Test]
        public void SutIsArticleCollector(CompositeArticleCollector sut)
        {
            Assert.IsAssignableFrom<IArticleCollector>(sut);
        }
    }
}