namespace EFPersistenceModelUnitTest
{
    using System.Data.Common;
    using System.Data.Entity;
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
    }
}