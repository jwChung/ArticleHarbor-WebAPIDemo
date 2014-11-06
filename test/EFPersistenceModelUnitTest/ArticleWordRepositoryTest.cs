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
            EFDataAccess.Article article)
        {
            try
            {
                var addedArticle = sut.Context.Articles.Add(article);
                sut.Context.SaveChanges();
                var articleWord = new DomainModel.ArticleWord(addedArticle.Id, word);
                
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
            ArticleWordRepository sut,
            EFDataAccess.Article article1,
            EFDataAccess.Article article2,
            string word1,
            string word2,
            string word3)
        {
            try
            {
                var addedArticle1 = sut.Context.Articles.Add(article1);
                var addedArticle2 = sut.Context.Articles.Add(article2);
                sut.Context.SaveChanges();
                sut.Insert(new DomainModel.ArticleWord(addedArticle1.Id, word1));
                sut.Insert(new DomainModel.ArticleWord(addedArticle1.Id, word2));
                sut.Insert(new DomainModel.ArticleWord(addedArticle2.Id, word3));
                sut.Context.SaveChanges();

                sut.Delete(addedArticle1.Id);

                sut.Context.SaveChanges();
                Assert.Empty(
                    sut.Context.ArticleWords.Where(
                        x => x.ArticleId == addedArticle1.Id).ToArray());
                string actual = sut.Context.ArticleWords.Where(
                    x => x.ArticleId == addedArticle2.Id).Single().Word;
                Assert.Equal(word3, actual);
            }
            finally
            {
                transaction.Rollback();
                transaction.Dispose();
            }
        }
    }
}