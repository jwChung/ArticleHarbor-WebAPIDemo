namespace ArticleHarbor.DomainModel
{
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
    }
}