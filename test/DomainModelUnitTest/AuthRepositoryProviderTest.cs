namespace ArticleHarbor.DomainModel
{
    using Xunit;

    public abstract class AuthRepositoryProviderTest<T> : IdiomaticTest<AuthRepositoryProvider<T>>
    {
        [Test]
        public void SutIsAuthRepositoryProvider(AuthRepositoryProvider<T> sut)
        {
            Assert.IsAssignableFrom<IAuthRepositoryProvider<Article>>(sut);
        }
    }

    public class AuthArticleRepositoryProviderTest : AuthRepositoryProviderTest<Article>
    {
    }
}