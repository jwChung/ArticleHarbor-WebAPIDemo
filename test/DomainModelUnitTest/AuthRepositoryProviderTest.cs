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

    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "To declare generic class")]
    public class AuthArticleRepositoryProviderTest : AuthRepositoryProviderTest<Article>
    {
    }
}