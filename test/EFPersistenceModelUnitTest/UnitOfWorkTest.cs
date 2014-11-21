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
        
        [Test]
        public void ArticlesIsCorrect(UnitOfWork sut)
        {
            var actual = sut.Articles;

            var articles = Assert.IsAssignableFrom<ArticleRepository2>(actual);
            Assert.Equal(sut.Context, articles.Context);
            Assert.Equal(sut.Context.Articles, articles.DbSet);
        }

        [Test]
        public void KeywordsIsCorrect(UnitOfWork sut)
        {
            var actual = sut.Keywords;

            var articles = Assert.IsAssignableFrom<KeywordRepository2>(actual);
            Assert.Equal(sut.Context, articles.Context);
            Assert.Equal(sut.Context.Keywords, articles.DbSet);
        }

        [Test]
        public void BookmarksIsCorrect(UnitOfWork sut)
        {
            var actual = sut.Bookmarks;

            var articles = Assert.IsAssignableFrom<BookmarkRepository2>(actual);
            Assert.Equal(sut.Context, articles.Context);
            Assert.Equal(sut.Context.Bookmarks, articles.DbSet);
        }

        [Test]
        public void UsersIsCorrect(UnitOfWork sut)
        {
            var actual = sut.Users;

            var articles = Assert.IsAssignableFrom<UserRepository2>(actual);
            Assert.Equal(sut.Context, articles.Context);
            Assert.Equal(sut.Context.Users, articles.DbSet);
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