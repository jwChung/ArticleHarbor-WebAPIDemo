namespace ArticleHarbor.DomainModel.Queries
{
    using Xunit;

    public class OrderByColumnsTest : IdiomaticTest<OrderByColumns>
    {
        [Test]
        public void SutIsOrderByColumns(OrderByColumns sut)
        {
            Assert.IsAssignableFrom<IOrderByColumns>(sut);
        }
    }
}