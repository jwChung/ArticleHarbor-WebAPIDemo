namespace ArticleHarbor.DomainModel.Collectors
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Models;
    using Ploeh.AutoFixture;
    using Xunit;

    public class ArticleCollectingExecutorTest : IdiomaticTest<ArticleCollectingExecutor>
    {
        [Test]
        public void ExecuteAsyncAddsArticles(
            IEnumerable<Article> articles,
            IFixture fixture)
        {
            fixture.Inject(0);
            var sut = fixture.Create<ArticleCollectingExecutor>();
            sut.Collector.Of(x => x.CollectAsync() == Task.FromResult(articles));

            var actual = sut.ExecuteAsync().Result;

            Assert.Equal(articles.Count(), actual);
            foreach (var article in articles)
                sut.Service.ToMock().Verify(x => x.AddAsync(article));
        }

        [Test]
        public void ExecuteAsyncCallsCallbackWhenAddingArticle(
            IEnumerable<Article> articles,
            IFixture fixture)
        {
            var collected = new List<Article>();
            fixture.Inject(0);
            fixture.Inject<Action<Article>>(collected.Add);
            var sut = fixture.Create<ArticleCollectingExecutor>();
            sut.Collector.Of(x => x.CollectAsync() == Task.FromResult(articles));
            
            var dummy = sut.ExecuteAsync().Result;

            Assert.Equal(collected, articles);
        }
    }
}