﻿namespace ArticleHarbor.WebApiPresentationModel.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;
    using System.Web.Http;
    using DomainModel;
    using DomainModel.Models;
    using DomainModel.Repositories;
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
            sut.Repository.Of(x => x.SelectAsync() == Task.FromResult(articles));
            var actual = await sut.GetAsync();
            Assert.Equal(articles, actual);
        }

        [Test]
        public async Task PostAsyncResturnsCorrectResult(
            ArticlesController sut,
            PostArticleViewModel postArticle,
            string userId,
            Article article,
            IEnumerable<IModel> models,
            IEnumerable<Keyword> keywords)
        {
            // Fixture setup
            sut.User.Identity.Of(x => x.Name == userId);

            var articleLikeness = postArticle.AsSource().OfLikeness<Article>()
                .With(x => x.Id).EqualsWhen((p, a) => a.Id == -1)
                .With(x => x.UserId).EqualsWhen((p, a) => a.UserId == userId);

            models = new IModel[] { article }.Concat(keywords).Concat(models);
            models = models.OrderBy(x => x.GetHashCode());
            sut.InsertCommand.Of(
                x => x.ExecuteAsync(It.Is<Article>(p => articleLikeness.Equals(p)))
                    == Task.FromResult(Mock.Of<IModelCommand<IEnumerable<IModel>>>(c => c.Value == models)));

            // Exercise system
            var actual = await sut.PostAsync2(postArticle);

            // Verify outcome
            var articleDetail = Assert.IsAssignableFrom<ArticleDetailViewModel>(actual);
            Assert.Equal(article, articleDetail.Article);
            Assert.Equal(keywords.Count(), articleDetail.Keywords.Count());
            Assert.Empty(articleDetail.Keywords.Except(keywords));
        }

        [Test]
        public void PostAsyncHasAuthorizeAttribute()
        {
            var method = this.Methods.Select(x => x.PostAsync2(null));
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