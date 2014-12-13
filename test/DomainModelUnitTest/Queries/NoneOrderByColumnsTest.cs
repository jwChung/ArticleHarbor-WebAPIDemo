namespace ArticleHarbor.DomainModel.Queries
{
    using System.Linq;
    using Xunit;

    public class NoneOrderByColumnsTest : IdiomaticTest<NoneOrderByColumns>
    {
        [Test]
        public void SutIsOrderByColumns(NoneOrderByColumns sut)
        {
            Assert.IsAssignableFrom<IOrderByColumns>(sut);
        }

        [Test]
        public void GetEnumeratorReturnsCorrectResult(NoneOrderByColumns sut)
        {
            Assert.Empty(sut.ToArray());
        }

        [Test]
        public void ExplicitGetEnumeratorReturnsCorrectResult(NoneOrderByColumns sut)
        {
            Assert.Empty(sut);
        }

        [Test]
        public void EqualsDoesNotEqualOtherOfDiffrentType(
            NoneOrderByColumns sut,
            OrderByColumns other)
        {
            var actual = sut.Equals(other);
            Assert.False(actual);
        }

        [Test]
        public void EqualsEqualsOtherOfSameType(
            NoneOrderByColumns sut,
            NoneOrderByColumns other)
        {
            var actual = sut.Equals(other);
            Assert.True(actual);
        }

        [Test]
        public void EqualsDoesNotEqualNull(
            NoneOrderByColumns sut)
        {
            var actual = sut.Equals(null);
            Assert.False(actual);
        }

        [Test]
        public void GetHashCodeReturnsZero(NoneOrderByColumns sut)
        {
            var actual = sut.GetHashCode();
            Assert.Equal(0, actual);
        }
    }
}