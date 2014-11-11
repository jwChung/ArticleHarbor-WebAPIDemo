namespace ArticleHarbor.DomainModel
{
    using Xunit;

    public class AuthArticleServiceTest : IdiomaticTest<AuthArticleService>
    {
        [Test]
        public void SutIsArticleService(AuthArticleService sut)
        {
            Assert.IsAssignableFrom<IArticleService>(sut);
        }
    }
}