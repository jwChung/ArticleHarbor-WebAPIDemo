namespace ArticleHarbor.EFPersistenceModel
{
    using System;
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

            Assert.Equal("user1", actual.UserId);
            article.AsSource().OfLikeness<Article>().Without(x => x.UserId).ShouldEqual(actual);
        }

        [Test]
        public void ConvertToModelAsyncWithIncorrectIdThrows(
            ArticleRepository2 sut,
            EFDataAccess.Article article)
        {
            article.Id = -1;
            var e = Assert.Throws<AggregateException>(
                () => sut.ConvertToModelAsync(article).Result);
            Assert.IsType<ArgumentException>(e.InnerException);
        }

        [Test]
        public void ConvertToPersistenceAsyncReturnsCorrectPersistence(
            ArticleHarborDbContext context,
            ArticleRepository2 sut,
            Article article)
        {
            var user = context.UserManager.FindByNameAsync("user1").Result;
            article = article.WithUserId("user1");

            var actual = sut.ConvertToPersistenceAsync(article).Result;

            Assert.Equal(user.Id, actual.UserId);
            actual.AsSource().OfLikeness<Article>().Without(x => x.UserId).ShouldEqual(article);
        }

        [Test]
        public void ConvertToPersistenceAsyncWithIncorrectIdThrows(
            ArticleRepository2 sut,
            Article article)
        {
            var e = Assert.Throws<AggregateException>(
                () => sut.ConvertToPersistenceAsync(article).Result);
            Assert.IsType<ArgumentException>(e.InnerException);
        }
    }
}