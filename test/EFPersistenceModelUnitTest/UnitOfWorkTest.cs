namespace ArticleHarbor.EFPersistenceModel
{
    using System;
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
        public void SutIsDisposable(UnitOfWork sut)
        {
            Assert.IsAssignableFrom<IDisposable>(sut);
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
        public void DisposeCorrectlyDisposesDbContext(UnitOfWork sut)
        {
            sut.Dispose();

            var e = Assert.Throws<InvalidOperationException>(
                () => sut.Context.Articles.Find(new object()));
            Assert.Contains("disposed", e.Message);
        }

        [Test]
        public void DisposeCorrectlyDisposesTransaction(UnitOfWork sut)
        {
            sut.Dispose();

            Assert.Null(sut.Transaction.UnderlyingTransaction.Connection);
        }
    }
}