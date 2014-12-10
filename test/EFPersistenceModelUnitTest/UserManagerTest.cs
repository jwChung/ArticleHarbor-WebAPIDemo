namespace ArticleHarbor.EFPersistenceModel
{
    using System.Collections.Generic;
    using System.Reflection;
    using DomainModel.Repositories;
    using Xunit;

    public class UserManagerTest : IdiomaticTest<UserManager>
    {
        [Test]
        public void SutIsUserManager(UserManager sut)
        {
            Assert.IsAssignableFrom<IUserManager>(sut);
        }

        protected override IEnumerable<MemberInfo> ExceptToVerifyGuardClause()
        {
            yield return this.Methods.Select(x => x.FindAsync(null, null));
        }
    }
}