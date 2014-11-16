namespace ArticleHarbor.EFPersistenceModel
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Threading.Tasks;
    using DomainModel;
    using DomainModel.Models;
    using DomainModel.Repositories;
    using Ploeh.SemanticComparison.Fluent;
    using Xunit;

    public class UserRepositoryTest : IdiomaticTest<UserRepository>
    {
        [DbContextTest]
        public void SutIsUserRepository(
            UserRepository sut)
        {
            Assert.IsAssignableFrom<IUserRepository>(sut);
        }

        [DbContextTest]
        public async Task FindAsyncReturnsCorrectUser(
            UserRepository sut)
        {
            var actual = await sut.FindAsync("user2", "password2");

            Assert.Equal("user2", actual.Id);
            Assert.Equal(Role.Author, actual.Role);
        }

        [DbContextTest]
        public async Task FindAsyncWithIncorrectIdOrPasswordReturnsNullUser(
            UserRepository sut)
        {
            var actual = await sut.FindAsync("user2", "password");
            Assert.Null(actual);
        }

        [DbContextTest]
        public async Task FindAsyncWithApiKeyReturnsCorrectUser(
            UserRepository sut)
        {
            var actual = await sut.FindAsync(Guid.Parse("232494f5670943dfac807226449fe795"));
            Assert.Equal("user2", actual.Id);
        }

        [DbContextTest]
        public async Task FindAsyncWithIncorrectApiKeyReturnsNullUser(
            UserRepository sut,
            Guid apiKey)
        {
            var actual = await sut.FindAsync(apiKey);
            Assert.Null(actual);
        }

        [DbContextTest]
        public async Task FindAsyncWithUserIdReturnsCorrectUser(
             UserRepository sut)
        {
            var id = "user1";
            var actual = await sut.FindAsync(id);
            Assert.Equal(id, actual.Id);
        }

        [DbContextTest]
        public async Task FindAsyncWithIncorrectUserIdReturnsNullUser(
            UserRepository sut,
            string id)
        {
            var actual = await sut.FindAsync(id);
            Assert.Null(actual);
        }

        protected override IEnumerable<MemberInfo> ExceptToVerifyGuardClause()
        {
            yield return this.Methods.Select(x => x.FindAsync(Guid.Empty));
        }
    }
}