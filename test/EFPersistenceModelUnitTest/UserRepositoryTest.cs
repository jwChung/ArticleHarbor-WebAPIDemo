namespace ArticleHarbor.EFPersistenceModel
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Threading.Tasks;
    using DomainModel;
    using Xunit;

    public class UserRepositoryTest : IdiomaticTest<UserRepository>
    {
        [Test]
        public void SutIsUserRepository(
            UserRepository sut)
        {
            Assert.IsAssignableFrom<IUserRepository>(sut);
        }

        [Test]
        public async Task SelectAsyncReturnsCorrectUserWhenUserExits(
            UserRepository sut)
        {
            var actual = await sut.SelectAsync("user2", "password2");

            Assert.Equal(actual.Id, "user2");
            Assert.Equal(Role.Author, actual.Role);
        }

        protected override IEnumerable<MemberInfo> ExceptToVerifyGuardClause()
        {
            yield return this.Methods.Select(x => x.SelectAsync(Guid.Empty));
        }
    }
}