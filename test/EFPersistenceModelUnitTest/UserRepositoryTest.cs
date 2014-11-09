namespace ArticleHarbor.EFPersistenceModel
{
    using System.Threading.Tasks;
    using ArticleHarbor.DomainModel;
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
    }
}