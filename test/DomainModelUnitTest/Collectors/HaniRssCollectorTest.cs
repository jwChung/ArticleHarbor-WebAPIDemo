namespace ArticleHarbor.DomainModel.Collectors
{
    using System;
    using System.Linq;
    using Moq;
    using Ploeh.SemanticComparison.Fluent;
    using Xunit;

    public class HaniRssCollectorTest : IdiomaticTest<HaniRssCollector>
    {
        [Test]
        public void SutIsArticleCollector(HaniRssCollector sut)
        {
            Assert.IsAssignableFrom<IArticleCollector>(sut);
        }

        [Test(RunOn.CI)]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames", MessageId = "string", Justification = "To represent dummy value of string.")]
        public void CollectAsyncCollectsArticles(
            HaniRssCollector sut,
            string dummyString,
            DateTime dummyDate)
        {
            // Fixture setup
            var article = new Article(
                id: -1,
                provider: "한겨레",
                guid: dummyString,
                subject: dummyString,
                body: dummyString,
                date: dummyDate,
                url: dummyString,
                userId: sut.Actor);

            var likeness = article.AsSource().OfLikeness<Article>()
                .Without(x => x.Guid)
                .Without(x => x.Subject)
                .Without(x => x.Body)
                .Without(x => x.Date)
                .Without(x => x.Url);

            // Excercise sytem;
            var actual = sut.CollectAsync().Result;

            // Verify outcome
            Assert.True(actual.All(a => likeness.Equals(a)), "likeness");
        }
    }
}