namespace DomainModelUnitTest
{
    using System.Collections.Generic;
    using System.Reflection;
    using DomainModel;
    using Ploeh.SemanticComparison.Fluent;
    using Xunit;

    public class ArticleTest : IdiomaticTest<Article>
    {
        [Test]
        public void IdReturnsDefaultValue(Article sut)
        {
            Assert.Equal(-1, sut.Id);
        }

        [Test]
        public void WithIdReturnsCorrectArticle(Article sut, int id)
        {
            var likeness = sut.AsSource().OfLikeness<Article>().Without(x => x.Id);

            var actual = sut.WithId(id);

            Assert.NotSame(sut, actual);
            likeness.ShouldEqual(actual);
            Assert.Equal(id, actual.Id);
        }

        protected override IEnumerable<MemberInfo> ExceptToVerifyInitialization()
        {
            yield return this.Properties.Select(x => x.Id);
        }
    }
}