namespace ArticleHarbor.DomainModel.Collectors
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models;
    using Moq;
    using Xunit;

    public class ArticleCollectorTransformationTest : IdiomaticTest<ArticleCollectorTransformation>
    {
        [Test]
        public void SutIsArticleCollector(ArticleCollectorTransformation sut)
        {
            Assert.IsAssignableFrom<IArticleCollector>(sut);
        }

        [Test]
        public void CollectAsyncReturnsTransformedArticles(
            ArticleCollectorTransformation sut,
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