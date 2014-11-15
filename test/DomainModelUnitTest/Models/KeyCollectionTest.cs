namespace ArticleHarbor.DomainModel.Models
{
    using System.Collections.Generic;
    using System.Reflection;
    using Ploeh.Albedo;
    using Xunit;

    public class KeyCollectionOfTKeyTest : IdiomaticTest<KeyCollection<object>>
    {
        [Test]
        public void SutIsKeyCollection(KeyCollection<object> sut)
        {
            Assert.IsAssignableFrom<IKeyCollection>(sut);
        }

        [Test]
        public void SutReturnsCorrectKeys(KeyCollection<object> sut)
        {
            var expected = new object[] { sut.Key };
            Assert.Equal(expected, sut);
        }

        [Test]
        public void EqualsWithSameKeyReturnsTrue(KeyCollection<object> sut)
        {
            var other = new KeyCollection<object>(sut.Key);
            var actual = sut.Equals(other);
            Assert.True(actual);
        }

        [Test]
        public void EqualsWithNotSameKeyReturnsFalse(
            KeyCollection<object> sut, KeyCollection<object> other)
        {
            var actual = sut.Equals(other);
            Assert.False(actual);
        }

        [Test]
        public void GetHashCodeWithSameKeyReturnsSameValue(KeyCollection<object> sut)
        {
            var other = new KeyCollection<object>(sut.Key);
            var expected = other.GetHashCode();

            var actual = sut.GetHashCode();

            Assert.Equal(expected, actual);
        }
    }

    public class IdCollectionOfTKey1AndKey2Test : IdiomaticTest<KeyCollection<object, object>>
    {
        [Test]
        public void SutIsKeyCollection(KeyCollection<object, object> sut)
        {
            Assert.IsAssignableFrom<IKeyCollection>(sut);
        }

        [Test]
        public void SutReturnsCorrectKeys(KeyCollection<object, object> sut)
        {
            var expected = new object[] { sut.Key1, sut.Key2 };
            Assert.Equal(expected, sut);
        }

        [Test]
        public void EqualsWithSameKeyReturnsTrue(KeyCollection<object, object> sut)
        {
            var other = new KeyCollection<object, object>(sut.Key1, sut.Key2);
            var actual = sut.Equals(other);
            Assert.True(actual);
        }

        [Test]
        public void EqualsWithNotSameKeyReturnsFalse(
            KeyCollection<object, object> sut, KeyCollection<object, object> other)
        {
            var actual = sut.Equals(other);
            Assert.False(actual);
        }

        [Test]
        public void GetHashCodeWithSameKeyReturnsSameValue(KeyCollection<object, object> sut)
        {
            var other = new KeyCollection<object, object>(sut.Key1, sut.Key2);
            var expected = other.GetHashCode();

            var actual = sut.GetHashCode();

            Assert.Equal(expected, actual);
        }
    }
}