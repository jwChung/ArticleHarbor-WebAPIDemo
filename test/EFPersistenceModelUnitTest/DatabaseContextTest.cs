namespace EFPersistenceModelUnitTest
{
    using System.Collections.Generic;
    using System.Reflection;
    using DomainModel;
    using EFPersistenceModel;
    using Xunit;

    public class DatabaseContextTest : IdiomaticTest<DatabaseContext>
    {
        [Test]
        public void SutIsDatabaseContext(DatabaseContext sut)
        {
            Assert.IsAssignableFrom<IDatabaseContext>(sut);
        }

        protected override IEnumerable<MemberInfo> ExceptToVerifyInitialization()
        {
            yield return this.Properties.Select(x => x.Articles);
        }
    }
}