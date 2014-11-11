namespace ArticleHarbor.WebApiPresentationModel.Controllers
{
    using System.Collections.Generic;
    using System.Reflection;
    using System.Threading.Tasks;
    using System.Web.Http;
    using DomainModel;
    using Models;
    using Moq;
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
        public async Task PostAsyncCorrectlyAddsArticle(
            ArticlesController sut,
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

        [Test]
        public void PostAsyncHasAuthorizeAttribute()
        {
            var method = this.Methods.Select(x => x.PostAsync(null));
            Assert.NotNull(method.GetCustomAttribute<AuthorizeAttribute>());
        }

        [Test]
        public async Task PutAsyncCorrectlyModifiesArticle(
            ArticlesController sut,
            PutArticleViewModel putArticle,
            string actor,
            string userId)
        {
            // Fixture setup
            sut.User.Identity.Of(x => x.Name == actor);
            sut.ArticleService.Of(x => x.GetUserIdAsync(putArticle.Id) == Task.FromResult(userId));

            var articleLikeness = putArticle.AsSource().OfLikeness<Article>()
                .With(x => x.UserId).EqualsWhen((p, a) => a.UserId == userId);

            // Exercise system
            await sut.PutAsync(putArticle);

            // Verify outcome
            sut.ArticleService.ToMock().Verify(
                x => x.ModifyAsync(actor, It.Is<Article>(p => articleLikeness.Equals(p))));
        }

        [Test]
        public void PutAsyncHasAuthorizeAttribute()
        {
            var method = this.Methods.Select(x => x.PutAsync(null));
            Assert.NotNull(method.GetCustomAttribute<AuthorizeAttribute>());
        }

        [Test]
        public async Task DeleteAsyncCorrectlyRemovesArticle(
            ArticlesController sut,
            string actor,
            int id)
        {
            sut.User.Identity.Of(x => x.Name == actor);
            await sut.DeleteAsync(id);
            sut.ArticleService.ToMock().Verify(x => x.RemoveAsync(actor, id));
        }

        [Test]
        public void DeleteAsyncHasAuthorizeAttribute()
        {
            var method = this.Methods.Select(x => x.DeleteAsync(0));
            Assert.NotNull(method.GetCustomAttribute<AuthorizeAttribute>());
        }
    }
}