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
    }
}