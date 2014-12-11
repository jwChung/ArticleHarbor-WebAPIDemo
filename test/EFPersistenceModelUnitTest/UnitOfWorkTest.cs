namespace ArticleHarbor.EFPersistenceModel
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using ArticleHarbor.DomainModel;
    using ArticleHarbor.EFDataAccess;
    using DomainModel.Repositories;
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
        public void RollbackTransactionAsyncCorrectlyRollbacksTransaction(
            UnitOfWork sut,
            EFDataAccess.Article article)
        {
            article.UserId = "user3";
            sut.Context.Articles.Add(article);
            sut.Context.SaveChanges();
            Assert.Equal(4, sut.Context.Articles.Count());

            sut.RollbackTransactionAsync().Wait();

            Assert.Equal(3, sut.Context.Articles.Count());
        }

        [Test]
        public void CommitTransactionAsyncCorrectlyCommitsTransaction(
            UnitOfWork sut,
            EFDataAccess.Article article)
        {
            // Fixture setup
            article.UserId = "user3";
            sut.Context.Articles.Add(article);
            
            // Exercise system
            sut.CommitTransactionAsync().Wait();

            using (var context = new ArticleHarborDbContext(
                new ArticleHarborDbContextTestInitializer()))
            {
                // Verify outcome
                Assert.Equal(4, sut.Context.Articles.Count());

                // Teardown
                article = context.Articles.Find(article.Id);
                context.Articles.Remove(article);
                context.SaveChanges();
                Assert.Equal(3, sut.Context.Articles.Count());
            }
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