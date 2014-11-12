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

    public class KeywordServiceTest : IdiomaticTest<KeywordService>
    {
        [Test]
        public void SutIsKeywordService(KeywordService sut)
        {
            Assert.IsAssignableFrom<IKeywordService>(sut);
        }

        [Test]
        public async Task RemoveWordsAsyncCorrectlyRemovesWords(
            KeywordService sut,
            int id)
        {
            await sut.RemoveWordsAsync(id);
            sut.Keywords.ToMock().Verify(x => x.DeleteAsync(id));
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
            var sut = fixture.Create<KeywordService>();

            // Excercise outcome
            await sut.AddWordsAsync(id, subject);

            // Verify system
            foreach (var word in words)
            {
                var likeness = word.AsSource().OfLikeness<Keyword>()
                    .With(x => x.ArticleId).EqualsWhen((a, b) => b.ArticleId == id)
                    .With(x => x.Word).EqualsWhen((a, b) => b.Word == word);
                sut.Keywords.ToMock().Verify(
                    x => x.InsertAsync(It.Is<Keyword>(p => likeness.Equals(p))));
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
            var sut = fixture.Create<KeywordService>();
            sut.Articles.Of(x => x.FindAsync(id) == Task.FromResult(article));

            // Excercise outcome
            await sut.ModifyWordsAsync(id, subject);

            // Verify system
            sut.Keywords.ToMock().Verify(x => x.DeleteAsync(id));
            foreach (var word in words)
            {
                var likeness = word.AsSource().OfLikeness<Keyword>()
                    .With(x => x.ArticleId).EqualsWhen((a, b) => b.ArticleId == id)
                    .With(x => x.Word).EqualsWhen((a, b) => b.Word == word);
                sut.Keywords.ToMock().Verify(
                    x => x.InsertAsync(It.Is<Keyword>(p => likeness.Equals(p))));
            }
        }

        [Test]
        public async Task ModifyWordsAsyncDoesNotModifiesWhenSubjectIsSame(
            KeywordService sut,
            int id,
            Article article,
            string subject)
        {
            article = article.WithSubject(subject);
            sut.Articles.Of(x => x.FindAsync(id) == Task.FromResult(article));

            await sut.ModifyWordsAsync(id, subject);

            sut.Keywords.ToMock().Verify(x => x.DeleteAsync(It.IsAny<int>()), Times.Never());
            sut.Keywords.ToMock().Verify(
                x => x.InsertAsync(It.IsAny<Keyword>()), Times.Never());
        }

        [Test]
        public async Task ModifyWordsAsyncThrowsWhenThereIsNoArticleWithGivenId(
            KeywordService sut,
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