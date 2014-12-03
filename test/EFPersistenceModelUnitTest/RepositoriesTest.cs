namespace ArticleHarbor.EFPersistenceModel
{
    using System.Collections.Generic;
    using System.Reflection;
    using DomainModel.Repositories;
    using Xunit;

    public class RepositoriesTest : IdiomaticTest<Repositories>
    {
        [Test]
        public void SutIsRepositories(Repositories sut)
        {
            Assert.IsAssignableFrom<IRepositories>(sut);
        }

        protected override IEnumerable<MemberInfo> ExceptToVerifyInitialization()
        {
            yield return this.Properties.Select(x => x.Articles);
            yield return this.Properties.Select(x => x.Keywords);
            yield return this.Properties.Select(x => x.Bookmarks);
            yield return this.Properties.Select(x => x.Users);
        }
    }
}