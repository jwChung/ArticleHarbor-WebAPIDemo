namespace EFPersistenceModelUnitTest
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;
    using DomainModel;
    using EFPersistenceModel;
    using Ploeh.AutoFixture;
    using Ploeh.SemanticComparison.Fluent;
    using Xunit;

    public class ArticleRepositoryTest : IdiomaticTest<ArticleRepository>
    {
        [Test]
        public void SutIsArticleRepository(ArticleRepository sut)
        {
            Assert.IsAssignableFrom<IArticleRepository>(sut);
        }

        [Test]
        public async Task InsertAsyncCorrectlyInsertsArticle(
            DbContextTransaction transaction,
            ArticleRepository sut,
            Article article1)
        {
            try
            {
                var newArticle = await sut.InsertAsync(article1);
                var expected = await sut.SelectAsync(newArticle.Id);
                newArticle.AsSource().OfLikeness<Article>().ShouldEqual(expected);
            }
            finally
            {
                transaction.Rollback();
                transaction.Dispose();
            }
        }

        [Test]
        public async Task InsertAsyncWithNullArticleThrows(ArticleRepository sut)
        {
            try
            {
                await sut.InsertAsync(null);
                throw new Exception();
            }
            catch (ArgumentNullException)
            {
            }
            catch
            {
                throw;
            }
        }

        [Test]
        public async Task SelectAsyncReturnsCorrectResult(
            DbContextTransaction transaction,
            ArticleRepository sut,
            Generator<Article> articles)
        {
            try
            {
                foreach (var article in articles.Take(60))
                    await sut.InsertAsync(article);

                var actual = await sut.SelectAsync();

                Assert.Equal(50, actual.Count());
            }
            finally
            {
                transaction.Rollback();
                transaction.Dispose();
            }
        }

        [Test]
        public async Task UpdateCorrectlyUpdatesArticle(
            DbContextTransaction transaction,
            ArticleRepository sut,
            Article article1,
            Article article2)
        {
            try
            {
                var insertedArticle = await sut.InsertAsync(article1);
                var modifiedArticle = article2.WithId(insertedArticle.Id);

                sut.Update(modifiedArticle);

                sut.Context.SaveChanges();
                var actual = await sut.SelectAsync(insertedArticle.Id);
                actual.AsSource().OfLikeness<Article>().ShouldEqual(modifiedArticle);
            }
            finally
            {
                transaction.Rollback();
                transaction.Dispose();
            }
        }

        [Test]
        public void UpdateDoesNotThrowWhenThereIsNoArticleWithGivenId(
            DbContextTransaction transaction,
            ArticleRepository sut,
            Article article)
        {
            try
            {
                Assert.DoesNotThrow(() =>
                {
                    sut.Update(article);
                    sut.Context.SaveChanges();
                });
            }
            finally
            {
                transaction.Rollback();
                transaction.Dispose();
            }
        }

        protected override IEnumerable<MemberInfo> ExceptToVerifyGuardClause()
        {
            yield return this.Methods.Select(x => x.InsertAsync(null));
        }
    }
}