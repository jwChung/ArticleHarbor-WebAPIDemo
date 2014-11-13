namespace ArticleHarbor.DomainModel.Collectors
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
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
        [Obsolete]
        public void ObsoleteTransformReplacesSubjectWithBodyContent(
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

        [Test]
        [InlineData(3)]
        [InlineData(6)]
        [InlineData]
        public void TransformReplacesSubjectWithBodyContent(
            int subjectLength,
            IList<Article> articles,
            IFixture fixture)
        {
            fixture.Inject(subjectLength);
            var sut = fixture.Create<SubjectFromBodyTransformation>();

            var actual = sut.Transform(articles).ToArray();

            Assert.Equal(articles.Count, actual.Length);
            for (int i = 0; i < articles.Count; i++)
            {
                Assert.True(actual[i].Subject.Length <= subjectLength);
                actual[i].AsSource().OfLikeness<Article>().Without(x => x.Subject)
                    .ShouldEqual(articles[i]);    
            }
        }
    }
}