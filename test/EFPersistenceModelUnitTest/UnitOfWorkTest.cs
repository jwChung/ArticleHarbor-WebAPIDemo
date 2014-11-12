namespace ArticleHarbor.EFPersistenceModel
{
    using System.Collections.Generic;
    using System.Reflection;
    using System.Threading.Tasks;
    using ArticleHarbor.DomainModel;
    using ArticleHarbor.EFDataAccess;
    using DomainModel.Repositories;
    using Moq;
    using Ploeh.AutoFixture;
    using Xunit;

    public class UnitOfWorkTest : IdiomaticTest<UnitOfWork>
    {
        [Test]
        public void SutIsUnitOfWork(UnitOfWork sut)
        {
            Assert.IsAssignableFrom<IUnitOfWork>(sut);
        }

        [Test]
        public void ArticlesIsCorrect(UnitOfWork sut)
        {
            Assert.NotNull(sut.Context.Articles);

            var actual = sut.Articles;

            var repository = Assert.IsAssignableFrom<ArticleRepository>(actual);
            Assert.Same(sut.Context, repository.Context);
        }

        [Test]
        public void ArticlesAlwaysReturnsSameInstance(UnitOfWork sut)
        {
            var actual = sut.Articles;
            Assert.Same(sut.Articles, actual);
        }

        [Test]
        public void KeywordsIsCorrect(UnitOfWork sut)
        {
            Assert.NotNull(sut.Context.Keywords);

            var actual = sut.Keywords;

            var repository = Assert.IsAssignableFrom<KeywordRepository>(actual);
            Assert.Same(sut.Context, repository.Context);
        }

        [Test]
        public void KeywordsAlwaysReturnsSameInstance(UnitOfWork sut)
        {
            var actual = sut.Keywords;
            Assert.Same(sut.Keywords, actual);
        }

        [Test]
        public void UsersIsCorrect(UnitOfWork sut)
        {
            var actual = sut.Users;

            var repository = Assert.IsAssignableFrom<UserRepository>(actual);
            Assert.Same(sut.Context, repository.Context);
        }

        [Test]
        public void UsersAlwaysReturnsSameInstance(UnitOfWork sut)
        {
            var actual = sut.Users;
            Assert.Same(sut.Users, actual);
        }

        [Test]
        public void BookmarksIsCorrect(UnitOfWork sut)
        {
            var actual = sut.Bookmarks;

            var repository = Assert.IsAssignableFrom<BookmarkRepository>(actual);
            Assert.Same(sut.Context, repository.Context);
        }

        [Test]
        public void BookmarksAlwaysReturnsSameInstance(UnitOfWork sut)
        {
            var actual = sut.Bookmarks;
            Assert.Same(sut.Bookmarks, actual);
        }

        [Test]
        public async Task SaveAsyncCorrectlySaves(
            Mock<ArticleHarborDbContext> context,
            IFixture fixture)
        {
            context.CallBase = false;
            fixture.Inject(context.Object);
            var sut = fixture.Create<UnitOfWork>();

            await sut.SaveAsync();

            context.Verify(x => x.SaveChangesAsync());
        }
        
        protected override IEnumerable<MemberInfo> ExceptToVerifyInitialization()
        {
            yield return this.Properties.Select(x => x.Articles);
            yield return this.Properties.Select(x => x.Keywords);
            yield return this.Properties.Select(x => x.Users);
            yield return this.Properties.Select(x => x.Bookmarks);
        }
    }
}