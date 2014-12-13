namespace ArticleHarbor.DomainModel.Queries
{
    using System.Collections.Generic;
    using System.Reflection;
    using Xunit;

    public class NoPredicateTest : IdiomaticTest<NoPredicate>
    {
        [Test]
        public void SutIsPredicate(NoPredicate sut)
        {
            Assert.IsAssignableFrom<IPredicate>(sut);
        }
        
        [Test]
        public void SqlTextIsCorrect(NoPredicate sut)
        {
            Assert.Empty(sut.SqlText);
        }

        [Test]
        public void ParametersIsCorrect(NoPredicate sut)
        {
            Assert.Empty(sut.Parameters);
        }
        
        [Test]
        public void EqualsDoesNotEqualOtherOfDifferentType(
            NoPredicate sut,
            AndPredicate other)
        {
            var actual = sut.Equals(other);
            Assert.False(actual);
        }

        [Test]
        public void EqualsEqualsOtherOfSameType(
            NoPredicate sut,
            NoPredicate other)
        {
            var actual = sut.Equals(other);
            Assert.True(actual);
        }

        [Test]
        public void EqualsDoesNotEqualNull(
            NoPredicate sut)
        {
            var actual = sut.Equals(null);
            Assert.False(actual);
        }

        [Test]
        public void GetHashCodeReturnsZero(NoPredicate sut)
        {
            var actual = sut.GetHashCode();
            Assert.Equal(0, actual);
        }

        protected override IEnumerable<MemberInfo> ExceptToVerifyInitialization()
        {
            yield return this.Properties.Select(x => x.SqlText);
            yield return this.Properties.Select(x => x.Parameters);
        }
    }
}