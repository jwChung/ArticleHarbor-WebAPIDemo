namespace ArticleHarbor.DomainModel
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using Jwc.Experiment.Xunit;
    using Ploeh.AutoFixture;
    using Ploeh.AutoFixture.Kernel;
    using Ploeh.SemanticComparison.Fluent;
    using Xunit;

    public class ArticleTest : IdiomaticTest<Article>
    {
        [Test]
        public IEnumerable<ITestCase> InitializeWithAnyEmptyStringThrows(int id, string value, DateTime date)
        {
            yield return TestCase.Create(() => Assert.Throws<ArgumentException>(
                () => new Article(string.Empty, value, value, value, date, value)));
            yield return TestCase.Create(() => Assert.Throws<ArgumentException>(
                () => new Article(value, string.Empty, value, value, date, value)));
            yield return TestCase.Create(() => Assert.Throws<ArgumentException>(
                () => new Article(value, value, string.Empty, value, date, value)));
            yield return TestCase.Create(() => Assert.Throws<ArgumentException>(
                () => new Article(value, value, value, string.Empty, date, value)));
            yield return TestCase.Create(() => Assert.Throws<ArgumentException>(
                () => new Article(value, value, value, value, date, string.Empty)));

            yield return TestCase.Create(() => Assert.Throws<ArgumentException>(
                () => new Article(id, string.Empty, value, value, value, date, value)));
            yield return TestCase.Create(() => Assert.Throws<ArgumentException>(
                () => new Article(id, value, string.Empty, value, value, date, value)));
            yield return TestCase.Create(() => Assert.Throws<ArgumentException>(
                () => new Article(id, value, value, string.Empty, value, date, value)));
            yield return TestCase.Create(() => Assert.Throws<ArgumentException>(
                () => new Article(id, value, value, value, string.Empty, date, value)));
            yield return TestCase.Create(() => Assert.Throws<ArgumentException>(
                () => new Article(id, value, value, value, value, date, string.Empty)));
        }

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
        public void WithUserIdReturnsCorrectArticle(Article sut, string userId)
        {
            var likeness = sut.AsSource().OfLikeness<Article>().Without(x => x.UserId);

            var actual = sut.WithUserId(userId);

            Assert.NotSame(sut, actual);
            likeness.ShouldEqual(actual);
            Assert.Equal(userId, actual.UserId);
        }

        [Test]
        public void WithIdReturnsCorrectArticle(Article sut, int id, string userId)
        {
            sut = sut.WithUserId(userId);
            var likeness = sut.AsSource().OfLikeness<Article>().Without(x => x.Id);

            var actual = sut.WithId(id);

            Assert.NotSame(sut, actual);
            likeness.ShouldEqual(actual);
            Assert.Equal(id, actual.Id);
        }

        [Test]
        public void WithSubjectReturnsCorrectArticle(Article sut, string newSubject, string userId)
        {
            sut = sut.WithUserId(userId);
            var likeness = sut.AsSource().OfLikeness<Article>().Without(x => x.Subject);

            var actual = sut.WithSubject(newSubject);

            Assert.NotSame(sut, actual);
            likeness.ShouldEqual(actual);
            Assert.Equal(newSubject, actual.Subject);
        }

        protected override IEnumerable<MemberInfo> ExceptToVerifyInitialization()
        {
            yield return this.Properties.Select(x => x.UserId);
        }
    }
}