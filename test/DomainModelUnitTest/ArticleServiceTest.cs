namespace DomainModelUnitTest
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using DomainModel;
    using Xunit;

    public class ArticleServiceTest : IdiomaticTest<ArticleService>
    {
        [Test]
        public void SutIsArticleService(ArticleService sut)
        {
            Assert.IsAssignableFrom<IArticleService>(sut);
        }

        [Test]
        public void GetAsyncReturnsCorrectResult(
            ArticleService sut,
            Task<IEnumerable<Article>> articles)
        {
            sut.Repository.Of(x => x.SelectAsync() == articles);
            var actual = sut.GetAsync();
            Assert.Equal(articles, actual);
        }

        [Test]
        public async Task AddOrModifyAsyncAddsWhenThereIsNoArticleWithGivenId(
            ArticleService sut,
            Article article,
            Article newArticle)
        {
            sut.Repository.ToMock().Setup(x => x.Select(article.Id)).Returns(() => null);
            sut.Repository.Of(x => x.InsertAsync(article) == Task.FromResult(newArticle));

            var actual = await sut.AddOrModifyAsync(article);

            Assert.Equal(newArticle, actual);
        }

        [Test]
        public async Task AddOrModifyAsyncModifiesWhenThereIsArticleWithGivenId(
            ArticleService sut,
            Article article,
            Article newArticle)
        {
            newArticle = newArticle.WithId(article.Id);
            sut.Repository.Of(x => x.Select(article.Id) == article);

            var actual = await sut.AddOrModifyAsync(newArticle);

            Assert.Equal(newArticle, actual);
            sut.Repository.ToMock().Verify(x => x.Update(newArticle));
        }

        [Test]
        public void RemoveCorrectlyRemoves(
            ArticleService sut,
            int id)
        {
            sut.Remove(id);
            sut.Repository.ToMock().Verify(x => x.Delete(id));
        }
    }
}