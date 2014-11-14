namespace ArticleHarbor.DomainModel.Models
{
    using System.Collections.Generic;
    using System.Reflection;
    using Ploeh.Albedo;
    using Xunit;

    public class IdOfTKeyTest : IdiomaticTest<Id<object>>
    {
        [Test]
        public void SutIsIndentity(Id<object> sut)
        {
            Assert.IsAssignableFrom<IId>(sut);
        }

        [Test]
        public void KeysReturnsCorrectValues(Id<object> sut)
        {
            var expected = new object[] { sut.Key };
            Assert.Equal(expected, sut.Keys);
        }

        [Test]
        public void KeysAlwaysReturnsSameInstance(Id<object> sut)
        {
            var actual = sut.Keys;
            Assert.Same(sut.Keys, actual);
        }

        [Test]
        public void EqualsWithSameKeyReturnsTrue(Id<object> sut)
        {
            var other = new Id<object>(sut.Key);
            var actual = sut.Equals(other);
            Assert.True(actual);
        }

        [Test]
        public void EqualsWithNotSameKeyReturnsFalse(
            Id<object> sut, Id<object> other)
        {
            var actual = sut.Equals(other);
            Assert.False(actual);
        }

        [Test]
        public void GetHashCodeWithSameKeyReturnsSameValue(Id<object> sut)
        {
            var other = new Id<object>(sut.Key);
            var expected = other.GetHashCode();

            var actual = sut.GetHashCode();

            Assert.Equal(expected, actual);
        }

        protected override IEnumerable<MemberInfo> ExceptToVerifyInitialization()
        {
            yield return this.Properties.Select(x => x.Keys);
        }
    }

    public class IdentityOfTKey1AndKey2Test : IdiomaticTest<Id<object, object>>
    {
        [Test]
        public void SutIsIndentity(Id<object, object> sut)
        {
            Assert.IsAssignableFrom<IId>(sut);
        }

        [Test]
        public void KeysReturnsCorrectValues(Id<object, object> sut)
        {
            var expected = new object[] { sut.Key1, sut.Key2 };
            Assert.Equal(expected, sut.Keys);
        }

        [Test]
        public void KeysAlwaysReturnsSameInstance(Id<object, object> sut)
        {
            var actual = sut.Keys;
            Assert.Same(sut.Keys, actual);
        }

        [Test]
        public void EqualsWithSameKeyReturnsTrue(Id<object, object> sut)
        {
            var other = new Id<object, object>(sut.Key1, sut.Key2);
            var actual = sut.Equals(other);
            Assert.True(actual);
        }

        [Test]
        public void EqualsWithNotSameKeyReturnsFalse(
            Id<object, object> sut, Id<object, object> other)
        {
            var actual = sut.Equals(other);
            Assert.False(actual);
        }

        [Test]
        public void GetHashCodeWithSameKeyReturnsSameValue(Id<object, object> sut)
        {
            var other = new Id<object, object>(sut.Key1, sut.Key2);
            var expected = other.GetHashCode();

            var actual = sut.GetHashCode();

            Assert.Equal(expected, actual);
        }

        protected override IEnumerable<MemberInfo> ExceptToVerifyInitialization()
        {
            yield return this.Properties.Select(x => x.Keys);
        }
    }
}