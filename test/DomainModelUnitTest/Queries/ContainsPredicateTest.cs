namespace ArticleHarbor.DomainModel.Queries
{
    using Xunit;

    public class ContainsPredicateTest : IdiomaticTest<ContainsPredicate>
    {
        [Test]
        public void SutIsOperablePredicate(ContainsPredicate sut)
        {
            Assert.IsAssignableFrom<OperablePredicate>(sut);
        }

        [Test]
        public void OperatorNameIsCorrect(ContainsPredicate sut)
        {
            var actual = sut.OperatorName;
            Assert.Equal("LIKE", actual);
        }

        [Test]
        public void ValueIsCorrect(string columnName, string[] values)
        {
            var sut = new ContainsPredicate(
                columnName,
                string.Join("%", values));
            var expected = string.Format("%{0}%", string.Join("[%]", values));

            var actual = sut.Value;

            Assert.Equal(expected, actual);
        }
    }
}