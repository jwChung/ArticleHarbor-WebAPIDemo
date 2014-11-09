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
        public async Task SelectAsyncReturnsCorrectUser(
            UserRepository sut)
        {
            var actual = await sut.SelectAsync("user2", "password2");

            Assert.Equal("user2", actual.Id);
            Assert.Equal(Role.Author, actual.Role);
        }

        [Test]
        public async Task SelectAsyncWithIncorrectIdOrPasswordReturnsNullUser(
            UserRepository sut)
        {
            var actual = await sut.SelectAsync("user2", "password");
            Assert.Null(actual);
        }

        [Test]
        public async Task SelectAsyncWithApiKeyReturnsCorrectUser(
            UserRepository sut)
        {
            var actual = await sut.SelectAsync(Guid.Parse("232494f5670943dfac807226449fe795"));
            Assert.Equal("user2", actual.Id);
        }

        [Test]
        public async Task SelectAsyncWithIncorrectApiKeyReturnsNullUser(
            UserRepository sut,
            Guid apiKey)
        {
            var actual = await sut.SelectAsync(apiKey);
            Assert.Null(actual);
        }

        protected override IEnumerable<MemberInfo> ExceptToVerifyGuardClause()
        {
            yield return this.Methods.Select(x => x.SelectAsync(Guid.Empty));
        }
    }
}