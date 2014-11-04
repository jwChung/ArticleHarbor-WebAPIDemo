namespace WebApiPresentationModel
{
    using System.Collections.Generic;
    using System.Web.Http;
    using DomainModel;
    using Xunit;

    public class ArticlesControllerTest : IdiomaticTest<ArticlesController>
    {
        [Test]
        public void SutIsApiController(ArticlesController sut)
        {
            Assert.IsAssignableFrom<ApiController>(sut);
        }

        [Test]
        public void GetReturnsCorrectResult(ArticlesController sut, IEnumerable<Article> articles)
        {
            sut.Repository.Of(x => x.Select() == articles);
            var actual = sut.Get();
            Assert.Equal(articles, actual);
        }
    }
}