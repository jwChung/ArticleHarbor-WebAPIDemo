namespace EFPersistenceModelUnitTest
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Reflection;
    using DomainModel;
    using EFDataAccess;
    using EFPersistenceModel;
    using Moq;
    using Ploeh.AutoFixture;
    using Ploeh.AutoFixture.Xunit;
    using Ploeh.SemanticComparison;
    using Ploeh.SemanticComparison.Fluent;
    using Xunit;
    using Article = DomainModel.Article;

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
                .OfLikeness<EFArticle>()
                .Without(x => x.ArticleWords)
                .Without(x => x.Id);

            sut.Insert(article);

            sut.EFArticles.ToMock().Verify(
                x => x.Add(It.Is<EFArticle>(p => likeness.Equals(p))),
                Times.Once());
        }

        [Test]
        public void InsertReturnsArticleWithId(
            ArticleRepository sut,
            Article article,
            [NoAutoProperties] EFArticle efArticle)
        {
            sut.EFArticles.ToMock()
                .Setup(x => x.Add(It.IsAny<EFArticle>())).Returns(efArticle);

            var actual = sut.Insert(article);

            Assert.NotSame(article, sut);
            actual.AsSource().OfLikeness<Article>().ShouldEqual(article.WithId(actual.Id));
        }

        [Test(RunOn.CI)]
        public void SelectReturnsCorrectArticleSet(
            IFixture fixture)
        {
            var articles = new ArticleHarborContext().Articles;
            fixture.Inject(articles);
            var sut = fixture.Create<ArticleRepository>();
            var values = articles.Take(50).ToArray();

            var actual = sut.Select().ToArray();

            for (int i = 0; i < values.Length; i++)
                values[i].AsSource().OfLikeness<Article>()
                    .ShouldEqual(actual[i]);
        }
    }
}