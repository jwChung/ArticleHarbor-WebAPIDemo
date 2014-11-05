namespace DomainModelUnitTest
{
    using System.Collections.Generic;
    using System.Reflection;
    using DomainModel;
    using Jwc.Experiment.Xunit;
    using Ploeh.AutoFixture;
    using Ploeh.AutoFixture.Kernel;
    using Ploeh.SemanticComparison.Fluent;
    using Xunit;

    public class ArticleTest : IdiomaticTest<Article>
    {
        [Test]
        public IEnumerable<ITestCase> IdIsCorrect()
        {
            yield return TestCase.WithAuto<Article>().Create(
                sut => Assert.Equal(-1, sut.Id));

            yield return TestCase.WithAuto<IFixture>().Create(
                fixture =>
                {
                    var id = fixture.Freeze<int>();
                    var sut = fixture.Build<Article>()
                        .FromFactory(
                            new MethodInvoker(new GreedyConstructorQuery()))
                        .Create();
                    Assert.Equal(id, sut.Id);
                });
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