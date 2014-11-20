namespace ArticleHarbor.EFPersistenceModel
{
    using DomainModel.Models;
    using Xunit;

    public class UserRepository2Test : IdiomaticTest<UserRepository2>
    {
        [Test]
        public void SutIsRepository(UserRepository2 sut)
        {
            Assert.IsAssignableFrom<Repository<Keys<string>, User, EFDataAccess.User>>(sut);
        }
    }
}