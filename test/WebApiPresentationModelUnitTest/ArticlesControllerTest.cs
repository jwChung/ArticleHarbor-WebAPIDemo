namespace ArticleHarbor.WebApiPresentationModel
{
    using System.Collections.Generic;
    using System.Reflection;
    using System.Threading.Tasks;
    using System.Web.Http;
    using ArticleHarbor.DomainModel;
    using Moq;
    using Ploeh.Albedo.Refraction;
    using Ploeh.AutoFixture.Xunit;
    using Ploeh.SemanticComparison.Fluent;
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

        [Test]
        public void PostAsyncHasCorrectPermissionAttribute()
        {
            var attribute = this.Methods.Select(x => x.PostAsync(null))
                .GetCustomAttribute<PermissionAuthorizationFilterAttribute>();

            Assert.Equal(
                UserPermissions.CreateArticle | UserPermissions.ModifyOwnArticle,
                attribute.Permissions);
        }

        [Test]
        public async Task PostAsyncReturnsCorrectResult(
            ArticlesController sut,
            Article article,
            Article newArticle,
            string userId)
        {
            sut.User.Identity.Of(x => x.Name == userId);
            var articleWithUserId = article.WithUserId(userId);
            var likeness = articleWithUserId.AsSource().OfLikeness<Article>();
            sut.ArticleService.Of(
                x => x.SaveAsync(It.Is<Article>(p => likeness.Equals(p)))
                    == Task.FromResult(newArticle));

            var actual = await sut.PostAsync(article);

            Assert.Equal(newArticle, actual);
        }
    }
}