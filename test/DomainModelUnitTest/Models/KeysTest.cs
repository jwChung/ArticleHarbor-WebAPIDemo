namespace ArticleHarbor.DomainModel.Models
{
    using System.Collections.Generic;
    using System.Reflection;
    using Ploeh.Albedo;
    using Xunit;

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