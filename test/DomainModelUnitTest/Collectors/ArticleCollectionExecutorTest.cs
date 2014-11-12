namespace ArticleHarbor.DomainModel.Collectors
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Models;
    using Ploeh.AutoFixture;
    using Xunit;

    public class ArticleCollectionExecutorTest : IdiomaticTest<ArticleCollectionExecutor>
    {
        [Test]
        public void ExecuteAsyncAddsArticles(
            IEnumerable<Article> articles,
            IFixture fixture)
        {
            fixture.Inject(0);
            var sut = fixture.Create<ArticleCollectionExecutor>();
            sut.Collector.Of(x => x.CollectAsync() == Task.FromResult(articles));

            var actual = sut.ExecuteAsync().Result;

            Assert.Equal(articles.Count(), actual);
            foreach (var article in articles)
                sut.Service.ToMock().Verify(x => x.AddAsync(article));
        }
    }
}