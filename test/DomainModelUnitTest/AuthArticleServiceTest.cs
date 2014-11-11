namespace ArticleHarbor.DomainModel
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
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
            sut.AuthService.Of(x => x.HasPermissionsAsync(article.UserId, Permissions.Create)
                == Task.FromResult(false));
            try
            {
                await sut.AddAsync(article);
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
    }
}