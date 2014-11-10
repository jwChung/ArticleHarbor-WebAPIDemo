namespace ArticleHarbor.EFPersistenceModel
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Threading.Tasks;
    using DomainModel;
    using Ploeh.SemanticComparison.Fluent;
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
        public async Task FindAsyncReturnsCorrectUser(
            UserRepository sut)
        {
            var actual = await sut.FindAsync("user2", "password2");

            Assert.Equal("user2", actual.Id);
            Assert.Equal(Role.Author, actual.Role);
        }

        [Test]
        public async Task FindAsyncWithIncorrectIdOrPasswordReturnsNullUser(
            UserRepository sut)
        {
            var actual = await sut.FindAsync("user2", "password");
            Assert.Null(actual);
        }

        [Test]
        public async Task FindAsyncWithApiKeyReturnsCorrectUser(
            UserRepository sut)
        {
            var actual = await sut.FindAsync(Guid.Parse("232494f5670943dfac807226449fe795"));
            Assert.Equal("user2", actual.Id);
        }

        [Test]
        public async Task FindAsyncWithIncorrectApiKeyReturnsNullUser(
            UserRepository sut,
            Guid apiKey)
        {
            var actual = await sut.FindAsync(apiKey);
            Assert.Null(actual);
        }

        protected override IEnumerable<MemberInfo> ExceptToVerifyGuardClause()
        {
            yield return this.Methods.Select(x => x.FindAsync(Guid.Empty));
        }
    }
}