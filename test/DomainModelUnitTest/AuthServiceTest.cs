﻿namespace DomainModelUnitTest
{
    using System.Threading.Tasks;
    using DomainModel;
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
    }
}