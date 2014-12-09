namespace ArticleHarbor.DomainModel.Models
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Ploeh.AutoFixture.Xunit;
    using Xunit;

    public class InClausePredicateTest : IdiomaticTest<InClausePredicate>
    {
        [Test]
        public void SutIsPredicate(InClausePredicate sut)
        {
            Assert.IsAssignableFrom<IPredicate>(sut);
        }

        [Test]
        public void SqlTextIsCorrectWhenInitializedWithParameterArray(
            [FavorTypes(typeof(IParameter[]))] InClausePredicate sut)
        {
            var expected = string.Format(
                "{0} IN ({1})",
                sut.ColumnName,
                string.Join(", ", sut.Parameters.Select(p => p.Name)));
            var actual = sut.SqlText;
            Assert.Equal(expected, actual);
        }

        [Test]
        public void SqlTextIsCorrectWhenInitializedWithObjectArray(
            [FavorTypes(typeof(object[]))] InClausePredicate sut)
        {
            var parameterNames = sut.ParameterValues.Select(
                (v, i) => sut.ColumnName + (i + 1).ToString().PadLeft(2, '0'));
            var expected = string.Format(
                "{0} IN ({1})",
                sut.ColumnName,
                string.Join(", ", parameterNames));

            var actual = sut.SqlText;

            Assert.Equal(expected, actual);
        }

        [Test]
        public void ParameterValuesIsCorrectWhenInitializedWithParameterArray(
            [FavorTypes(typeof(IParameter[]))] InClausePredicate sut)
        {
            var expected = sut.Parameters.Select(x => x.Value);
            var actual = sut.ParameterValues;
            Assert.Equal(expected, actual);
        }

        protected override IEnumerable<MemberInfo> ExceptToVerifyInitialization()
        {
            yield return this.Properties.Select(x => x.SqlText);
        }
    }
}