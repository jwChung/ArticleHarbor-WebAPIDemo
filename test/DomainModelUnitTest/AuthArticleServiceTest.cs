namespace ArticleHarbor.DomainModel
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Threading.Tasks;
    using Moq;
    using Xunit;

    public class AuthArticleServiceTest : IdiomaticTest<AuthArticleService>
    {
        [Test]
        public void SutIsArticleService(AuthArticleService sut)
        {
            Assert.IsAssignableFrom<IArticleService>(sut);
        }

        [Test]
        public async Task GetAsyncReturnsCorrctResult(
            AuthArticleService sut,
            IEnumerable<Article> articles)
        {
            sut.InnerService.Of(x => x.GetAsync() == Task.FromResult(articles));
            var actual = await sut.GetAsync();
            Assert.Equal(articles, actual);
        }

        [Test]
        public async Task AddAsyncThrowsWhenUnauthorized(
            AuthArticleService sut,
            Article article)
        {
            // Fixture setup
            sut.AuthService.Of(x => x.HasPermissionsAsync(article.UserId, Permissions.Create)
                == Task.FromResult(false));
            try
            {
                // Excercise system
                await sut.AddAsync(article);

                // Verify outcome
                Assert.True(false, "throw exception");
            }
            catch (UnauthorizedException)
            {
                return;
            }
        }

        [Test]
        public async Task AddAsyncCorrectlyAddsArticle(
            AuthArticleService sut,
            Article article)
        {
            sut.AuthService.Of(x => x.HasPermissionsAsync(article.UserId, Permissions.Create)
                == Task.FromResult(true));
            await sut.AddAsync(article);
            sut.InnerService.ToMock().Verify(x => x.AddAsync(article));
        }

        [Test]
        public async Task ModifyAsyncThrowsWhenUnauthorized(
            AuthArticleService sut,
            string actor,
            Article article)
        {
            // Fixture setup
            sut.AuthService.Of(x => x.HasPermissionsAsync(actor, Permissions.Modify)
                == Task.FromResult(false));
            try
            {
                // Excercise system
                await sut.ModifyAsync(actor, article);

                // Verify outcome
                Assert.True(false, "throw exception");
            }
            catch (UnauthorizedException)
            {
                return;
            }
        }

        [Test]
        public async Task ModifyAsyncWithIncorrectActorThrows(
            AuthArticleService sut,
            string actor,
            Article article)
        {
            // Fixture setup
            sut.AuthService.Of(x => x.HasPermissionsAsync(actor, Permissions.Modify)
                == Task.FromResult(true));
            sut.AuthService.Of(x => x.HasPermissionsAsync(actor, Permissions.ModifyAny)
                == Task.FromResult(false));
            try
            {
                // Exercise system
                await sut.ModifyAsync(actor, article);

                // Verify outcome
                Assert.True(false, "throw exception");
            }
            catch (UnauthorizedException)
            {
                return;
            }
        }

        [Test]
        public async Task ModifyAsyncWithCorrectActorCorrectlyModifiesArticle(
            AuthArticleService sut,
            string actor,
            Article article)
        {
            sut.AuthService.Of(x => x.HasPermissionsAsync(actor, Permissions.Modify)
                == Task.FromResult(true));
            sut.AuthService.Of(x => x.HasPermissionsAsync(actor, Permissions.ModifyAny)
                == Task.FromResult(false));
            article = article.WithUserId(actor);

            await sut.ModifyAsync(actor, article);

            sut.InnerService.ToMock().Verify(x => x.ModifyAsync(actor, article), Times.Once());
        }

        [Test]
        public async Task ModifyAsyncWithincorrectActorAndModifyAnyPermissionCorrectlyModifiesArticle(
            AuthArticleService sut,
            string actor,
            Article article)
        {
            sut.AuthService.Of(x => x.HasPermissionsAsync(actor, Permissions.Modify)
                == Task.FromResult(true));
            sut.AuthService.Of(x => x.HasPermissionsAsync(actor, Permissions.ModifyAny)
                == Task.FromResult(true));
            
            await sut.ModifyAsync(actor, article);

            sut.InnerService.ToMock().Verify(x => x.ModifyAsync(actor, article), Times.Once());
        }
    }
}