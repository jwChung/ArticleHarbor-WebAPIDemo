namespace ArticleHarbor.DomainModel.Queries
{
    using Xunit;

    public class PredicateTest : IdiomaticTest<Predicate>
    {
        [Test]
        public void EqualReturnsCorrectResult(string columnName, object value)
        {
            var expected = new OperablePredicate(columnName, "=", value);
            var actual = Predicate.Equal(columnName, value);
            Assert.Equal(expected, actual);
        }
    }
}