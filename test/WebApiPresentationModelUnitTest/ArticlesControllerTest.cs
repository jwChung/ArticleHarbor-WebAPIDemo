namespace WebApiPresentationModelUnitTest
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Web.Http;
    using DomainModel;
    using Ploeh.AutoFixture.Xunit;
    using WebApiPresentationModel;
    using Xunit;

    public class ArticlesControllerTest : IdiomaticTest<ArticlesController>
    {
        [Test]
        public void SutIsApiController(ArticlesController sut)
        {
            Assert.IsAssignableFrom<ApiController>(sut);
        }

        [Test]
        public async Task GetAsyncReturnsCorrectResult(
            ArticlesController sut,
            IEnumerable<Article> articles)
        {
            sut.ArticleService.Of(x => x.GetAsync() == Task.FromResult(articles));
            var actual = await sut.GetAsync();
            Assert.Equal(articles, actual);
        }
    }
}