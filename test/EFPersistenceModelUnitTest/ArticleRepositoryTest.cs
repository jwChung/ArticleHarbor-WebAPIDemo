namespace EFPersistenceModelUnitTest
{
    using System.Linq;
    using System.Threading.Tasks;
    using DomainModel;
    using EFPersistenceModel;
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
        public async Task InsertCorrectlyInsertsArticle(
            ArticleRepository sut,
            Article article)
        {
            var newArticle = sut.Insert(article);
            var expected = await sut.SelectAsync(newArticle.Id);
            newArticle.AsSource().OfLikeness<Article>().ShouldEqual(expected);
        }

        [Test]
        public async Task SelectAsyncReturnsCorrectResult(
            ArticleRepository sut,
            Article[] articles)
        {
            var actual = await sut.SelectAsync();
            Assert.Equal(50, actual.Count());
        }
    }
}