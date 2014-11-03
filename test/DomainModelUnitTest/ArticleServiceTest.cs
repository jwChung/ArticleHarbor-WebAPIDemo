namespace DomainModelUnitTest
{
    using DomainModel;

    public class ArticleServiceTest : IdiomaticTest<ArticleService>
    {
        [Test]
        public void AddCorrectlyAddsArticle(
            ArticleService sut,
            Article article)
        {
            sut.Add(article);
            sut.Repository.ToMock().Verify(x => x.Insert(article));
        }
    }
}