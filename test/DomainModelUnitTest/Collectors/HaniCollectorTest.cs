namespace ArticleHarbor.DomainModel.Collectors
{
    using System;
    using Models;
    using Moq;
    using Ploeh.SemanticComparison.Fluent;

    public class HaniCollectorTest : IdiomaticTest<HaniCollector>
    {
        [Test]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames", MessageId = "string", Justification = "To represent dummy value of string.")]
        public void CollectAsyncCollectsArticles(
            HaniCollector sut,
            string dummyString,
            DateTime dummyDate)
        {
            // Fixture setup
            var article = new Article(
                id: -1,
                provider: "한겨레",
                no: dummyString, 
                subject: dummyString,
                body: dummyString, 
                date: dummyDate, 
                url: dummyString,
                userId: sut.Actor);

            var likeness = article.AsSource().OfLikeness<Article>()
                .Without(x => x.No)
                .Without(x => x.Subject)
                .Without(x => x.Body)
                .Without(x => x.Date)
                .Without(x => x.Url);

            // Excercise sytem;
            sut.CollectAsync().Wait();

            // Verify outcome
            sut.ArticleService.ToMock().Verify(
                x => x.AddAsync(It.Is<Article>(a => likeness.Equals(a))),
                Times.Exactly(25));
        }
    }
}