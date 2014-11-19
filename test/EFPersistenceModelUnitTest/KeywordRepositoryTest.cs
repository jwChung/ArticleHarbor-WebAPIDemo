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

    public class KeywordRepositoryTest : IdiomaticTest<KeywordRepository>
    {
        [Test]
        public void SutIsKeywordRepository(KeywordRepository sut)
        {
            Assert.IsAssignableFrom<IKeywordRepository>(sut);
        }

        [Test]
        public async Task FindAsyncReturnsNullWhenThereIsNoKeywordWithGivenIdentity(
            KeywordRepository sut,
            string word,
            int articleId)
        {
            var actual = await sut.FindAsync(articleId, word);
            Assert.Null(actual);
        }

        [Test]
        public async Task FindAsyncIsCaseInsensitive(
            DbContextTransaction transaction,
            KeywordRepository sut)
        {
            try
            {
                await sut.InsertAsync(new Keyword(1, "ABC"));
                var actual = await sut.FindAsync(1, "abc");
                Assert.NotNull(actual);
            }
            finally
            {
                transaction.Rollback();
                transaction.Dispose();
            }
        }

        [Test]
        public async Task InsertAsyncCorrectlyInsertsKeyword(
            DbContextTransaction transaction,
            KeywordRepository sut,
            string word)
        {
            try
            {
                var article = sut.Context.Articles.First();
                var keyword = new Keyword(article.Id, word);

                Keyword actual = await sut.InsertAsync(keyword);

                var expected = await sut.FindAsync(keyword.ArticleId, keyword.Word);
                keyword.AsSource().OfLikeness<Keyword>().ShouldEqual(expected);
                keyword.AsSource().OfLikeness<Keyword>().ShouldEqual(actual);
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
            KeywordRepository sut,
            string word)
        {
            try
            {
                var article = sut.Context.Articles.First();
                await sut.InsertAsync(new Keyword(article.Id, word));

                await sut.InsertAsync(new Keyword(article.Id, word));

                Assert.DoesNotThrow(() => sut.Context.SaveChanges());
            }
            finally
            {
                transaction.Rollback();
                transaction.Dispose();
            }
        }

        [Test]
        public async Task DeleteAsyncWithIdDeletesAllKeywordsByArticleId(
            DbContextTransaction transaction,
            KeywordRepository sut)
        {
            try
            {
                var article = sut.Context.Articles.First();

                await sut.DeleteAsync(article.Id);
                sut.Context.SaveChanges();

                Assert.Empty(
                    sut.Context.Keywords.Where(
                        x => x.ArticleId == article.Id).ToArray());
                Assert.Equal(2, sut.Context.Keywords.Count());
            }
            finally
            {
                transaction.Rollback();
                transaction.Dispose();
            }
        }
    }
}