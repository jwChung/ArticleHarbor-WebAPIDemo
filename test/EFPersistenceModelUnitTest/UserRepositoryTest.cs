namespace ArticleHarbor.EFPersistenceModel
{
    using System;
    using System.Linq;
    using DomainModel;
    using DomainModel.Models;
    using DomainModel.Queries;
    using EFDataAccess;
    using Ploeh.SemanticComparison.Fluent;
    using Xunit;
    using User = DomainModel.Models.User;

    public class UserRepositoryTest : IdiomaticTest<UserRepository>
    {
        [Test]
        public void SutIsRepository(UserRepository sut)
        {
            Assert.IsAssignableFrom<Repository<Keys<string>, User, EFDataAccess.User>>(sut);
        }

        [Test]
        public void ConvertToModelAsyncReturnsCorrectResult(
            ArticleHarborDbContext context,
            UserRepository sut)
        {
            EFDataAccess.User persistence = context.UserManager.FindByIdAsync("user2").Result;

            var actual = sut.ConvertToModelAsync(persistence).Result;

            Assert.Equal("user2", actual.Id);
            Assert.Equal(Guid.Parse("232494f5670943dfac807226449fe795"), actual.ApiKey);
            Assert.Equal(Role.Author, actual.Role);
        }

        [Test(Skip = "NotImplementedException")]
        public void ConvertToPersistenceAsyncReturnsCorrectResult(
            ArticleHarborDbContext context,
            UserRepository sut)
        {
            var user = new User(
                "user1", Role.Administrator, Guid.Parse("692c7798206844b88ba9a660e3374eef"));

            var actual = sut.ConvertToPersistenceAsync(user).Result;

            actual.AsSource().OfLikeness<User>()
                .With(x => x.Id).EqualsWhen((a, b) => a.UserName == b.Id)
                .With(x => x.Role).EqualsWhen((a, b) => Role.Administrator == b.Role)
                .ShouldEqual(user);
        }

        [Test]
        public void SelectAsyncReturnsCorrectResult(UserRepository sut)
        {
            var actual = sut.SelectAsync().Result;
            Assert.Equal(3, actual.Count());
        }

        [Test]
        public void ExecuteSelectCommandAsyncReturnsCorrectResult(UserRepository sut)
        {
            var actual = sut.ExecuteSelectCommandAsync(Predicate.Equal("Id", "user1")).Result;
            Assert.Equal("user1", actual.Single().Id);
        }

        [Test]
        public void FindAsyncReturnsCorrectUser(UserRepository sut)
        {
            var actual = sut.FindAsync(new Keys<string>("User1")).Result;
            Assert.Equal("user1", actual.Id);
        }
    }
}