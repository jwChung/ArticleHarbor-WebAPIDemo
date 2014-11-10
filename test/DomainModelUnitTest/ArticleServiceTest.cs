namespace ArticleHarbor.DomainModel
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Jwc.Experiment.Xunit;
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
        public async Task SaveAsyncAddsWhenThereIsNoArticleWithGivenId(
            ArticleService sut,
            Article article,
            Article newArticle)
        {
            sut.Articles.ToMock().Setup(x => x.FineAsync(article.Id)).Returns(Task.FromResult<Article>(null));
            sut.Articles.Of(x => x.InsertAsync(article) == Task.FromResult(newArticle));

            var actual = await sut.SaveAsync(article);

            Assert.Equal(newArticle, actual);
        }

        [Test]
        public async Task SaveAsyncModifiesWhenThereIsArticleWithGivenId(
            ArticleService sut,
            Article article,
            Article newArticle)
        {
            newArticle = newArticle.WithId(article.Id);
            sut.Articles.Of(x => x.FineAsync(article.Id) == Task.FromResult(article));

            var actual = await sut.SaveAsync(newArticle);

            Assert.Equal(newArticle, actual);
            sut.Articles.ToMock().Verify(x => x.UpdateAsync(newArticle));
        }

        [Test]
        public IEnumerable<ITestCase> SaveAsyncRenewsArticleWordsWhenSubjectIsModifiedWithGivenId(
            Article article,
            string subject,
            string[] words,
            IFixture fixture)
        {
            // Fixture setup
            var modifiedArticle = article.WithSubject(subject);
            fixture.Inject<Func<string, IEnumerable<string>>>(
               x =>
               {
                   Assert.Equal(subject, x);
                   return words;
               });
            var sut = fixture.Create<ArticleService>();
            sut.Articles.Of(x => x.FineAsync(article.Id) == Task.FromResult(article));

            // Verify outcome
            sut.SaveAsync(modifiedArticle).Wait();

            // Excercise system
            yield return TestCase.Create(() =>
                sut.ArticleWords.ToMock().Verify(x => x.DeleteAsync(modifiedArticle.Id)));

            yield return TestCase.Create(() =>
            {
                foreach (var word in words)
                {
                    var likeness = new ArticleWord(modifiedArticle.Id, word)
                        .AsSource().OfLikeness<ArticleWord>();
                    sut.ArticleWords.ToMock().Verify(
                        x => x.InsertAsync(It.Is<ArticleWord>(p => likeness.Equals(p))));
                }
            });
        }

        [Test]
        public async Task SaveAsyncDoesNotDeleteArticleWordsWhenSubjectIsNotModifiedWithGivenId(
            ArticleService sut,
            Article article,
            Article modifiedArticle)
        {
            modifiedArticle = modifiedArticle.WithSubject(article.Subject);
            sut.Articles.Of(x => x.FineAsync(article.Id) == Task.FromResult(article));

            await sut.SaveAsync(modifiedArticle);

            sut.ArticleWords.ToMock().Verify(x => x.DeleteAsync(modifiedArticle.Id), Times.Never());
            sut.ArticleWords.ToMock().Verify(x => x.InsertAsync(It.IsAny<ArticleWord>()), Times.Never());
        }

        [Test]
        public async Task SaveAsyncAddsArticleWordsWhenAddingArticle(
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
            sut.Articles.ToMock().Setup(x => x.FineAsync(article.Id))
                .Returns(Task.FromResult<Article>(null));
            sut.Articles.Of(x => x.InsertAsync(article) == Task.FromResult(newArticle));

            // Exercise system
            await sut.SaveAsync(article);

            // Verify outcome
            foreach (var word in words)
            {
                var likeness = new ArticleWord(newArticle.Id, word)
                    .AsSource().OfLikeness<ArticleWord>();
                sut.ArticleWords.ToMock().Verify(
                    x => x.InsertAsync(It.Is<ArticleWord>(p => likeness.Equals(p))));
            }
        }

        [Test]
        public async Task RemoveAsyncCorrectlyRemoves(
            ArticleService sut,
            int id)
        {
            await sut.RemoveAsync(id);
            sut.Articles.ToMock().Verify(x => x.DeleteAsync(id));
        }
    }
}