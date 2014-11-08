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
        public async Task InsertAsyncDuplicateArticleDoesNotInsert(
            DbContextTransaction transaction,
            ArticleRepository sut,
            Article article)
        {
            try
            {
                var persistence = sut.Context.Articles.First();
                article = article.WithId(persistence.Id);

                var actual = await sut.InsertAsync(article);

                actual.AsSource().OfLikeness<Article>().ShouldEqual(article);
                Assert.Equal(3, sut.Context.Articles.Count());
            }
            finally
            {
                transaction.Rollback();
                transaction.Dispose();
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
        public async Task UpdateAsyncCorrectlyUpdatesArticle(
            DbContextTransaction transaction,
            ArticleRepository sut,
            Article article1,
            Article article2)
        {
            try
            {
                var insertedArticle = await sut.InsertAsync(article1);
                var modifiedArticle = article2.WithId(insertedArticle.Id);

                await sut.UpdateAsync(modifiedArticle);

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
        public void UpdateAsyncDoesNotThrowWhenThereIsNoArticleWithGivenId(
            DbContextTransaction transaction,
            ArticleRepository sut,
            Article article)
        {
            try
            {
                Assert.DoesNotThrow(() =>
                {
                    sut.UpdateAsync(article).Wait();
                    sut.Context.SaveChanges();
                });
            }
            finally
            {
                transaction.Rollback();
                transaction.Dispose();
            }
        }

        [Test]
        public async Task DeleteAsyncCorrectlyDeletesWhenThereIsArticleWithGivenId(
            DbContextTransaction transaction,
            ArticleRepository sut,
            Article article)
        {
            try
            {
                article = await sut.InsertAsync(article);
                Assert.NotNull(await sut.SelectAsync(article.Id));

                await sut.DeleteAsync(article.Id);

                await sut.Context.SaveChangesAsync();
                Assert.Null(await sut.SelectAsync(article.Id));
            }
            finally
            {
                transaction.Rollback();
                transaction.Dispose();
            }
        }

        [Test]
        public void DeleteAsyncDoesNotThrowWhenThereIsNoArticleWithGivenId(
            ArticleRepository sut,
            Article article)
        {
            Assert.DoesNotThrow(() => sut.DeleteAsync(article.Id).Wait());
        }

        [Test]
        public async Task SelectAsyncWithIdReturnsNullWhenThereIsNoArticleWithGivenId(
            ArticleRepository sut,
            int id)
        {
            var actual = await sut.SelectAsync(id);
            Assert.Null(actual);
        }
    }
}