namespace ArticleHarbor.EFPersistenceModel
{
    using ArticleHarbor.DomainModel.Models;
    using EFDataAccess;
    using Ploeh.SemanticComparison.Fluent;
    using Xunit;
    using Article = DomainModel.Models.Article;

    public class ArticleRepository2Test : IdiomaticTest<ArticleRepository2>
    {
        [Test]
        public void SutIsRepository(ArticleRepository2 sut)
        {
            Assert.IsAssignableFrom<Repository<Keys<int>, Article, EFDataAccess.Article>>(sut);
        }

        [Test]
        public void ConvertToModelAsyncReturnsCorrectModel(
            ArticleHarborDbContext context,
            ArticleRepository2 sut)
        {
            var article = context.Articles.Find(1);
            var actual = sut.ConvertToModelAsync(article).Result;
            article.AsSource().OfLikeness<Article>().ShouldEqual(actual);
        }

        [Test]
        public void ConvertToPersistenceAsyncReturnsCorrectPersistence(
            ArticleRepository2 sut,
            Article article)
        {
            var actual = sut.ConvertToPersistenceAsync(article).Result;
            actual.AsSource().OfLikeness<Article>().ShouldEqual(article);
        }
    }
}