namespace ArticleHarbor.DomainModel.Queries
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Ploeh.AutoFixture;
    using Xunit;

    public class EqualPredicateTest : IdiomaticTest<EqualPredicate>
    {
        [Test]
        public void SutIsPredicate(EqualPredicate sut)
        {
            Assert.IsAssignableFrom<IPredicate>(sut);
        }

        [Test]
        public void SutIsOperablePredicate(EqualPredicate sut)
        {
            Assert.IsAssignableFrom<OperablePredicate>(sut);
        }

        [Test]
        public void InitializeWithEmptyNameThrows(IFixture fixture, object value)
        {
            Assert.Throws<ArgumentException>(() => new EqualPredicate(string.Empty, value));
        }

        [Test]
        public void SqlTextIsCorrect(EqualPredicate sut)
        {
            var parameter = sut.Parameters.Single();
            var expected = string.Format("{0} = {1}", sut.ColumnName, parameter.Name);
            var actual = sut.SqlText;
            Assert.Equal(expected, actual);
        }

        [Test]
        public void ParametersIsCorrect(EqualPredicate sut)
        {
            var actual = sut.Parameters.Single();

            Assert.True(actual.Name.StartsWith("@" + sut.ColumnName));
            Assert.DoesNotThrow(() => Guid.Parse(actual.Name.Remove(0, sut.ColumnName.Length + 1)));
            Assert.Equal(sut.Value, actual.Value);
        }

        [Test]
        public void ParametersReturnsAlwaysSameValue(EqualPredicate sut)
        {
            var actual = sut.Parameters;
            Assert.Equal(sut.Parameters, actual);
        }

        [Test]
        public void EqualsEqualsOtherWithSameValues(EqualPredicate sut)
        {
            var other = new EqualPredicate(sut.ColumnName, sut.Value);
            var actual = sut.Equals(other);
            Assert.True(actual);
        }

        [Test]
        public void EqualsDoesNotEqualOtherWithDifferentValues(
            EqualPredicate sut,
            EqualPredicate other)
        {
            var actual = sut.Equals(other);
            Assert.False(actual);
        }

        [Test]
        public void EqualsIsCaseInsensitiveForColumnName(EqualPredicate sut)
        {
            var other = new EqualPredicate(sut.ColumnName.ToUpper(), sut.Value);
            var actual = sut.Equals(other);
            Assert.True(actual);
        }

        [Test]
        public void GetHasCodeReturnsCorrectResult(EqualPredicate sut)
        {
            var other = new EqualPredicate(sut.ColumnName.ToUpper(), sut.Value);
            var actual = sut.GetHashCode();
            Assert.Equal(other.GetHashCode(), actual);
        }

        protected override IEnumerable<MemberInfo> ExceptToVerifyInitialization()
        {
            yield return this.Properties.Select(x => x.SqlText);
            yield return this.Properties.Select(x => x.Parameters);
        }
    }
}