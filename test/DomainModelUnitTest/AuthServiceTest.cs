namespace ArticleHarbor.DomainModel
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Threading.Tasks;
    using Moq;
    using Xunit;

    public class AuthServiceTest : IdiomaticTest<AuthService>
    {
        [Test]
        public void SutIsAuthService(
            AuthService sut)
        {
            Assert.IsAssignableFrom<IAuthService>(sut);
        }

        [Test]
        public async Task FindUserAsync(
            AuthService sut,
            string id,
            string password,
            User user)
        {
            sut.Users.Of(x => x.SelectAsync(id, password) == Task.FromResult(user));
            var actual = await sut.FindUserAsync(id, password);
            Assert.Equal(user, actual);
        }

        [Test]
        public void DisposeCorrectlyDisposesOwnedService(
            AuthService sut)
        {
            sut.Dispose();
            sut.Dispose();

            sut.Owned.ToMock().Verify(x => x.Dispose(), Times.Once());
        }

        protected override IEnumerable<MemberInfo> ExceptToVerifyGuardClause()
        {
            yield return this.Methods.Select(x => x.FindUserAsync(Guid.Empty));
        }
    }
}