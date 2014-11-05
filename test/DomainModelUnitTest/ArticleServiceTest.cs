namespace DomainModelUnitTest
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using DomainModel;
    using Xunit;

    public class ArticleServiceTest : IdiomaticTest<ArticleService>
    {
        [Test]
        public void SutIsArticleService(ArticleService sut)
        {
            Assert.IsAssignableFrom<IArticleService>(sut);
        }

        [Test]
        public void GetAsyncReturnsCorrectResult(
            ArticleService sut,
            Task<IEnumerable<Article>> articles)
        {
            sut.Repository.Of(x => x.SelectAsync() == articles);
            var actual = sut.GetAsync();
            Assert.Equal(articles, actual);
        }
    }
}