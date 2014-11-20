namespace ArticleHarbor.EFPersistenceModel
{
    using System;
    using System.Linq;
    using DomainModel.Models;
    using EFDataAccess;
    using Ploeh.SemanticComparison.Fluent;
    using Xunit;
    using User = DomainModel.Models.User;

    public class UserRepository2Test : IdiomaticTest<UserRepository2>
    {
        [Test]
        public void SutIsRepository(UserRepository2 sut)
        {
            Assert.IsAssignableFrom<Repository<Keys<string>, User, EFDataAccess.User>>(sut);
        }

        [Test]
        public void ConvertToModelAsyncReturnsCorrectResult(
            ArticleHarborDbContext context,
            UserRepository2 sut)
        {
            EFDataAccess.User persistence = context.UserManager.FindByNameAsync("user2").Result;

            var actual = sut.ConvertToModelAsync(persistence).Result;

            Assert.Equal("user2", actual.Id);
            Assert.Equal(Guid.Parse("232494f5670943dfac807226449fe795"), actual.ApiKey);
            Assert.Equal(Role.Author, actual.Role);
        }

        [Test]
        public void ConvertToModelAsyncWithIncorrectUserIdThrows(
            UserRepository2 sut,
             EFDataAccess.User persistence)
        {
            var e = Assert.Throws<AggregateException>(() => sut.ConvertToModelAsync(persistence).Result);
            Assert.IsType<ArgumentException>(e.InnerException);
        }

        [Test(Skip = "NotImplementedException")]
        public void ConvertToPersistenceAsyncReturnsCorrectResult(
            ArticleHarborDbContext context,
            UserRepository2 sut)
        {
            var user = new User(
                "user1", Role.Administrator, Guid.Parse("692c7798206844b88ba9a660e3374eef"));

            var actual = sut.ConvertToPersistenceAsync(user).Result;

            actual.AsSource().OfLikeness<User>()
                .With(x => x.Id).EqualsWhen((a, b) => a.UserName == b.Id)
                .With(x => x.Role).EqualsWhen((a, b) => Role.Administrator == b.Role)
                .ShouldEqual(user);
        }
    }
}