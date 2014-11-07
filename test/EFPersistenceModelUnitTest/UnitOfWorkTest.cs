namespace EFPersistenceModelUnitTest
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
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
        public void DisposeCorrectlyDisposesEFContext(
            Mock<ArticleHarborDbContext> context,
            IFixture fixture)
        {
            fixture.Inject(context.Object);
            var sut = fixture.Create<UnitOfWork>();

            sut.Dispose();
            sut.Dispose();

            context.Protected().Verify("Dispose", Times.Once(), true);
        }

        [Test]
        public void DisposeAsynchronouslySavesChanges(
            Mock<ArticleHarborDbContext> context,
            IFixture fixture)
        {
            fixture.Inject(context.Object);
            var sut = fixture.Create<UnitOfWork>();

            sut.Dispose();

            context.Verify(x => x.SaveChangesAsync(), Times.Once());
        }

        [Test(RunOn.Local)]
        public void DisposeThrowsUnhandledExceptionWhenSaveChangesAsyncThrows(
            TssArticleHarborDbContext context,
            IFixture fixture)
        {
            var throws = false;
            UnhandledExceptionEventHandler handler = (s, e) => throws = true;
            try
            {
                AppDomain.CurrentDomain.UnhandledException += handler;
                fixture.Inject<ArticleHarborDbContext>(context);
                var sut = fixture.Create<UnitOfWork>();

                sut.Dispose();

                Thread.Sleep(150);
                Assert.True(throws);
            }
            finally
            {
                AppDomain.CurrentDomain.UnhandledException -= handler;
            }
        }

        [Test]
        public void DisposeShouldSaveChangesBeforeDisposing(
            TssArticleHarborDbContext2 context,
            IFixture fixture)
        {
            fixture.Inject<ArticleHarborDbContext>(context);
            var sut = fixture.Create<UnitOfWork>();

            sut.Dispose();

            Thread.Sleep(150);
            Assert.Equal(new[] { 1, 2 }, context.Order.ToArray());
        }

        protected override IEnumerable<MemberInfo> ExceptToVerifyInitialization()
        {
            yield return this.Properties.Select(x => x.Articles);
            yield return this.Properties.Select(x => x.ArticleWords);
        }

        public class TssArticleHarborDbContext : ArticleHarborDbContext
        {
            public TssArticleHarborDbContext()
                : base(new NullDatabaseInitializer<ArticleHarborDbContext>())
            {
            }

            public override Task<int> SaveChangesAsync()
            {
                return Task.Run(new Func<int>(() => { throw new Exception(); }));
            }
        }

        public class TssArticleHarborDbContext2 : ArticleHarborDbContext
        {
            private readonly IList<int> order = new List<int>();

            public TssArticleHarborDbContext2()
                : base(new NullDatabaseInitializer<ArticleHarborDbContext>())
            {
            }

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