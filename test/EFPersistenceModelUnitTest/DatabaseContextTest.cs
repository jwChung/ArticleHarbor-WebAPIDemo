namespace EFPersistenceModelUnitTest
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Threading;
    using System.Threading.Tasks;
    using DomainModel;
    using EFDataAccess;
    using EFPersistenceModel;
    using Moq;
    using Moq.Protected;
    using Ploeh.AutoFixture;
    using Xunit;

    public class DatabaseContextTest : IdiomaticTest<DatabaseContext>
    {
        [Test]
        public void SutIsDatabaseContext(DatabaseContext sut)
        {
            Assert.IsAssignableFrom<IDatabaseContext>(sut);
        }

        [Test]
        public void ArticlesIsCorrect(DatabaseContext sut)
        {
            Assert.NotNull(sut.Context.Articles);

            var actual = sut.Articles;

            var repository = Assert.IsAssignableFrom<ArticleRepository>(sut.Articles);
            Assert.Same(sut.Context.Articles, repository.EFArticles);
        }

        [Test]
        public void ArticlesAlwaysReturnsSameInstance(DatabaseContext sut)
        {
            var actual = sut.Articles;
            Assert.Same(sut.Articles, actual);
        }

        [Test]
        public void DisposeCorrectlyDisposesEFContext(
            Mock<ArticleHarborContext> context,
            IFixture fixture)
        {
            fixture.Inject(context.Object);
            var sut = fixture.Create<DatabaseContext>();

            sut.Dispose();
            sut.Dispose();

            context.Protected().Verify("Dispose", Times.Once(), true);
        }

        [Test]
        public void DisposeAsynchronouslySavesChanges(
            Mock<ArticleHarborContext> context,
            IFixture fixture)
        {
            fixture.Inject(context.Object);
            var sut = fixture.Create<DatabaseContext>();

            sut.Dispose();

            context.Verify(x => x.SaveChangesAsync(), Times.Once());
        }

        [Test]
        public void DisposeThrowsUnhandledExceptionWhenSaveChangesAsyncThrows(
            TssArticleHarborContext context,
            IFixture fixture)
        {
            var throws = false;
            UnhandledExceptionEventHandler handler = (s, e) => throws = true;
            try
            {
                AppDomain.CurrentDomain.UnhandledException += handler;
                fixture.Inject<ArticleHarborContext>(context);
                var sut = fixture.Create<DatabaseContext>();

                sut.Dispose();

                Thread.Sleep(100);
                Assert.True(throws);
            }
            finally
            {
                AppDomain.CurrentDomain.UnhandledException -= handler;
            }
        }

        [Test]
        public void DisposeShouldSaveChangesBeforeDisposing(
            TssArticleHarborContext2 context,
            IFixture fixture)
        {
            fixture.Inject<ArticleHarborContext>(context);
            var sut = fixture.Create<DatabaseContext>();

            sut.Dispose();

            Thread.Sleep(150);
            Assert.Equal(new[] { 1, 2 }, context.Order.ToArray());
        }

        protected override IEnumerable<MemberInfo> ExceptToVerifyInitialization()
        {
            yield return this.Properties.Select(x => x.Articles);
        }

        public class TssArticleHarborContext : ArticleHarborContext
        {
            public override Task<int> SaveChangesAsync()
            {
                return Task.Run(new Func<int>(() => { throw new Exception(); }));
            }
        }

        public class TssArticleHarborContext2 : ArticleHarborContext
        {
            private readonly IList<int> order = new List<int>();

            public IList<int> Order
            {
                get { return this.order; }
            }

            public override Task<int> SaveChangesAsync()
            {
                return Task.Run(() =>
                {
                    Thread.Sleep(100);
                    this.order.Add(1);
                    return 0;
                });
            }

            protected override void Dispose(bool disposing)
            {
                this.order.Add(2);
            }
        }
    }
}