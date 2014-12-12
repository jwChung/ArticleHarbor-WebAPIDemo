namespace ArticleHarbor.DomainModel.Collectors
{
    using System;
    using System.Linq;
    using Models;
    using Ploeh.SemanticComparison.Fluent;
    using Xunit;

    public class FacebookRssCollectorTest : IdiomaticTest<FacebookRssCollector>
    {
        [Test]
        public void SutIsArticleCollector(FacebookRssCollector sut)
        {
            Assert.IsAssignableFrom<IArticleCollector>(sut);
        }

        [Test(RunOn.CI)]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames", MessageId = "string", Justification = "To represent dummy value of string.")]
        public void CollectAsyncCollectsArticles(
            string author,
            string facebookName,
            string dummyString,
            DateTime dummyDate)
        {
            // Fixture setup
            var sut = new FacebookRssCollector(
                author, "177323639028540", facebookName); // ASP.NET Korea group

            var article = new Article(
                id: -1,
                provider: facebookName,
                guid: dummyString,
                subject: dummyString,
                body: dummyString,
                date: dummyDate,
                url: dummyString,
                userId: author);

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