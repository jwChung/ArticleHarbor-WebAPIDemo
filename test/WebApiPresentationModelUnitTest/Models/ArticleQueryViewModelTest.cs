namespace ArticleHarbor.WebApiPresentationModel.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using DomainModel.Queries;
    using Jwc.Experiment.Xunit;
    using Ploeh.Albedo;
    using Ploeh.AutoFixture;
    using Ploeh.AutoFixture.Xunit;
    using Xunit;

    public class ArticleQueryViewModelTest : IdiomaticTest<ArticleQueryViewModel>
    {
        [Test]
        public void SutIsSqlQueryable(ArticleQueryViewModel sut)
        {
            Assert.IsAssignableFrom<ISqlQueryable>(sut);
        }

        [Test]
        public IEnumerable<ITestCase> PropertiesAreReadWritable()
        {
            var properties = new Properties<ArticleQueryViewModel>();
            var testData = new[]
            {
                properties.Select(x => x.PreviousId),
                properties.Select(x => x.Subject),
            };

            return TestCases.WithArgs(testData).WithAuto<ReadWritablePropertyAssertion>()
                .Create((p, a) => a.Verify(p));
        }

        [Test]
        public void CountIsCorrect(
            ArticleQueryViewModel sut)
        {
            var actual = sut.Count;
            Assert.Equal(50, actual);
        }

        [Test]
        public void CountThrowsWhenValueIsGreatThan50(
            ArticleQueryViewModel sut)
        {
            Assert.Throws<ArgumentException>(() => sut.Count = 51);
        }

        [Test]
        public void ProvideQueryReturnsResultHavingCorrectTop(
            ArticleQueryViewModel sut,
            Generator<int> generator)
        {
            int count = generator.First(x => x <= 50);
            sut.Count = count;

            var actual = sut.ProvideQuery();

            Assert.Equal(new Top(count), actual.Top);
        }

        [Test]
        public void ProvideQueryReturnsResultHavingCorrectPreviousEndIdPredicate(
            ArticleQueryViewModel sut,
            int previousEndId)
        {
            sut.PreviousId = previousEndId;

            var actual = sut.ProvideQuery();

            var andPredicate = Assert.IsAssignableFrom<AndPredicate>(actual.Predicate);
            Assert.Contains(Predicate.GreatThan("Id", previousEndId), andPredicate.Predicates);
        }

        [Test]
        public void ProvideQueryWithNullPreviousEndIdReturnsResultNotHavingPreviousEndIdPredicate(
            ArticleQueryViewModel sut)
        {
            var actual = sut.ProvideQuery();

            var andPredicate = Assert.IsAssignableFrom<AndPredicate>(actual.Predicate);
            Assert.False(andPredicate.Predicates.OfType<OperablePredicate>().Any(
                x => x.OperatorName.Equals("Id", StringComparison.CurrentCultureIgnoreCase)));
        }

        protected override IEnumerable<MemberInfo> ExceptToVerifyInitialization()
        {
            yield return this.Properties.Select(x => x.PreviousId);
            yield return this.Properties.Select(x => x.Count);
            yield return this.Properties.Select(x => x.Subject);
        }

        protected override IEnumerable<MemberInfo> ExceptToVerifyGuardClause()
        {
            yield return this.Properties.Select(x => x.Subject);
        }
    }
}