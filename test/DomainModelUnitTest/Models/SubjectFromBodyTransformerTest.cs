namespace ArticleHarbor.DomainModel.Models
{
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
                .EqualsWhen((a, b) => a.Subject.Substring(0, length) == b.Subject)
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
                .ShouldEqual(actual);
        }
    }
}