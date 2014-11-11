namespace ArticleHarbor.DomainModel.Services
{
    using System.Collections.Generic;
    using System.Reflection;
    using System.Threading.Tasks;
    using Models;
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
        public async Task GetUserIdAsyncReturnsCorrectUserId(
            AuthArticleService sut,
            int id,
            string userId)
        {
            sut.InnerService.Of(x => x.GetUserIdAsync(id) == Task.FromResult(userId));
            var actual = await sut.GetUserIdAsync(id);
            Assert.Equal(userId, actual);
        }

        [Test]
        public async Task GetAsyncReturnsCorrectResult(
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
        public async Task ModifyAsyncWithIncorrectActorAndModifyAnyPermissionCorrectlyModifiesArticle(
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

        [Test]
        public async Task RemoveAsyncThrowsWhenUnauthorized(
            AuthArticleService sut,
            string actor,
            int id)
        {
            // Fixture setup
            sut.AuthService.Of(x => x.HasPermissionsAsync(actor, Permissions.Delete)
                == Task.FromResult(false));
            try
            {
                // Excercise system
                await sut.RemoveAsync(actor, id);

                // Verify outcome
                Assert.True(false, "throw exception");
            }
            catch (UnauthorizedException)
            {
                return;
            }
        }

        [Test]
        public async Task RemoveAsyncWithIncorrectActorThrows(
            AuthArticleService sut,
            string actor,
            int id,
            string userId)
        {
            // Fixture setup
            sut.AuthService.Of(x => x.HasPermissionsAsync(actor, Permissions.Delete)
                == Task.FromResult(true));
            sut.AuthService.Of(x => x.HasPermissionsAsync(actor, Permissions.DeleteAny)
                == Task.FromResult(false));
            sut.InnerService.Of(x => x.GetUserIdAsync(id) == Task.FromResult(userId));

            try
            {
                // Excercise system
                await sut.RemoveAsync(actor, id);

                // Verify outcome
                Assert.True(false, "throw exception");
            }
            catch (UnauthorizedException)
            {
                return;
            }
        }

        [Test]
        public async Task RemoveAsyncWithCorrectActorRemovesArticle(
            AuthArticleService sut,
            string actor,
            int id)
        {
            sut.AuthService.Of(x => x.HasPermissionsAsync(actor, Permissions.Delete)
                == Task.FromResult(true));
            sut.AuthService.Of(x => x.HasPermissionsAsync(actor, Permissions.DeleteAny)
                == Task.FromResult(false));
            sut.InnerService.Of(x => x.GetUserIdAsync(id) == Task.FromResult(actor));

            await sut.RemoveAsync(actor, id);

            sut.InnerService.ToMock().Verify(x => x.RemoveAsync(actor, id), Times.Once());
        }

        [Test]
        public async Task RemoveAsyncWithIncorrectActorAndDeleteAnyPermissionRemovesArticle(
            AuthArticleService sut,
            string actor,
            int id)
        {
            sut.AuthService.Of(x => x.HasPermissionsAsync(actor, Permissions.Delete)
                == Task.FromResult(true));
            sut.AuthService.Of(x => x.HasPermissionsAsync(actor, Permissions.DeleteAny)
                == Task.FromResult(true));

            await sut.RemoveAsync(actor, id);

            sut.InnerService.ToMock().Verify(x => x.RemoveAsync(actor, id), Times.Once());
        }

        protected override IEnumerable<MemberInfo> ExceptToVerifyGuardClause()
        {
            yield return this.Methods.Select(x => x.RemoveAsync(null, 0));
        }
    }
}