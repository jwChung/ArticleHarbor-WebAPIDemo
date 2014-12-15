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
                properties.Select(x => x.Before),
                properties.Select(x => x.Duration),
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
        public void BeforeIsCorrect(ArticleQueryViewModel sut)
        {
            var actual = sut.Before;
            var gap = DateTime.Now - actual;
            Assert.Equal(gap.Ticks, 0);
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
        public void ProvideQueryWithNullPreviousIdReturnsResultNotHavingPreviousIdPredicate(
            ArticleQueryViewModel sut)
        {
            sut.PreviousId = null;
            var actual = sut.ProvideQuery();
            Assert.Equal(new NoPredicate(), actual.Predicate);
        }

        [Test]
        public void ProvideQueryReturnsResultHavingCorrectSubjectPredicate(
            ArticleQueryViewModel sut,
            string subject)
        {
            sut.Subject = subject;

            var actual = sut.ProvideQuery();

            var andPredicate = Assert.IsAssignableFrom<AndPredicate>(actual.Predicate);
            Assert.Contains(Predicate.Contains("Subject", subject), andPredicate.Predicates);
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
            Assert.Equal(new NoPredicate(), actual.Predicate);
        }

        [Test]
        public void ProvideQueryReturnsResultHavingCorrectBodyPredicate(
            ArticleQueryViewModel sut,
            string body)
        {
            sut.Body = body;

            var actual = sut.ProvideQuery();

            var andPredicate = Assert.IsAssignableFrom<AndPredicate>(actual.Predicate);
            Assert.Contains(Predicate.Contains("Body", body), andPredicate.Predicates);
        }

        [Test]
        [InlineData(null)]
        [InlineData("")]
        public void ProvideQueryWithNullOrEmptySubjectReturnsResultNotHavingBodyPredicate(
            string body,
            ArticleQueryViewModel sut)
        {
            sut.Body = body;
            var actual = sut.ProvideQuery();
            Assert.Equal(new NoPredicate(), actual.Predicate);
        }

        [Test]
        public void ProvideQueryWithEmptyPredicateReturnsResultHavingNoPredicate(
            ArticleQueryViewModel sut)
        {
            var actual = sut.ProvideQuery();
            Assert.Equal(new NoPredicate(), actual.Predicate);
        }

        [Test]
        public void ProvideQueryWithNullDurationReturnsResultNotHavingDatePredicate(
            string subject,
            ArticleQueryViewModel sut)
        {
            sut.Duration = null;
            var actual = sut.ProvideQuery();
            Assert.Equal(new NoPredicate(), actual.Predicate);
        }
        
        [Test]
        public void ProvideQueryWithDurationReturnsResultHavingCorretDatePredicate(
            string subject,
            ArticleQueryViewModel sut,
            DateTime now,
            TimeSpan duration)
        {
            sut.Before = now;
            sut.Duration = duration;

            var actual = sut.ProvideQuery();

            var andPredicate = Assert.IsAssignableFrom<AndPredicate>(actual.Predicate);
            Assert.Contains(Predicate.GreatOrEqualThan("Date", now - duration), andPredicate.Predicates);
            Assert.Contains(Predicate.LessOrEqualThan("Date", now), andPredicate.Predicates);
        }
        
        protected override IEnumerable<MemberInfo> ExceptToVerifyInitialization()
        {
            yield return this.Properties.Select(x => x.PreviousId);
            yield return this.Properties.Select(x => x.Count);
            yield return this.Properties.Select(x => x.Subject);
            yield return this.Properties.Select(x => x.Body);
            yield return this.Properties.Select(x => x.Before);
            yield return this.Properties.Select(x => x.Duration);
        }

        protected override IEnumerable<MemberInfo> ExceptToVerifyGuardClause()
        {
            yield return this.Properties.Select(x => x.Subject);
            yield return this.Properties.Select(x => x.Body);
        }
    }
}