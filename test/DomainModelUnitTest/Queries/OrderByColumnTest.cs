namespace ArticleHarbor.DomainModel.Queries
{
    using Xunit;

    public class OrderByColumnTest : IdiomaticTest<OrderByColumn>
    {
        [Test]
        public void SutIsOrderByColumn(OrderByColumn sut)
        {
            Assert.IsAssignableFrom<IOrderByColumn>(sut);
        }

        [Test]
        public void EqualsEqualsWithOrderByColumnWithSameValues(OrderByColumn sut)
        {
            var other = new OrderByColumn(sut.Name, sut.OrderDirection);
            var actual = sut.Equals(other);
            Assert.True(actual);
        }

        [Test]
        public void EqualsDoesNotEqualWithOrderByColumnWithOtherValues(
            OrderByColumn sut,
            OrderByColumn other)
        {
            var actual = sut.Equals(other);
            Assert.False(actual);
        }

        [Test]
        public void EqualsIgnoresNameCase(OrderByColumn sut)
        {
            var other = new OrderByColumn(sut.Name.ToUpper(), sut.OrderDirection);
            var actual = sut.Equals(other);
            Assert.True(actual);
        }
    }
}