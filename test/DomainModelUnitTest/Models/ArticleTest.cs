namespace ArticleHarbor.DomainModel.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Jwc.Experiment.Xunit;
    using Ploeh.AutoFixture;
    using Ploeh.AutoFixture.Kernel;
    using Ploeh.SemanticComparison.Fluent;
    using Xunit;

    public class ArticleTest : IdiomaticTest<Article>
    {
        [Test]
        public void SutIsModel(Article sut)
        {
            Assert.IsAssignableFrom<IModel>(sut);
        }

        [Test]
        public IEnumerable<ITestCase> InitializeWithAnyEmptyStringThrows(int id, string value, DateTime date)
        {
            yield return TestCase.Create(() => Assert.Throws<ArgumentException>(
                () => new Article(id, string.Empty, value, value, value, date, value, value)));
            yield return TestCase.Create(() => Assert.Throws<ArgumentException>(
                () => new Article(id, value, string.Empty, value, value, date, value, value)));
            yield return TestCase.Create(() => Assert.Throws<ArgumentException>(
                () => new Article(id, value, value, string.Empty, value, date, value, value)));
            yield return TestCase.Create(() => Assert.Throws<ArgumentException>(
                () => new Article(id, value, value, value, string.Empty, date, value, value)));
            yield return TestCase.Create(() => Assert.Throws<ArgumentException>(
                () => new Article(id, value, value, value, value, date, string.Empty, value)));
            yield return TestCase.Create(() => Assert.Throws<ArgumentException>(
                () => new Article(id, value, value, value, value, date, value, string.Empty)));
        }

        [Test]
        public IEnumerable<ITestCase> UserIdIsCorrect()
        {
            yield return TestCase.WithAuto<Generator<Article>>().Create(
                factory =>
                {
                    Assert.NotNull(factory.First().UserId);
                    var actual = factory.Take(100).Select(x => x.UserId).Distinct().Count();
                    Assert.Equal(100, actual);
                });

            yield return TestCase.WithAuto<IFixture>().Create(
                fixture =>
                {
                    var userId = fixture.Freeze<string>();
                    var sut = fixture.Build<Article>()
                        .FromFactory(
                            new MethodInvoker(new GreedyConstructorQuery()))
                        .Create();
                    Assert.Equal(userId, sut.UserId);
                });
        }

        [Test]
        public IEnumerable<ITestCase> IdIsCorrect()
        {
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
        public void WithUserIdWithEmptyStringThrows(
            Article sut)
        {
            Assert.Throws<ArgumentException>(() => sut.WithUserId(string.Empty));
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

        [Test]
        public void WithBodyReturnsCorrectArticle(Article sut, string newBody)
        {
            var likeness = sut.AsSource().OfLikeness<Article>().Without(x => x.Body);

            var actual = sut.WithBody(newBody);

            Assert.NotSame(sut, actual);
            likeness.ShouldEqual(actual);
            Assert.Equal(newBody, actual.Body);
        }

        [Test]
        public void ExecuteCommandReturnsCorrectResult(
            Article sut,
            IModelCommand<object> command,
            IModelCommand<object> expected)
        {
            command.Of(x => x.Execute(sut) == expected);
            var actual = sut.ExecuteCommand(command);
            Assert.Equal(expected, actual);
        }

        [Test]
        public void GetKeysReturnsCorrectKeys(Article sut)
        {
            var expected = new Keys<int>(sut.Id);
            var actual = sut.GetKeys();
            Assert.Equal(expected, actual);
        }

        protected override IEnumerable<MemberInfo> ExceptToVerifyInitialization()
        {
            yield return this.Properties.Select(x => x.UserId);
        }
    }
}