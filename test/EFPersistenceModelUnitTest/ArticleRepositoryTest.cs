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
            Article article)
        {
            try
            {
                var newArticle = await sut.InsertAsync(article);
                var expected = await sut.SelectAsync(newArticle.Id);
                newArticle.AsSource().OfLikeness<Article>().ShouldEqual(expected);
            }
            finally
            {
                transaction.Rollback();
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

        protected override IEnumerable<MemberInfo> ExceptToVerifyGuardClause()
        {
            yield return this.Methods.Select(x => x.InsertAsync(null));
        }
    }
}