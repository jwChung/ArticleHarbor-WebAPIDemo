namespace ArticleHarbor.EFPersistenceModel
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using ArticleHarbor.DomainModel;
    using Ploeh.AutoFixture;
    using Ploeh.SemanticComparison.Fluent;
    using Xunit;

    public class ArticleRepositoryTest : IdiomaticTest<ArticleRepository>
    {
        [Test]
        public void SutIsRepository(ArticleRepository sut)
        {
            Assert.IsAssignableFrom<IRepository<Article>>(sut);
        }

        [Test]
        public async Task InsertAsyncCorrectlyInsertsArticle(
            DbContextTransaction transaction,
            ArticleRepository sut,
            Article article)
        {
            try
            {
                var newArticle = await sut.InsertAsync(article.WithUserId("user1"));
                var expected = await sut.FindAsync(new object[] { newArticle.Id });
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
        public void InsertAsyncArticleWithInvalidUserIdThrows(
            ArticleRepository sut,
            Article article,
            string userId)
        {
            var e = Assert.Throws<AggregateException>(() => sut.InsertAsync(article.WithUserId(userId)).Wait());
            Assert.IsType<ArgumentException>(e.InnerException);
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
                    await sut.InsertAsync(article.WithUserId("user2"));

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
                var insertedArticle = await sut.InsertAsync(article1.WithUserId("user2"));
                var modifiedArticle = article2.WithId(insertedArticle.Id).WithUserId("user2");

                await sut.UpdateAsync(modifiedArticle);

                sut.Context.SaveChanges();
                var actual = await sut.FindAsync(new object[] { insertedArticle.Id });
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
                article = await sut.InsertAsync(article.WithUserId("user1"));
                Assert.NotNull(await sut.FindAsync(new object[] { article.Id }));

                await sut.DeleteAsync(new object[] { article.Id });

                await sut.Context.SaveChangesAsync();
                Assert.Null(await sut.FindAsync(new object[] { article.Id }));
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
            Assert.DoesNotThrow(() => sut.DeleteAsync(new object[] { article.Id }).Wait());
        }

        [Test]
        public async Task FineAsyncWithIdReturnsNullWhenThereIsNoArticleWithGivenId(
            ArticleRepository sut,
            int id)
        {
            var actual = await sut.FindAsync(new object[] { id });
            Assert.Null(actual);
        }
    }
}