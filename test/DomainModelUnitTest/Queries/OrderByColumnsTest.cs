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
    }
}