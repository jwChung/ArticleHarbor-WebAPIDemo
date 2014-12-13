namespace ArticleHarbor.DomainModel.Queries
{
    using System;
    using Xunit;

    public class ParameterTest : IdiomaticTest<Parameter>
    {
        [Test]
        public void SutIsParameter(Parameter sut)
        {
            Assert.IsAssignableFrom<IParameter>(sut);
        }

        [Test]
        public void InitializeWithIncorrectNameThrows(
            string name,
            object value)
        {
            Assert.Throws<ArgumentException>(() => new Parameter(name, value));
        }

        [Test]
        public void InitializeWithEmptyNameThrows(object value)
        {
            Assert.Throws<ArgumentException>(() => new Parameter(string.Empty, value));
        }

        [Test]
        public void EqualsParameterWithSameValuesReturnsTrue(
            Parameter sut)
        {
            var other = new Parameter(sut.Name, sut.Value);
            var actual = sut.Equals(other);
            Assert.True(actual);
        }

        [Test]
        public void EqualsParameterWithNotSameValuesReturnsFalse(
            Parameter sut,
            Parameter other)
        {
            var actual = sut.Equals(other);
            Assert.False(actual);
        }

        [Test]
        public void EqualsIsCaseInsensitiveForName(Parameter sut)
        {
            Assert.Equal(new Parameter(sut.Name.ToUpper(), sut.Value), sut);
            Assert.Equal(new Parameter(sut.Name.ToLower(), sut.Value), sut);
        }

        [Test]
        public void GetHashCodeWithSameValuesReturnsSameCode(Parameter sut)
        {
            var other = new Parameter(sut.Name.ToUpper(), sut.Value);
            var actual = sut.GetHashCode();
            Assert.Equal(other.GetHashCode(), actual);
        }
    }
}