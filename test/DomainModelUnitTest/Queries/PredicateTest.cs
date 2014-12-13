namespace ArticleHarbor.DomainModel.Queries
{
    using System.Linq;
    using Xunit;

    public class PredicateTest
    {
        [Test]
        public void NoneReturnsCorrectResult()
        {
            Assert.IsType<NoPredicate>(Predicate.None);
        }

        [Test]
        public void AndReturnsCorrectResult(AndPredicate expected)
        {
            var actual = Predicate.And(expected.Predicates.ToArray());
            Assert.Equal(expected, actual);
        }

        [Test]
        public void InClauseReturnsCorrectResult(InClausePredicate expected)
        {
            var actual = Predicate.InClause(expected.ColumnName, expected.Values.ToArray());
            Assert.Equal(expected, actual);
        }

        [Test]
        public void EqualReturnsCorrectResult(string columnName, object value)
        {
            var expected = new OperablePredicate(columnName, "=", value);
            var actual = Predicate.Equal(columnName, value);
            Assert.Equal(expected, actual);
        }

        [Test]
        public void NotEqualReturnsCorrectResult(string columnName, object value)
        {
            var expected = new OperablePredicate(columnName, "<>", value);
            var actual = Predicate.NotEqual(columnName, value);
            Assert.Equal(expected, actual);
        }

        [Test]
        public void LikeReturnsCorrectResult(string columnName, string value)
        {
            var expected = new OperablePredicate(columnName, "LIKE", value);
            var actual = Predicate.Like(columnName, value);
            Assert.Equal(expected, actual);
        }
        
        [Test]
        public void GreatThanReturnsCorrectResult(string columnName, object value)
        {
            var expected = new OperablePredicate(columnName, ">", value);
            var actual = Predicate.GreatThan(columnName, value);
            Assert.Equal(expected, actual);
        }
    }
}