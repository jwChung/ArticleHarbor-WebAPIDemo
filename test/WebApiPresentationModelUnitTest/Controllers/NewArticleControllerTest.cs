namespace ArticleHarbor.WebApiPresentationModel.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Web.Http;
    using DomainModel;
    using Xunit;

    public class NewArticleControllerTest : IdiomaticTest<NewArticleController>
    {
        [Test]
        public void SutIsApiController(NewArticleController sut)
        {
            Assert.IsAssignableFrom<ApiController>(sut);
        }

        [Test]
        public async Task GetAsyncReturnsCorrectResult(
            NewArticleController sut,
            IEnumerable<Article> articles)
        {
            sut.ArticleService.Of(x => x.GetAsync() == Task.FromResult(articles));
            var actual = await sut.GetAsync();
            Assert.Equal(articles, actual);
        }
    }
}