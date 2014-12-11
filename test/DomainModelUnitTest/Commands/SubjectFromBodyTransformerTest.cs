namespace ArticleHarbor.DomainModel.Commands
{
    using Models;
    using Ploeh.AutoFixture;
    using Ploeh.SemanticComparison.Fluent;
    using Xunit;
    using Xunit.Extensions;

    public class SubjectFromBodyTransformerTest : IdiomaticTest<SubjectFromBodyTransformer>
    {
        [Test]
        public void SutIsModelTransformer(SubjectFromBodyTransformer sut)
        {
            Assert.IsAssignableFrom<IModelTransformer>(sut);
        }

        [Test]
        [InlineData(5)]
        [InlineData(1)]
        public void TransformAsyncArticleCorrectlyTransformsArticle(
            int length,
            Article article,
            IFixture fixture)
        {
            fixture.Inject(length);
            var sut = fixture.Create<SubjectFromBodyTransformer>();

            var actual = sut.TransformAsync(article).Result;

            article.AsSource().OfLikeness<Article>()
                .With(x => x.Subject)
                .EqualsWhen((a, b) => a.Body.Substring(0, length) == b.Subject)
                .ShouldEqual(actual);
        }

        [Test]
        public void TransformAsyncArticleCorrectlyTransformsArticleWhenLengthIsGreatThanActualLength(
            Article article,
            IFixture fixture)
        {
            int length = 100;
            fixture.Inject(length);
            var sut = fixture.Create<SubjectFromBodyTransformer>();

            var actual = sut.TransformAsync(article).Result;

            article.AsSource().OfLikeness<Article>()
                .With(x => x.Subject)
                .EqualsWhen((a, b) => a.Body == b.Subject)
                .ShouldEqual(actual);
        }

        [Test]
        [InlineData("word\r\nword\nword\rword", "word word word word")]
        [InlineData("word\r\n\nword", "word word")]
        [InlineData("word\r\n\n\rword\r\rword", "word word word")]
        public void TransformAsyncArticleReplacesNewLineWithBlank(
            string body,
            string expected,
            Article article,
            IFixture fixture)
        {
            int length = 100;
            fixture.Inject(length);
            article = article.WithBody(body);
            var sut = fixture.Create<SubjectFromBodyTransformer>();

            var actual = sut.TransformAsync(article).Result;

            Assert.Equal(expected, actual.Subject);
        }
    }
}