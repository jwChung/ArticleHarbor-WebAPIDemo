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
    }
}