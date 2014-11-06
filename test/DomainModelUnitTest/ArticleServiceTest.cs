namespace DomainModelUnitTest
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using DomainModel;
    using Moq;
    using Ploeh.AutoFixture;
    using Ploeh.SemanticComparison.Fluent;
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
            sut.Articles.Of(x => x.SelectAsync() == articles);
            var actual = sut.GetAsync();
            Assert.Equal(articles, actual);
        }

        [Test]
        public async Task AddOrModifyAsyncAddsWhenThereIsNoArticleWithGivenId(
            ArticleService sut,
            Article article,
            Article newArticle)
        {
            sut.Articles.ToMock().Setup(x => x.Select(article.Id)).Returns(() => null);
            sut.Articles.Of(x => x.InsertAsync(article) == Task.FromResult(newArticle));

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
            sut.Articles.Of(x => x.Select(article.Id) == article);

            var actual = await sut.AddOrModifyAsync(newArticle);

            Assert.Equal(newArticle, actual);
            sut.Articles.ToMock().Verify(x => x.Update(newArticle));
        }

        [Test]
        public async Task AddOrModifyAddsArticleWordsWhenAddingArticle(
            Article article,
            Article newArticle,
            string[] words,
            IFixture fixture)
        {
            // Fixture setup
            fixture.Inject<Func<string, IEnumerable<string>>>(
                x =>
                {
                    Assert.Equal(newArticle.Subject, x);
                    return words;
                });
            var sut = fixture.Create<ArticleService>();
            sut.Articles.ToMock().Setup(x => x.Select(article.Id)).Returns(() => null);
            sut.Articles.Of(x => x.InsertAsync(article) == Task.FromResult(newArticle));

            // Exercise system
            await sut.AddOrModifyAsync(article);

            // Verify outcome
            foreach (var word in words)
            {
                var likeness = new ArticleWord(word, newArticle.Id)
                    .AsSource().OfLikeness<ArticleWord>();
                sut.ArticleWords.ToMock().Verify(
                    x => x.Insert(It.Is<ArticleWord>(p => likeness.Equals(p))));
            }
        }

        [Test]
        public void AddOrModifyAsyncWithNullArticleThrows(
            ArticleService sut)
        {
            var e = Assert.Throws<AggregateException>(() => sut.AddOrModifyAsync(null).Result);
            Assert.IsType<ArgumentNullException>(e.InnerException);
        }

        [Test]
        public void RemoveCorrectlyRemoves(
            ArticleService sut,
            int id)
        {
            sut.Remove(id);
            sut.Articles.ToMock().Verify(x => x.Delete(id));
        }

        protected override IEnumerable<System.Reflection.MemberInfo> ExceptToVerifyGuardClause()
        {
            yield return this.Methods.Select(x => x.AddOrModifyAsync(null));
        }
    }
}