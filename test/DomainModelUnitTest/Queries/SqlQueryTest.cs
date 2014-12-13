namespace ArticleHarbor.DomainModel.Queries
{
    using Xunit;

    public class SqlQueryTest : IdiomaticTest<SqlQuery>
    {
        [Test]
        public void SutIsSqlQuery(SqlQuery sut)
        {
            Assert.IsAssignableFrom<ISqlQuery>(sut);
        }

        [Test]
        public void EqualsDoesNotEqualOtherWithDifferentValues(SqlQuery sut, SqlQuery other)
        {
            var actual = sut.Equals(other);
            Assert.False(actual);
        }

        [Test]
        public void EqualsEqualsOtherWithSameValues(SqlQuery sut)
        {
            var other = new SqlQuery(sut.Top, sut.OrderByColumns, sut.Predicate);
            var actual = sut.Equals(other);
            Assert.True(actual);
        }
    }
}