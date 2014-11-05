namespace EFPersistenceModelUnitTest
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;
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

    public class ArticleRepository2Test : IdiomaticTest<ArticleRepository2>
    {
        [Test]
        public void SutIsArticleRepository(ArticleRepository2 sut)
        {
            Assert.IsAssignableFrom<IArticleRepository>(sut);
        }

        [Test]
        public void InsertCorrectlyInsertsArticle(
            ArticleRepository2 sut,
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
            ArticleRepository2 sut,
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
        public async Task SelectAsyncReturnsCorrectArticleSet(
            IFixture fixture)
        {
            var articles = new ArticleHarborContext().Articles;
            fixture.Inject(articles);
            var sut = fixture.Create<ArticleRepository2>();
            var values = articles.Take(50).ToArray();

            var actual = (await sut.SelectAsync()).ToArray();

            for (int i = 0; i < values.Length; i++)
                values[i].AsSource().OfLikeness<Article>()
                    .ShouldEqual(actual[i]);
        }

        [Test]
        public async Task SelectAsyncWithIdReturnsCorrectResult(
            ArticleRepository2 sut,
            int id,
            IFixture fixture)
        {
            var efArticle = fixture.Build<EFArticle>().Without(x => x.ArticleWords).Create();
            var likeness = efArticle.AsSource().OfLikeness<Article>();
            sut.EFArticles.ToMock().Setup(x => x.Find(new object[] { id })).Returns(efArticle);

            var actual = await sut.SelectAsync(id);

            likeness.ShouldEqual(actual);
        }

        [Test]
        public async Task SelectAsyncCanReturnNull(
            ArticleRepository2 sut,
            int id,
            IFixture fixture)
        {
            sut.EFArticles.ToMock().Setup(x => x.Find(new object[] { id })).Returns(() => null);
            var actual = await sut.SelectAsync(id);
            Assert.Null(actual);
        }
    }
}