namespace ArticleHarbor.DomainModel.Collectors
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Models;
    using Xunit;

    public class CompositeArticleCollectorTest : IdiomaticTest<CompositeArticleCollector>
    {
        [Test]
        public void SutIsArticleCollector(CompositeArticleCollector sut)
        {
            Assert.IsAssignableFrom<IArticleCollector>(sut);
        }

        [Test]
        public void CollectAsyncReturnsAllArticles(
            CompositeArticleCollector sut,
            IEnumerable<Article> articles1,
            IEnumerable<Article> articles2,
            IEnumerable<Article> articles3)
        {
            var collects = sut.Collectors.ToArray();
            Assert.Equal(3, collects.Length);
            collects[0].Of(x => x.CollectAsync() == Task.FromResult(articles1));
            collects[1].Of(x => x.CollectAsync() == Task.FromResult(articles2));
            collects[2].Of(x => x.CollectAsync() == Task.FromResult(articles3));
            var expected = articles1.Concat(articles2).Concat(articles3);

            var actual = sut.CollectAsync().Result;

            Assert.Equal(expected, actual);
        }
    }
}