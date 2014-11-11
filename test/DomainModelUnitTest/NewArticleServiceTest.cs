namespace ArticleHarbor.DomainModel
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Ploeh.AutoFixture;
    using Xunit;

    public class NewArticleServiceTest : IdiomaticTest<NewArticleService>
    {
        [Test]
        public void SutIsArticleService(NewArticleService sut)
        {
            Assert.IsAssignableFrom<IArticleService>(sut);
        }

        [Test]
        public void GetAsyncReturnsCorrectResult(
            FakeRepositoryBase<Article> articles,
            NewArticleService sut)
        {
            IEnumerable<Article> actual = sut.GetAsync().Result;
            Assert.Equal(articles.Items, actual);
        }
    }
}