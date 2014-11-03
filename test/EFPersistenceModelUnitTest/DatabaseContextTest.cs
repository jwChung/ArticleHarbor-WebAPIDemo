namespace EFPersistenceModelUnitTest
{
    using DomainModel;
    using EFPersistenceModel;
    using Xunit;

    public class DatabaseContextTest
    {
        [Test]
        public void SutIsDatabaseContext(DatabaseContext sut)
        {
            Assert.IsAssignableFrom<IDatabaseContext>(sut);
        }
    }
}