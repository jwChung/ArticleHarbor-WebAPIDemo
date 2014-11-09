namespace ArticleHarbor.WebApiPresentationModel
{
    using System.Collections.Generic;
    using System.Reflection;
    using System.Security.Principal;
    using DomainModel;
    using Ploeh.AutoFixture.Xunit;
    using Xunit;

    public class SimplePrincipalTest : IdiomaticTest<SimplePrincipal>
    {
        [Test]
        public void SutIsPrincipal(SimplePrincipal sut)
        {
            Assert.IsAssignableFrom<IPrincipal>(sut);
        }

        [Test]
        public void SutIsIdentity(SimplePrincipal sut)
        {
            Assert.IsAssignableFrom<IIdentity>(sut);
        }

        [Test]
        public void IndentityIsCorrect(SimplePrincipal sut)
        {
            var actual = sut.Identity;
            Assert.Equal(actual, sut);
        }

        [Test]
        public void IsAuthenticatedIsCorrect(SimplePrincipal sut)
        {
            Assert.True(sut.IsAuthenticated);
        }

        [Test]
        public void AuthenticationTypeIsCorrect(SimplePrincipal sut)
        {
            var actual = sut.AuthenticationType;
            Assert.Equal("ApiKey", actual);
        }

        [Test]
        public void IsInRoleWithMatchedRoleNameReturnsTrue(
            [Frozen] Role role,
            SimplePrincipal sut)
        {
            var actual = sut.IsInRole(role.ToString());
            Assert.True(actual);
        }

        [Test]
        public void IsInRoleWithNotMatchedRoleNameReturnsFalse(
            SimplePrincipal sut,
            string roleName)
        {
            var actual = sut.IsInRole(roleName);
            Assert.False(actual);
        }

        [Test]
        public void IsInRoleIsCaseInsenstive(
            [Frozen] Role role,
            SimplePrincipal sut)
        {
            var actual = sut.IsInRole(role.ToString().ToLower());
            Assert.True(actual);
        }

        protected override IEnumerable<MemberInfo> ExceptToVerifyInitialization()
        {
            yield return this.Properties.Select(x => x.AuthenticationType);
            yield return this.Properties.Select(x => x.IsAuthenticated);
            yield return this.Properties.Select(x => x.Identity);
        }
    }
}