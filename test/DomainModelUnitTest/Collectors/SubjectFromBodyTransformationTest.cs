namespace ArticleHarbor.DomainModel.Collectors
{
    using Models;
    using Ploeh.AutoFixture;
    using Ploeh.AutoFixture.Xunit;
    using Ploeh.SemanticComparison.Fluent;
    using Xunit;
    using Xunit.Extensions;

    public class SubjectFromBodyTransformationTest : IdiomaticTest<SubjectFromBodyTransformation>
    {
        [Test]
        public void SutIsArticleConverter(SubjectFromBodyTransformation sut)
        {
            Assert.IsAssignableFrom<IArticleTransformation>(sut);
        }

        [Test]
        [InlineData(3)]
        [InlineData(6)]
        [InlineData]
        public void TransformReplacesSubjectWithBodyContent(
            int subjectLength,
            Article article,
            IFixture fixture)
        {
            fixture.Inject(subjectLength);
            var sut = fixture.Create<SubjectFromBodyTransformation>();

            var actual = sut.Transform(article);

            Assert.True(actual.Subject.Length <= subjectLength);
            actual.AsSource().OfLikeness<Article>().Without(x => x.Subject).ShouldEqual(article);
        }
    }
}