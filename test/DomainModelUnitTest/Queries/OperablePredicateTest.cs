namespace ArticleHarbor.DomainModel.Queries
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Ploeh.AutoFixture;
    using Xunit;

    public class OperablePredicateTest : IdiomaticTest<OperablePredicate>
    {
        [Test]
        public void SutIsPredicate(OperablePredicate sut)
        {
            Assert.IsAssignableFrom<IPredicate>(sut);
        }

        [Test]
        public void InitializeWithEmptyColumnNameThrows(string operatorName, object value)
        {
            Assert.Throws<ArgumentException>(() => new OperablePredicate(string.Empty, operatorName, value));
        }
        
        [Test]
        public void InitializeWithEmptyOperatorNameThrows(string columnName, object value)
        {
            Assert.Throws<ArgumentException>(() => new OperablePredicate(columnName, string.Empty, value));
        }

        [Test]
        public void ParametersIsCorrect(OperablePredicate sut)
        {
            var actual = sut.Parameters.Single();

            Assert.True(actual.Name.StartsWith("@" + sut.ColumnName));
            Assert.DoesNotThrow(() => Guid.Parse(actual.Name.Remove(0, sut.ColumnName.Length + 1)));
            Assert.Equal(sut.Value, actual.Value);
        }

        [Test]
        public void ParametersReturnsAlwaysSameInstance(OperablePredicate sut)
        {
            var actual = sut.Parameters;
            Assert.Same(sut.Parameters, actual);
        }

        [Test]
        public void SqlTextIsCorrect(OperablePredicate sut)
        {
            var parameter = sut.Parameters.Single();
            var expected = string.Format("{0} {1} {2}", sut.ColumnName, sut.OperatorName,  parameter.Name);
            var actual = sut.SqlText;
            Assert.Equal(expected, actual);
        }

        [Test]
        public void EqualsDoesNotEqualOtherWithDifferentValues(
            OperablePredicate sut,
            OperablePredicate other)
        {
            var actual = sut.Equals(other);
            Assert.False(actual);
        }

        [Test]
        public void EqualsEqualsOtherWithSameValues(OperablePredicate sut)
        {
            var other = new OperablePredicate(sut.ColumnName, sut.OperatorName, sut.Value);
            var actual = sut.Equals(other);
            Assert.True(actual);
        }

        [Test]
        public void EqualsIgnoresCaseOfColumnName(OperablePredicate sut)
        {
            var other = new OperablePredicate(
                sut.ColumnName.ToUpper(), sut.OperatorName, sut.Value);
            var actual = sut.Equals(other);
            Assert.True(actual);
        }

        [Test]
        public void EqualsIgnoresCaseOfOperatorName(OperablePredicate sut)
        {
            var other = new OperablePredicate(
                sut.ColumnName, sut.OperatorName.ToUpper(), sut.Value);
            var actual = sut.Equals(other);
            Assert.True(actual);
        }

        [Test]
        public void GetHasCodeReturnsCorrectResult(OperablePredicate sut)
        {
            var other = new OperablePredicate(
                sut.ColumnName.ToUpper(), sut.OperatorName.ToUpper(), sut.Value);
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