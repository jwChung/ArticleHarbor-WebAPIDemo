namespace ArticleHarbor.DomainModel.Services
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Threading.Tasks;
    using Models;
    using Moq;
    using Ploeh.AutoFixture;
    using Ploeh.SemanticComparison.Fluent;
    using Xunit;

    public class ArticleWordServiceTest : IdiomaticTest<ArticleWordService>
    {
        [Test]
        public void SutIsArticleWordService(ArticleWordService sut)
        {
            Assert.IsAssignableFrom<IArticleWordService>(sut);
        }

        [Test]
        public async Task RemoveWordsAsyncCorrectlyRemovesWords(
            ArticleWordService sut,
            int id)
        {
            await sut.RemoveWordsAsync(id);
            sut.ArticleWords.ToMock().Verify(x => x.DeleteAsync(id));
        }

        [Test]
        public async Task AddWordsAsyncCorrectlyAddsWords(
            int id,
            string subject,
            string[] words,
            IFixture fixture)
        {
            // Fixture setup
            fixture.Inject<Func<string, IEnumerable<string>>>(s =>
            {
                Assert.Equal(subject, s);
                return words;
            });
            var sut = fixture.Create<ArticleWordService>();

            // Excercise outcome
            await sut.AddWordsAsync(id, subject);

            // Verify system
            foreach (var word in words)
            {
                var likeness = word.AsSource().OfLikeness<ArticleWord>()
                    .With(x => x.ArticleId).EqualsWhen((a, b) => b.ArticleId == id)
                    .With(x => x.Word).EqualsWhen((a, b) => b.Word == word);
                sut.ArticleWords.ToMock().Verify(
                    x => x.InsertAsync(It.Is<ArticleWord>(p => likeness.Equals(p))));
            }
        }

        [Test]
        public async Task ModifyWordsAsyncCorrectlyModifiesWords(
            int id,
            string subject,
            string[] words,
            IFixture fixture,
            Article article)
        {
            // Fixture setup
            fixture.Inject<Func<string, IEnumerable<string>>>(s =>
            {
                Assert.Equal(subject, s);
                return words;
            });
            var sut = fixture.Create<ArticleWordService>();
            sut.Articles.Of(x => x.FindAsync(id) == Task.FromResult(article));

            // Excercise outcome
            await sut.ModifyWordsAsync(id, subject);

            // Verify system
            sut.ArticleWords.ToMock().Verify(x => x.DeleteAsync(id));
            foreach (var word in words)
            {
                var likeness = word.AsSource().OfLikeness<ArticleWord>()
                    .With(x => x.ArticleId).EqualsWhen((a, b) => b.ArticleId == id)
                    .With(x => x.Word).EqualsWhen((a, b) => b.Word == word);
                sut.ArticleWords.ToMock().Verify(
                    x => x.InsertAsync(It.Is<ArticleWord>(p => likeness.Equals(p))));
            }
        }

        [Test]
        public async Task ModifyWordsAsyncDoesNotModifiesWhenSubjectIsSame(
            ArticleWordService sut,
            int id,
            Article article,
            string subject)
        {
            article = article.WithSubject(subject);
            sut.Articles.Of(x => x.FindAsync(id) == Task.FromResult(article));

            await sut.ModifyWordsAsync(id, subject);

            sut.ArticleWords.ToMock().Verify(x => x.DeleteAsync(It.IsAny<int>()), Times.Never());
            sut.ArticleWords.ToMock().Verify(
                x => x.InsertAsync(It.IsAny<ArticleWord>()), Times.Never());
        }

        [Test]
        public async Task ModifyWordsAsyncThrowsWhenThereIsNoArticleWithGivenId(
            ArticleWordService sut,
            int id,
            Article article,
            string subject)
        {
            article = article.WithSubject(subject);
            try
            {
                await sut.ModifyWordsAsync(id, null);
                throw new Exception("throw exception.");
            }
            catch (ArgumentException)
            {
            }
        }

        protected override IEnumerable<MemberInfo> ExceptToVerifyGuardClause()
        {
            yield return this.Methods.Select(x => x.AddWordsAsync(0, null));
            yield return this.Methods.Select(x => x.ModifyWordsAsync(0, null));
        }
    }
}