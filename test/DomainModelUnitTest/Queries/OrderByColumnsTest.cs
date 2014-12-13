namespace ArticleHarbor.DomainModel.Queries
{
    using System.Linq;
    using Xunit;

    public class OrderByColumnsTest : IdiomaticTest<OrderByColumns>
    {
        [Test]
        public void SutIsOrderByColumns(OrderByColumns sut)
        {
            Assert.IsAssignableFrom<IOrderByColumns>(sut);
        }

        [Test]
        public void NoneReturnsEmptyColumns()
        {
            var actual = OrderByColumns.None;
            Assert.Empty(actual);
        }

        [Test]
        public void NoneIsCorrect()
        {
            var actual = OrderByColumns.None;
            Assert.IsType<NoOrderByColumns>(actual);
        }
        
        [Test]
        public void GetEnumeratorReturnsCorrectResult(OrderByColumns sut)
        {
            var actual = sut.ToArray();
            Assert.Equal(sut.Columns, actual);
        }

        [Test]
        public void ExplicitGetEnumeratorReturnsCorrectResult(OrderByColumns sut)
        {
            Assert.Equal(sut.Columns, sut);
        }

        [Test]
        public void EqualsDoesNotEqualOtherWithDifferentValues(
            OrderByColumns sut,
            OrderByColumns other)
        {
            var actual = sut.Equals(other);
            Assert.False(actual);
        }

        [Test]
        public void EqualsEqualsOtherWithSameValues(OrderByColumns sut)
        {
            var other = new OrderByColumns(sut.Columns.ToArray());
            var actual = sut.Equals(other);
            Assert.True(actual);
        }

        [Test]
        public void GetHasCodeReturnsCorrectResult(OrderByColumns sut)
        {
            var other = new OrderByColumns(sut.Columns.ToArray());
            var actual = sut.GetHashCode();
            Assert.Equal(other.GetHashCode(), actual);
        }
    }
}