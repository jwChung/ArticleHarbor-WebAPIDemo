namespace EFPersistenceModelUnitTest
{
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Reflection;
    using DomainModel;
    using EFPersistenceModel;
    using Moq;
    using Ploeh.AutoFixture.Xunit;
    using Ploeh.SemanticComparison;
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
        public void InsertCorrectlyInsertsArticle(
            ArticleRepository sut,
            Article article)
        {
            var likeness = article.AsSource()
                .OfLikeness<EFDataAccess.Article>()
                .Without(x => x.ArticleWords)
                .Without(x => x.Id);

            sut.Insert(article);

            sut.Articles.ToMock().Verify(
                x => x.Add(It.Is<EFDataAccess.Article>(p => likeness.Equals(p))),
                Times.Once());
        }

        [Test]
        public void InsertReturnsArticleWithId(
            ArticleRepository sut,
            Article article,
            EFDataAccess.Article newArticle)
        {
            sut.Articles.ToMock()
                .Setup(x => x.Add(It.IsAny<EFDataAccess.Article>())).Returns(newArticle);
            
            var actual = sut.Insert(article);

            Assert.NotSame(article, sut);
            actual.AsSource().OfLikeness<Article>().ShouldEqual(article.WithId(actual.Id));
        }
    }
}