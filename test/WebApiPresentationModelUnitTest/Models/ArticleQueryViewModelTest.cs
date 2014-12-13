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
    using Xunit.Extensions;

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
                properties.Select(x => x.Body),
            };

            return TestCases.WithArgs(testData).WithAuto<ReadWritablePropertyAssertion>()
                .Create((p, a) => a.Verify(p));
        }

        [Test]
        public void CountIsCorrect(
            ArticleQueryViewModel sut)
        {
            var actual = sut.Count;
            Assert.Equal(ArticleQueryViewModel.MaxCount, actual);
        }

        [Test]
        public void CountThrowsWhenValueIsGreatThanMaxCount(
            ArticleQueryViewModel sut)
        {
            Assert.Throws<ArgumentException>(
                () => sut.Count = ArticleQueryViewModel.MaxCount + 1);
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
        public void ProvideQueryReturnsResultHavingCorrectPreviousIdPredicate(
            ArticleQueryViewModel sut,
            int previousId)
        {
            sut.PreviousId = previousId;

            var actual = sut.ProvideQuery();

            var andPredicate = Assert.IsAssignableFrom<AndPredicate>(actual.Predicate);
            Assert.Contains(Predicate.GreatThan("Id", previousId), andPredicate.Predicates);
        }

        [Test]
        public void ProvideQueryWithNullPreviousEndIdReturnsResultNotHavingPreviousIdPredicate(
            ArticleQueryViewModel sut)
        {
            var actual = sut.ProvideQuery();

            var andPredicate = Assert.IsAssignableFrom<AndPredicate>(actual.Predicate);
            Assert.False(andPredicate.Predicates.OfType<OperablePredicate>().Any(
                x => x.ColumnName.Equals("Id", StringComparison.CurrentCultureIgnoreCase)));
        }

        [Test]
        public void ProvideQueryReturnsResultHavingCorrectSubjectPredicate(
            ArticleQueryViewModel sut,
            string subject)
        {
            sut.Subject = subject;

            var actual = sut.ProvideQuery();

            var andPredicate = Assert.IsAssignableFrom<AndPredicate>(actual.Predicate);
            Assert.Contains(Predicate.Like("Subject", "%" + subject + "%"), andPredicate.Predicates);
        }

        [Test]
        [InlineData(null)]
        [InlineData("")]
        public void ProvideQueryWithNullOrEmptySubjectReturnsResultNotHavingSubjectPredicate(
            string subject,
            ArticleQueryViewModel sut)
        {
            sut.Subject = subject;

            var actual = sut.ProvideQuery();

            var andPredicate = Assert.IsAssignableFrom<AndPredicate>(actual.Predicate);
            Assert.False(andPredicate.Predicates.OfType<OperablePredicate>().Any(
                x => x.ColumnName.Equals("Subject", StringComparison.CurrentCultureIgnoreCase)));
        }

        protected override IEnumerable<MemberInfo> ExceptToVerifyInitialization()
        {
            yield return this.Properties.Select(x => x.PreviousId);
            yield return this.Properties.Select(x => x.Count);
            yield return this.Properties.Select(x => x.Subject);
            yield return this.Properties.Select(x => x.Body);
        }

        protected override IEnumerable<MemberInfo> ExceptToVerifyGuardClause()
        {
            yield return this.Properties.Select(x => x.Subject);
            yield return this.Properties.Select(x => x.Body);
        }
    }
}