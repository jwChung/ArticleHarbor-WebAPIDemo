namespace EFPersistenceModelUnitTest
{
    using DomainModel;
    using EFPersistenceModel;
    using Xunit;

    public class UserRepositoryTest : IdiomaticTest<UserRepository>
    {
        [Test]
        public void SutIsUserRepository(
            UserRepository sut)
        {
            Assert.IsAssignableFrom<IUserRepository>(sut);
        }
    }
}