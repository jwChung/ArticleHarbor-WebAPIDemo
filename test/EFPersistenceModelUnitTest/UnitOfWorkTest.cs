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
        public void ArticleWordsIsCorrect(UnitOfWork sut)
        {
            Assert.NotNull(sut.Context.ArticleWords);

            var actual = sut.ArticleWords;

            var repository = Assert.IsAssignableFrom<ArticleWordRepository>(actual);
            Assert.Same(sut.Context, repository.Context);
        }

        [Test]
        public void ArticleWordsAlwaysReturnsSameInstance(UnitOfWork sut)
        {
            var actual = sut.ArticleWords;
            Assert.Same(sut.ArticleWords, actual);
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
            yield return this.Properties.Select(x => x.ArticleWords);
            yield return this.Properties.Select(x => x.Users);
        }
    }
}