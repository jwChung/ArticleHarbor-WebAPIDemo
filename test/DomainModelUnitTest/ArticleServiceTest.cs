namespace DomainModelUnitTest
{
    using DomainModel;
    using Ploeh.AutoFixture.Xunit;

    public class ArticleServiceTest : IdiomaticTest<ArticleService>
    {
        [Test]
        public void AddCorrectlyAddsArticle(
            [Frozen] IArticleRepository repository,
            ArticleService sut,
            Article article)
        {
            sut.Add(article);

            repository.ToMock().Verify(x => x.Insert(article));
        }
    }
}