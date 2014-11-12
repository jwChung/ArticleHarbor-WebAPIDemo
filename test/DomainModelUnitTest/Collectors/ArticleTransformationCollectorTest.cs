namespace ArticleHarbor.DomainModel.Collectors
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models;
    using Moq;
    using Xunit;

    public class ArticleTransformationCollectorTest : IdiomaticTest<ArticleTransformationCollector>
    {
        [Test]
        public void SutIsArticleCollector(ArticleTransformationCollector sut)
        {
            Assert.IsAssignableFrom<IArticleCollector>(sut);
        }

        [Test]
        public void CollectAsyncReturnsTransformedArticles(
            ArticleTransformationCollector sut,
            IEnumerable<Article> articles,
            Article[] expected)
        {
            sut.Collector.Of(x => x.CollectAsync() == Task.FromResult(articles));
            int index = 0;
            sut.Transformation.ToMock().Setup(x => x.Transform(It.IsAny<Article>()))
                .Returns(() => expected[index++]);

            var actual = sut.CollectAsync().Result;

            Assert.Equal(expected, actual);
        }
    }
}