namespace ArticleHarbor.DomainModel.Models
{
    using Xunit;

    public class ParameterTest : IdiomaticTest<Parameter>
    {
        [Test]
        public void SutIsParameter(Parameter sut)
        {
            Assert.IsAssignableFrom<IParameter>(sut);
        }

        [Test]
        public void EqualsParameterWithSameValuesReturnsTrue(Parameter sut)
        {
            var other = new Parameter(sut.Name, sut.Value);
            var actual = sut.Equals(other);
            Assert.True(actual);
        }

        [Test]
        public void EqualsParameterWithNotSameValuesReturnsTrue(Parameter sut, Parameter other)
        {
            var actual = sut.Equals(other);
            Assert.False(actual);
        }

        [Test]
        public void GetHashCodeWithSameParameterReturnsSameValue(Parameter sut)
        {
            var other = new Parameter(sut.Name, sut.Value);
            var actual = sut.Equals(other);
            Assert.True(actual);
        }
    }
}