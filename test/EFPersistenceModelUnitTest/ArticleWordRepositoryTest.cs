namespace EFPersistenceModelUnitTest
{
    using System;
    using System.Data.Common;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Linq;
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
            string word,
            EFArticle efArticle)
        {
            try
            {
                var articleWord = new ArticleWord(word, 1);
                sut.Context.Articles.Add(efArticle);

                sut.Insert(articleWord);
                sut.Context.SaveChanges();

                var expected = sut.Select(articleWord.Word, articleWord.ArticleId);
                articleWord.AsSource()
                    .OfLikeness<ArticleWord>()
                    .ShouldEqual(expected);
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
            var actual = sut.Select(word, articleId);
            Assert.Null(actual);
        }

        [Test]
        public void DeleteWithIdDeletesAllArticleWordsRelatedWithId(
            DbContextTransaction transaction,
            ArticleWordRepository sut,
            EFArticle efArticle1,
            EFArticle efArticle2,
            string word1,
            string word2,
            string word3)
        {
            try
            {
                sut.Context.Articles.Add(efArticle1);
                sut.Context.Articles.Add(efArticle2);
                sut.Insert(new ArticleWord(word1, 1));
                sut.Insert(new ArticleWord(word2, 1));
                sut.Insert(new ArticleWord(word3, 2));
                sut.Context.SaveChanges();

                sut.Delete(1);
                sut.Context.SaveChanges();

                Assert.Empty(
                    sut.Context.ArticleWords.Where(x => x.EFArticleId == 1).ToArray());
                Assert.Equal(
                    word3,
                    sut.Context.ArticleWords.Where(x => x.EFArticleId == 2).Single().Word);
            }
            finally
            {
                transaction.Rollback();
                transaction.Dispose();
            }
        }
    }
}