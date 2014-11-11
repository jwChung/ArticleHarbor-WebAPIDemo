namespace ArticleHarbor.DomainModel
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;
    using Ploeh.AutoFixture;
    using Ploeh.AutoFixture.Xunit;
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
            [Frozen(As = typeof(IRepository<Article>))] FakeArticleRepository articles,
            NewArticleService sut)
        {
            IEnumerable<Article> actual = sut.GetAsync().Result;
            Assert.Equal(articles.Items, actual);
        }

        [Test]
        public async Task GetUserIdAsyncReturnsCorrectUserId(
            [Frozen(As = typeof(IRepository<Article>))] FakeArticleRepository articles,
            NewArticleService sut)
        {
            var article = articles.Items[1];
            var id = article.Id;

            var actual = await sut.GetUserIdAsync(id);

            Assert.Equal(article.UserId, actual);
        }

        [Test]
        public void GetIncorrectUserIdAsyncThrows(
            [Frozen(As = typeof(IRepository<Article>))] FakeArticleRepository articles,
            NewArticleService sut,
            Generator<Article> generator)
        {
            var article = generator.First(x => !articles.Items.Select(i => i.Id).Contains(x.Id));
            var id = article.Id;

            var e = Assert.Throws<AggregateException>(() => sut.GetUserIdAsync(id).Wait());
            Assert.IsType<ArgumentException>(e.InnerException);
        }

        [Test]
        public async Task AddAsyncCorrectlyAddsArticle(
            [Frozen(As = typeof(IRepository<Article>))] FakeArticleRepository articles,
            NewArticleService sut,
            Article article)
        {
            var actual = await sut.AddAsync(article);
            Assert.Contains(actual, articles.Items);
            Assert.Equal(4, articles.Items.Count);
        }

        [Test]
        public async Task AddAsyncCorrectlyAddsArticleWords(
            NewArticleService sut,
            Article article)
        {
            await sut.AddAsync(article);
            sut.ArticleWordService.ToMock().Verify(
                x => x.AddWordsAsync(article.Id, article.Subject));
        }

        [Test]
        public async Task ModifyAsyncCorrectlyModifiesArticle(
            [Frozen(As = typeof(IRepository<Article>))] FakeArticleRepository articles,
            Article article,
            NewArticleService sut)
        {
            article = article.WithId(articles.Items[1].Id);

            await sut.ModifyAsync(null, article);

            Assert.Contains(article, articles.Items);
            Assert.Equal(3, articles.Items.Count);
        }

        [Test]
        public async Task ModifyAsyncCorrectlyModifiesArticleWords(
            [Frozen(As = typeof(IRepository<Article>))] FakeArticleRepository articles,
            Article article,
            NewArticleService sut)
        {
            await sut.ModifyAsync(null, article);
            sut.ArticleWordService.ToMock().Verify(
                x => x.ModifyWordsAsync(article.Id, article.Subject));
        }

        [Test]
        public async Task RemoveAsyncCorrectlyRemovesArticle(
            [Frozen(As = typeof(IRepository<Article>))] FakeArticleRepository articles,
            NewArticleService sut)
        {
            var id = articles.Items[1].Id;

            await sut.RemoveAsync(null, id);

            Assert.DoesNotContain(id, articles.Items.Select(x => x.Id));
            Assert.Equal(2, articles.Items.Count);
        }

        [Test]
        public async Task RemoveAsyncCorrectlyRemovesArticleWords(
            NewArticleService sut,
            int id)
        {
            await sut.RemoveAsync(null, id);
            sut.ArticleWordService.ToMock().Verify(
                x => x.RemoveWordsAsync(id));
        }

        [Test]
        public void ModifyAsyncWithNullArticleThrows(NewArticleService sut)
        {
            Assert.Throws<ArgumentNullException>(() => sut.ModifyAsync(null, null));
        }

        protected override IEnumerable<MemberInfo> ExceptToVerifyGuardClause()
        {
            yield return this.Methods.Select(x => x.ModifyAsync(null, null));
            yield return this.Methods.Select(x => x.RemoveAsync(null, 0));
        }

        public class FakeArticleRepository : FakeRepositoryBase<Article>
        {
            public FakeArticleRepository(Generator<Article> generator)
                : base(generator)
            {
            }

            public override object[] GetIdentity(Article item)
            {
                return new object[] { item.Id };
            }
        }
    }
}