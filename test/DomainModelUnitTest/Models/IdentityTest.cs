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
}