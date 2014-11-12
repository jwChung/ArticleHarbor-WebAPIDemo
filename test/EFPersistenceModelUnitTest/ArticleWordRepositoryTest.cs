namespace ArticleHarbor.EFPersistenceModel
{
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using ArticleHarbor.DomainModel;
    using DomainModel.Models;
    using DomainModel.Repositories;
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
        public async Task FindAsyncReturnsNullWhenThereIsNoArticleWordWithGivenIdentity(
            ArticleWordRepository sut,
            string word,
            int articleId)
        {
            var actual = await sut.FindAsync(articleId, word);
            Assert.Null(actual);
        }

        [Test]
        public async Task FindAsyncIsCaseInsensitive(
            ArticleWordRepository sut)
        {
            await sut.InsertAsync(new ArticleWord(1, "ABC"));
            var actual = await sut.FindAsync(1, "abc");
            Assert.NotNull(actual);
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
                var articleWord = new ArticleWord(article.Id, word);

                ArticleWord actual = await sut.InsertAsync(articleWord);

                var expected = await sut.FindAsync(articleWord.ArticleId, articleWord.Word);
                articleWord.AsSource().OfLikeness<ArticleWord>().ShouldEqual(expected);
                articleWord.AsSource().OfLikeness<ArticleWord>().ShouldEqual(actual);
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
                await sut.InsertAsync(new ArticleWord(article.Id, word));

                await sut.InsertAsync(new ArticleWord(article.Id, word));

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