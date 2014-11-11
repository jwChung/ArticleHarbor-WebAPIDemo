namespace ArticleHarbor.WebApiPresentationModel.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Web.Http;
    using DomainModel;
    using Models;
    using Moq;
    using Ploeh.SemanticComparison.Fluent;
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

        [Test]
        public async Task PostAsyncReturnsCorrectResult(
            NewArticleController sut,
            PostArticleViewModel postArticle,
            string userId,
            Article expected)
        {
            // Fixture setup
            sut.User.Identity.Of(x => x.Name == userId);

            var articleLikeness = postArticle.AsSource().OfLikeness<Article>()
                .With(x => x.Id).EqualsWhen((p, a) => a.Id == -1)
                .With(x => x.UserId).EqualsWhen((p, a) => a.UserId == userId);

            sut.ArticleService.Of(
                x => x.AddAsync(It.Is<Article>(p => articleLikeness.Equals(p)))
                    == Task.FromResult(expected));

            // Exercise system
            var actual = await sut.PostAsync(postArticle);

            // Verify outcome
            Assert.Equal(expected, actual);
        }
    }
}