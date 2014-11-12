namespace ArticleHarbor.DomainModel.Collectors
{
    using System.Collections.Generic;
    using System.Linq;
    using Models;
    using Ploeh.AutoFixture;
    using Ploeh.AutoFixture.Xunit;
    using Ploeh.SemanticComparison.Fluent;
    using Xunit;
    using Xunit.Extensions;

    public class SubjectFromBodyConvertorTest : IdiomaticTest<SubjectFromBodyConvertor>
    {
        [Test]
        public void SutIsArticleConverter(SubjectFromBodyConvertor sut)
        {
            Assert.IsAssignableFrom<IArticleConvertor>(sut);
        }

        [Test]
        [InlineData(3)]
        [InlineData(6)]
        [InlineData]
        public void ConvertReplacesSubjectWithBodyContent(
            int subjectLength,
            IEnumerable<Article> articles,
            IFixture fixture)
        {
            fixture.Inject(subjectLength);
            var sut = fixture.Create<SubjectFromBodyConvertor>();
            
            var actual = sut.Convert(articles);

            Assert.True(actual.All(a => a.Subject.Length <= subjectLength));
            var articleArray = articles.ToArray();
            var actualArray = actual.ToArray();
            for (int i = 0; i < articleArray.Length; i++)
                articleArray[i].AsSource().OfLikeness<Article>()
                    .Without(x => x.Subject).ShouldEqual(actualArray[i]);
        }
    }
}