namespace EFPersistenceModelUnitTest
{
    using System;
    using System.Data.Common;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Linq;
    using System.Threading.Tasks;
    using DomainModel;
    using EFDataAccess;
    using EFPersistenceModel;
    using Ploeh.SemanticComparison.Fluent;
    using Xunit;

    public class ArticleWordRepositoryTest : IdiomaticTest<ArticleWordRepository>
    {
        [Test]
        public void SutIsArticleWordRepository(ArticleWordRepository sut)
        {
            Assert.IsAssignableFrom<IArticleWordRepository>(sut);
        }

        [Test]
        public void InsertCorrectlyInsertsArticleWord(
            DbContextTransaction transaction,
            ArticleWordRepository sut,
            string word)
        {
            try
            {
                var article = sut.Context.Articles.First();
                var articleWord = new DomainModel.ArticleWord(article.Id, word);
                
                sut.Insert(articleWord);

                var expected = sut.Select(articleWord.ArticleId, articleWord.Word);
                articleWord.AsSource()
                    .OfLikeness<DomainModel.ArticleWord>()
                    .ShouldEqual(expected);
            }
            finally
            {
                transaction.Rollback();
                transaction.Dispose();
            }
        }

        [Test]
        public void InsertDuplicateEntityDoesNotThrow(
            DbContextTransaction transaction,
            ArticleWordRepository sut,
            string word)
        {
            try
            {
                var article = sut.Context.Articles.First();
                sut.Insert(new DomainModel.ArticleWord(article.Id, word));

                sut.Insert(new DomainModel.ArticleWord(article.Id, word));

                Assert.DoesNotThrow(() => sut.Context.SaveChanges());
            }
            finally
            {
                transaction.Rollback();
                transaction.Dispose();
            }
        }

        [Test]
        public void SelectReturnsNullWhenThereIsNoArticleWordWithGivenIdentity(
            ArticleWordRepository sut,
            string word,
            int articleId)
        {
            var actual = sut.Select(articleId, word);
            Assert.Null(actual);
        }

        [Test]
        public void DeleteWithIdDeletesAllArticleWordsByArticleId(
            DbContextTransaction transaction,
            ArticleWordRepository sut)
        {
            try
            {
                var article = sut.Context.Articles.First();

                sut.Delete(article.Id);
                sut.Context.SaveChanges();

                Assert.Empty(
                    sut.Context.ArticleWords.Where(
                        x => x.ArticleId == article.Id).ToArray());
                Assert.Equal(2, sut.Context.ArticleWords.Count());
            }
            finally
            {
                transaction.Rollback();
                transaction.Dispose();
            }
        }
        
        [Test]
        public async Task InsertAsyncCorrectlyInsertsArticleWord(
            DbContextTransaction transaction,
            ArticleWordRepository sut,
            string word)
        {
            try
            {
                var article = sut.Context.Articles.First();
                var articleWord = new DomainModel.ArticleWord(article.Id, word);

                await sut.InsertAsync(articleWord);

                var expected = sut.Select(articleWord.ArticleId, articleWord.Word);
                articleWord.AsSource()
                    .OfLikeness<DomainModel.ArticleWord>()
                    .ShouldEqual(expected);
            }
            finally
            {
                transaction.Rollback();
                transaction.Dispose();
            }
        }

        [Test]
        public async Task InsertAsyncDuplicateEntityDoesNotThrow(
            DbContextTransaction transaction,
            ArticleWordRepository sut,
            string word)
        {
            try
            {
                var article = sut.Context.Articles.First();
                await sut.InsertAsync(new DomainModel.ArticleWord(article.Id, word));

                await sut.InsertAsync(new DomainModel.ArticleWord(article.Id, word));

                Assert.DoesNotThrow(() => sut.Context.SaveChanges());
            }
            finally
            {
                transaction.Rollback();
                transaction.Dispose();
            }
        }

        [Test]
        public async Task DeleteAsyncWithIdDeletesAllArticleWordsByArticleId(
            DbContextTransaction transaction,
            ArticleWordRepository sut)
        {
            try
            {
                var article = sut.Context.Articles.First();

                await sut.DeleteAsync(article.Id);
                sut.Context.SaveChanges();

                Assert.Empty(
                    sut.Context.ArticleWords.Where(
                        x => x.ArticleId == article.Id).ToArray());
                Assert.Equal(2, sut.Context.ArticleWords.Count());
            }
            finally
            {
                transaction.Rollback();
                transaction.Dispose();
            }
        }
    }
}