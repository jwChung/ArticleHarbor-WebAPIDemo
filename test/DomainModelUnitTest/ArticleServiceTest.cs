namespace ArticleHarbor.DomainModel
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;
    using Ploeh.AutoFixture;
    using Ploeh.AutoFixture.Xunit;
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
            [Frozen(As = typeof(IArticleRepository))] FakeRepository articles,
            ArticleService sut)
        {
            IEnumerable<Article> actual = sut.GetAsync().Result;
            Assert.Equal(articles, actual);
        }

        [Test]
        public async Task GetUserIdAsyncReturnsCorrectUserId(
            [Frozen(As = typeof(IArticleRepository))] FakeRepository articles,
            ArticleService sut)
        {
            var article = articles.ElementAt(1);
            var id = article.Id;

            var actual = await sut.GetUserIdAsync(id);

            Assert.Equal(article.UserId, actual);
        }

        [Test]
        public void GetIncorrectUserIdAsyncThrows(
            [Frozen(As = typeof(IArticleRepository))] FakeRepository articles,
            ArticleService sut)
        {
            var article = articles.New();

            var e = Assert.Throws<AggregateException>(() => sut.GetUserIdAsync(article.Id).Wait());
            Assert.IsType<ArgumentException>(e.InnerException);
        }

        [Test]
        public async Task AddAsyncCorrectlyAddsArticle(
            [Frozen(As = typeof(IArticleRepository))] FakeRepository articles,
            ArticleService sut,
            Article article)
        {
            var actual = await sut.AddAsync(article);
            Assert.Contains(actual, articles);
            Assert.Equal(4, articles.Count());
        }

        [Test]
        public async Task AddAsyncCorrectlyAddsArticleWords(
            ArticleService sut,
            Article article)
        {
            await sut.AddAsync(article);
            sut.ArticleWordService.ToMock().Verify(
                x => x.AddWordsAsync(article.Id, article.Subject));
        }

        [Test]
        public async Task ModifyAsyncCorrectlyModifiesArticle(
            [Frozen(As = typeof(IArticleRepository))] FakeRepository articles,
            Article article,
            ArticleService sut)
        {
            article = article.WithId(articles.ElementAt(1).Id);

            await sut.ModifyAsync(null, article);

            Assert.Contains(article, articles);
            Assert.Equal(3, articles.Count());
        }

        [Test]
        public async Task ModifyAsyncCorrectlyModifiesArticleWords(
            [Frozen(As = typeof(IArticleRepository))] FakeRepository articles,
            Article article,
            ArticleService sut)
        {
            await sut.ModifyAsync(null, article);
            sut.ArticleWordService.ToMock().Verify(
                x => x.ModifyWordsAsync(article.Id, article.Subject));
        }

        [Test]
        public async Task RemoveAsyncCorrectlyRemovesArticle(
            [Frozen(As = typeof(IArticleRepository))] FakeRepository articles,
            ArticleService sut)
        {
            var id = articles.ElementAt(1).Id;

            await sut.RemoveAsync(null, id);

            Assert.DoesNotContain(id, articles.Select(x => x.Id));
            Assert.Equal(2, articles.Count());
        }

        [Test]
        public async Task RemoveAsyncCorrectlyRemovesArticleWords(
            ArticleService sut,
            int id)
        {
            await sut.RemoveAsync(null, id);
            sut.ArticleWordService.ToMock().Verify(
                x => x.RemoveWordsAsync(id));
        }

        [Test]
        public void ModifyAsyncWithNullArticleThrows(ArticleService sut)
        {
            Assert.Throws<ArgumentNullException>(() => sut.ModifyAsync(null, null));
        }

        protected override IEnumerable<MemberInfo> ExceptToVerifyGuardClause()
        {
            yield return this.Methods.Select(x => x.ModifyAsync(null, null));
            yield return this.Methods.Select(x => x.RemoveAsync(null, 0));
        }

        public class FakeRepository : FakeRepositoryBase<int, Article>, IArticleRepository
        {
            public FakeRepository(Generator<Article> generator) : base(generator)
            {
            }

            protected override int GetKeyForItem(Article item)
            {
                return item.Id;
            }
        }
    }
}