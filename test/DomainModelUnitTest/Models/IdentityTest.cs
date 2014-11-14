namespace ArticleHarbor.DomainModel.Models
{
    using System.Collections.Generic;
    using System.Reflection;
    using Ploeh.Albedo;
    using Xunit;

    public class IdentityOfTKeyTest : IdiomaticTest<Identity<object>>
    {
        [Test]
        public void SutIsIndentity(Identity<object> sut)
        {
            Assert.IsAssignableFrom<IIndentity>(sut);
        }

        [Test]
        public void KeysReturnsCorrectValues(Identity<object> sut)
        {
            var expected = new object[] { sut.Key };
            Assert.Equal(expected, sut.Keys);
        }

        [Test]
        public void KeysAlwaysReturnsSameInstance(Identity<object> sut)
        {
            var actual = sut.Keys;
            Assert.Same(sut.Keys, actual);
        }

        [Test]
        public void EqualsWithSameKeyReturnsTrue(Identity<object> sut)
        {
            var other = new Identity<object>(sut.Key);
            var actual = sut.Equals(other);
            Assert.True(actual);
        }

        [Test]
        public void EqualsWithNotSameKeyReturnsFalse(
            Identity<object> sut, Identity<object> other)
        {
            var actual = sut.Equals(other);
            Assert.False(actual);
        }

        [Test]
        public void GetHashCodeWithSameKeyReturnsSameValue(Identity<object> sut)
        {
            var other = new Identity<object>(sut.Key);
            var expected = other.GetHashCode();

            var actual = sut.GetHashCode();

            Assert.Equal(expected, actual);
        }

        protected override IEnumerable<MemberInfo> ExceptToVerifyInitialization()
        {
            yield return this.Properties.Select(x => x.Keys);
        }
    }

    public class IdentityOfTKey1AndKey2Test : IdiomaticTest<Identity<object, object>>
    {
        [Test]
        public void SutIsIndentity(Identity<object, object> sut)
        {
            Assert.IsAssignableFrom<IIndentity>(sut);
        }

        [Test]
        public void KeysReturnsCorrectValues(Identity<object, object> sut)
        {
            var expected = new object[] { sut.Key1, sut.Key2 };
            Assert.Equal(expected, sut.Keys);
        }

        [Test]
        public void KeysAlwaysReturnsSameInstance(Identity<object, object> sut)
        {
            var actual = sut.Keys;
            Assert.Same(sut.Keys, actual);
        }

        [Test]
        public void EqualsWithSameKeyReturnsTrue(Identity<object, object> sut)
        {
            var other = new Identity<object, object>(sut.Key1, sut.Key2);
            var actual = sut.Equals(other);
            Assert.True(actual);
        }

        [Test]
        public void EqualsWithNotSameKeyReturnsFalse(
            Identity<object, object> sut, Identity<object, object> other)
        {
            var actual = sut.Equals(other);
            Assert.False(actual);
        }

        [Test]
        public void GetHashCodeWithSameKeyReturnsSameValue(Identity<object, object> sut)
        {
            var other = new Identity<object, object>(sut.Key1, sut.Key2);
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