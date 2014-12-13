namespace ArticleHarbor.DomainModel.Queries
{
    using System.Linq;
    using Xunit;

    public class NoOrderByColumnsTest : IdiomaticTest<NoOrderByColumns>
    {
        [Test]
        public void SutIsOrderByColumns(NoOrderByColumns sut)
        {
            Assert.IsAssignableFrom<IOrderByColumns>(sut);
        }

        [Test]
        public void GetEnumeratorReturnsCorrectResult(NoOrderByColumns sut)
        {
            Assert.Empty(sut.ToArray());
        }

        [Test]
        public void ExplicitGetEnumeratorReturnsCorrectResult(NoOrderByColumns sut)
        {
            Assert.Empty(sut);
        }

        [Test]
        public void EqualsDoesNotEqualOtherOfDiffrentType(
            NoOrderByColumns sut,
            OrderByColumns other)
        {
            var actual = sut.Equals(other);
            Assert.False(actual);
        }

        [Test]
        public void EqualsEqualsOtherOfSameType(
            NoOrderByColumns sut,
            NoOrderByColumns other)
        {
            var actual = sut.Equals(other);
            Assert.True(actual);
        }

        [Test]
        public void EqualsDoesNotEqualNull(
            NoOrderByColumns sut)
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
    }
}