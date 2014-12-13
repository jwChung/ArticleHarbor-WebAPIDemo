namespace ArticleHarbor.DomainModel.Queries
{
    using System.Collections.Generic;
    using System.Reflection;
    using Xunit;

    public class NoTopTest : IdiomaticTest<NoTop>
    {
        [Test]
        public void SutIsTop(NoTop sut)
        {
            Assert.IsAssignableFrom<ITop>(sut);
        }

        [Test]
        public void CountIsCorrect(NoTop sut)
        {
            var actual = sut.Count;
            Assert.Equal(0, actual);
        }

        [Test]
        public void EqualsDoesNotEqualOtherOfDiffrentType(
            NoTop sut,
            Top other)
        {
            var actual = sut.Equals(other);
            Assert.False(actual);
        }
        
        [Test]
        public void EqualsEqualsOtherOfSameType(
            NoTop sut,
            NoTop other)
        {
            var actual = sut.Equals(other);
            Assert.True(actual);
        }

        [Test]
        public void EqualsDoesNotEqualNull(
            NoTop sut)
        {
            var actual = sut.Equals(null);
            Assert.False(actual);
        }

        [Test]
        public void GetHashCodeReturnsZero(NoOrderByColumns sut)
        {
            var actual = sut.GetHashCode();
            Assert.Equal(0, actual);
        }

        protected override IEnumerable<MemberInfo> ExceptToVerifyInitialization()
        {
            yield return this.Properties.Select(x => x.Count);
        }
    }
}