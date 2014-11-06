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
    using Article = EFDataAccess.Article;
    using ArticleWord = DomainModel.ArticleWord;

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
            Article article)
        {
            try
            {
                var articleWord = new ArticleWord(1, word);
                sut.Context.Articles.Add(article);

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
        public void DeleteWithIdDeletesAllArticleWordsByArticleId(
            DbContextTransaction transaction,
            ArticleWordRepository sut,
            Article article1,
            Article article2,
            string word1,
            string word2,
            string word3)
        {
            try
            {
                sut.Context.Articles.Add(article1);
                sut.Context.Articles.Add(article2);
                sut.Insert(new ArticleWord(1, word1));
                sut.Insert(new ArticleWord(1, word2));
                sut.Insert(new ArticleWord(2, word3));
                sut.Context.SaveChanges();

                sut.Delete(1);
                sut.Context.SaveChanges();

                Assert.Empty(
                    sut.Context.ArticleWords.Where(x => x.ArticleId == 1).ToArray());
                Assert.Equal(
                    word3,
                    sut.Context.ArticleWords.Where(x => x.ArticleId == 2).Single().Word);
            }
            finally
            {
                transaction.Rollback();
                transaction.Dispose();
            }
        }
    }
}