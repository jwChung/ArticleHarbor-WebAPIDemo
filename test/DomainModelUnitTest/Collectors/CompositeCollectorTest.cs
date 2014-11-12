namespace ArticleHarbor.DomainModel.Collectors
{
    using Xunit;

    public class CompositeCollectorTest : IdiomaticTest<CompositeCollector>
    {
        [Test]
        public void SutIsArticleCollector(CompositeCollector sut)
        {
            Assert.IsAssignableFrom<IArticleCollector>(sut);
        }
    }
}