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

        [Test]
        public async Task SaveAsyncAddsWhenThereIsNoArticleWithGivenId(
            FakeRepositoryBase<Article> articles,
            NewArticleService sut,
            Generator<Article> generator)
        {
            var count = articles.Items.Count;
            var article = generator.First(x => !articles.Items.Contains(x));

            var actual = await sut.SaveAsync(article);

            Assert.Equal(article, actual);
            Assert.Equal(count + 1, articles.Items.Count);
            Assert.Contains(article, articles.Items);
        }

        [Test]
        public async Task SaveAsyncModifiesWhenThereIsArticleWithGivenId(
            FakeRepositoryBase<Article> articles,
            NewArticleService sut,
            Article article)
        {
            var count = articles.Items.Count;
            article = article.WithId(articles.Items[1].Id);
            
            var actual = await sut.SaveAsync(article);

            Assert.Equal(article, actual);
            Assert.Equal(count, articles.Items.Count);
            Assert.Contains(article, articles.Items);
        }

        [Test]
        public async Task SaveAsyncRenewsArticleWordsWhenInsertingArticle(
            FakeRepositoryBase<Article> articles,
            NewArticleService sut,
            Generator<Article> generator)
        {
            var article = generator.First(x => !articles.Items.Contains(x));
            await sut.SaveAsync(article);
            sut.ArticleWordService.ToMock().Verify(x => x.RenewAsync(article.Id, article.Subject));
        }

        [Test]
        public async Task SaveAsyncRenewsArticleWordsWhenModifyingArticle(
            FakeRepositoryBase<Article> articles,
            NewArticleService sut,
            Article article)
        {
            article = article.WithId(articles.Items[1].Id);
            await sut.SaveAsync(article);
            sut.ArticleWordService.ToMock().Verify(x => x.RenewAsync(article.Id, article.Subject));
        }
    }
}