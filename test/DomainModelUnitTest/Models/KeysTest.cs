namespace ArticleHarbor.DomainModel.Models
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Ploeh.Albedo;
    using Xunit;

    public class KeysTest : IdiomaticTest<Keys>
    {
        [Test]
        public void SutIsKeyCollection(Keys sut)
        {
            Assert.IsAssignableFrom<IKeys>(sut);
        }

        [Test]
        public void SutReturnsCorrectKeys(Keys sut)
        {
            var expected = sut.KeyValues;
            Assert.Equal(expected, sut);
        }

        [Test]
        public void EqualsWithSameKeyValuesReturnsTrue(Keys sut)
        {
            var other = new Keys(sut.KeyValues.ToArray());
            var actual = sut.Equals(other);
            Assert.True(actual);
        }

        [Test]
        public void EqualsWithNotSameValuesReturnsFalse(
            Keys sut, Keys other)
        {
            var actual = sut.Equals(other);
            Assert.False(actual);
        }

        [Test]
        public void GetHashCodeWithSameValuesReturnsSameValue(Keys sut)
        {
            var other = new Keys(sut.KeyValues.ToArray());
            var expected = other.GetHashCode();

            var actual = sut.GetHashCode();

            Assert.Equal(expected, actual);
        }

        [Test]
        public void GetHashCodeWithNotSameValuesReturnsDifferentValue(Keys sut, Keys other)
        {
            var expected = other.GetHashCode();
            var actual = sut.GetHashCode();
            Assert.NotEqual(expected, actual);
        }
    }

    public class KeysOfTKeyTest : IdiomaticTest<Keys<object>>
    {
        [Test]
        public void SutIsKeyCollection(Keys<object> sut)
        {
            Assert.IsAssignableFrom<IKeys>(sut);
        }

        [Test]
        public void SutReturnsCorrectKeys(Keys<object> sut)
        {
            var expected = new object[] { sut.Key };
            Assert.Equal(expected, sut);
        }

        [Test]
        public void EqualsWithSameKeyReturnsTrue(Keys<object> sut)
        {
            var other = new Keys<object>(sut.Key);
            var actual = sut.Equals(other);
            Assert.True(actual);
        }

        [Test]
        public void EqualsWithNotSameKeyReturnsFalse(
            Keys<object> sut, Keys<object> other)
        {
            var actual = sut.Equals(other);
            Assert.False(actual);
        }

        [Test]
        public void GetHashCodeWithSameKeyReturnsSameValue(Keys<object> sut)
        {
            var other = new Keys<object>(sut.Key);
            var expected = other.GetHashCode();

            var actual = sut.GetHashCode();

            Assert.Equal(expected, actual);
        }
    }

    public class KeysOfTKey1AndKey2Test : IdiomaticTest<Keys<object, object>>
    {
        [Test]
        public void SutIsKeyCollection(Keys<object, object> sut)
        {
            Assert.IsAssignableFrom<IKeys>(sut);
        }

        [Test]
        public void SutReturnsCorrectKeys(Keys<object, object> sut)
        {
            var expected = new object[] { sut.Key1, sut.Key2 };
            Assert.Equal(expected, sut);
        }

        [Test]
        public void EqualsWithSameKeyReturnsTrue(Keys<object, object> sut)
        {
            var other = new Keys<object, object>(sut.Key1, sut.Key2);
            var actual = sut.Equals(other);
            Assert.True(actual);
        }

        [Test]
        public void EqualsWithNotSameKeyReturnsFalse(
            Keys<object, object> sut, Keys<object, object> other)
        {
            var actual = sut.Equals(other);
            Assert.False(actual);
        }

        [Test]
        public void GetHashCodeWithSameKeyReturnsSameValue(Keys<object, object> sut)
        {
            var other = new Keys<object, object>(sut.Key1, sut.Key2);
            var expected = other.GetHashCode();

            var actual = sut.GetHashCode();

            Assert.Equal(expected, actual);
        }
    }
}