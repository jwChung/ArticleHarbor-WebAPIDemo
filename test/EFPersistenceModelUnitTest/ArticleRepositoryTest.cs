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
                var expected = sut.Select(newArticle.Id);
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
        public void SelectWithIdReturnsNullWhenThereIsNoArticleWithGivenId(
            ArticleRepository sut,
            int id)
        {
            var actual = sut.Select(id);
            Assert.Null(actual);
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
                var actual = sut.Select(insertedArticle.Id);
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

        [Test]
        public async Task DeleteCorrectlyDeletesWhenThereIsArticleWithGivenId(
            DbContextTransaction transaction,
            ArticleRepository sut,
            Article article)
        {
            try
            {
                article = await sut.InsertAsync(article);
                Assert.NotNull(sut.Select(article.Id));

                sut.Delete(article.Id);

                await sut.Context.SaveChangesAsync();
                Assert.Null(sut.Select(article.Id));
            }
            finally
            {
                transaction.Rollback();
                transaction.Dispose();
            }
        }

        [Test]
        public void DeleteDoesNotThrowWhenThereIsNoArticleWithGivenId(
            ArticleRepository sut,
            Article article)
        {
            Assert.DoesNotThrow(() => sut.Delete(article.Id));
        }
    }
}