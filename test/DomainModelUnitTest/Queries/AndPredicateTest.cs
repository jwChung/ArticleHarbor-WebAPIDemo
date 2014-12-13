namespace ArticleHarbor.DomainModel.Queries
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Ploeh.AutoFixture.Xunit;
    using Xunit;

    public class AndPredicateTest : IdiomaticTest<AndPredicate>
    {
        [Test]
        public void SutIsPredicate(AndPredicate sut)
        {
            Assert.IsAssignableFrom<IPredicate>(sut);
        }

        [Test]
        public void SqlTextIsCorrect(
            [Frozen] IEnumerable<IPredicate> predicates,
            string[] sqlTexts,
            [FavorEnumerables] AndPredicate sut)
        {
            Assert.Equal(predicates, sut.Predicates);
            predicates.Select((p, i) => p.Of(x => x.SqlText == sqlTexts[i])).ToArray();
            var expected = string.Join(" AND ", predicates.Select(p => p.SqlText));

            var actual = sut.SqlText;

            Assert.Equal(expected, actual);
        }

        [Test]
        public void ParametersIsCorrect(
            [Frozen] IEnumerable<IPredicate> predicates,
            IEnumerable<IParameter>[] parameterValues,
            [FavorEnumerables] AndPredicate sut)
        {
            predicates.Select((p, i) => p.Of(x => x.Parameters == parameterValues[i])).ToArray();
            var expected = parameterValues.SelectMany(x => x);

            var actual = sut.Parameters;

            Assert.Equal(expected, actual);
        }

        [Test]
        public void EqualsDoesNotEqualOtherWithDifferentValues(
            AndPredicate sut,
            AndPredicate other)
        {
            var actual = sut.Equals(other);
            Assert.False(actual);
        }

        [Test]
        public void EqualsEqualsOtherWithSameValues(AndPredicate sut)
        {
            var other = new AndPredicate(sut.Predicates.ToArray());
            var actual = sut.Equals(other);
            Assert.True(actual);
        }

        [Test]
        public void GetHasCodeReturnsCorrectResult(AndPredicate sut)
        {
            var other = new AndPredicate(sut.Predicates.ToArray());
            var actual = sut.GetHashCode();
            Assert.Equal(other.GetHashCode(), actual);
        }

        protected override IEnumerable<MemberInfo> ExceptToVerifyInitialization()
        {
            yield return this.Properties.Select(x => x.SqlText);
            yield return this.Properties.Select(x => x.Parameters);
        }
    }
}