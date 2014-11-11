namespace ArticleHarbor.DomainModel
{
    using System.Threading.Tasks;
    using Xunit;

    public class ArticleWordServiceTest : IdiomaticTest<ArticleWordService>
    {
        [Test]
        public void SutIsArticleWordService(ArticleWordService sut)
        {
            Assert.IsAssignableFrom<IArticleWordService>(sut);
        }

        [Test]
        public async Task RemoveAsyncCorrectlyRemovesWords(
            ArticleWordService sut, int id)
        {
            await sut.RemoveWordsAsync(id);
            sut.ArticleWords.ToMock().Verify(x => x.DeleteAsync(id));
        }
    }
}