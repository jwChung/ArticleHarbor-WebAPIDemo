namespace ArticleHarbor.DomainModel.Models
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Xunit;

    public class InClausePredicateTest : IdiomaticTest<InClausePredicate>
    {
        [Test]
        public void SutIsPredicate(InClausePredicate sut)
        {
            Assert.IsAssignableFrom<IPredicate>(sut);
        }

        [Test]
        public void SqlTextIsCorrect(InClausePredicate sut)
        {
            var expected = string.Format(
                "{0} IN ({1})",
                sut.ColumnName,
                string.Join(", ", sut.Parameters.Select(p => p.Name)));
            var actual = sut.SqlText;
            Assert.Equal(expected, actual);
        }

        protected override IEnumerable<MemberInfo> ExceptToVerifyInitialization()
        {
            yield return this.Properties.Select(x => x.SqlText);
        }
    }
}