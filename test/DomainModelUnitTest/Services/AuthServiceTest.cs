namespace ArticleHarbor.DomainModel.Services
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Threading.Tasks;
    using Jwc.Experiment.Xunit;
    using Models;
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
        public async Task FindUserAsyncReturnsCorrectUser(
            AuthService sut,
            string id,
            string password,
            User user)
        {
            sut.Users.Of(x => x.FindAsync(id, password) == Task.FromResult(user));
            var actual = await sut.FindUserAsync(id, password);
            Assert.Equal(user, actual);
        }

        [Test]
        public async Task FindUserAsyncWithApiKeyReturnsCorrectUser(
            AuthService sut,
            Guid apiKey,
            User user)
        {
            sut.Users.Of(x => x.FindAsync(apiKey) == Task.FromResult(user));
            var actual = await sut.FindUserAsync(apiKey);
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

        [Test]
        public async Task HasPermissionsAsyncThrowsWhenThereIsNoUserMatchedWithActor(
            AuthService sut,
            string id,
            Permissions permissions)
        {
            sut.Users.Of(x => x.FindAsync(id) == Task.FromResult<User>(null));
            try
            {
                await sut.HasPermissionsAsync(id, permissions);
                Assert.True(false, "throws exception.");
            }
            catch (ArgumentException)
            {
            }
        }

        [Test]
        public IEnumerable<ITestCase> HasPermissionsAsyncReturnsCorrectResult(
            string userId,
            Guid value)
        {
            var testData = new[]
            {
                new
                {
                    Permissions = Permissions.DeleteAny,
                    User = new User(userId, Role.User, value),
                    Expected = false
                },
                new
                {
                    Permissions = Permissions.DeleteAny,
                    User = new User(userId, Role.Administrator, value),
                    Expected = true
                },
                new
                {
                    Permissions = Permissions.Create,
                    User = new User(userId, Role.User, value),
                    Expected = false
                },
                new
                {
                    Permissions = Permissions.Create,
                    User = new User(userId, Role.Author, value),
                    Expected = true
                },
            };
            return TestCases.WithArgs(testData).WithAuto<AuthService, string>().Create(
                (d, sut, id) =>
                {
                    sut.Users.Of(x => x.FindAsync(id) == Task.FromResult<User>(d.User));
                    var actual = sut.HasPermissionsAsync(id, d.Permissions).Result;
                    Assert.Equal(d.Expected, actual);
                });
        }

        protected override IEnumerable<MemberInfo> ExceptToVerifyGuardClause()
        {
            yield return this.Methods.Select(x => x.FindUserAsync(Guid.Empty));
            yield return this.Methods.Select(x => x.HasPermissionsAsync(null, Permissions.None));
        }
    }
}