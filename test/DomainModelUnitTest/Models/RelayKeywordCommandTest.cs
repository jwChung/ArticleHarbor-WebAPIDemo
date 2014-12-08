namespace ArticleHarbor.DomainModel.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Moq;
    using Ploeh.AutoFixture;
    using Ploeh.SemanticComparison.Fluent;
    using Xunit;

    public class RelayKeywordCommandTest : IdiomaticTest<RelayKeywordCommand>
    {
        [Test]
        public void SutIsModelCommand(RelayKeywordCommand sut)
        {
            Assert.IsAssignableFrom<ModelCommand<IEnumerable<IModel>>>(sut);
        }

        [Test]
        public void ExecuteAsyncArticleCorrectlyRelaysKeywords(
            Article article,
            IEnumerable<string> words,
            IEnumerable<IModel> innerCommandValue,
            IFixture fixture)
        {
            // Fixture setup
            fixture.Inject<Func<string, IEnumerable<string>>>(x =>
            {
                Assert.Equal(article.Subject, x);
                return words;
            });

            var sut = fixture.Create<RelayKeywordCommand>();

            var keywords = words.Select(
                w => new Keyword(article.Id, w).AsSource().OfLikeness<Keyword>().CreateProxy());

            sut.InnerCommand.Of(x => x.ExecuteAsync(keywords) == Task.FromResult(
                Mock.Of<IModelCommand<IEnumerable<IModel>>>(c => c.Value == innerCommandValue)));

            var expected = sut.Value.Concat(innerCommandValue);

            // Exercise system
            var actual = sut.ExecuteAsync(article).Result;

            // Verify outcome
            var relayKeywordCommand = Assert.IsAssignableFrom<RelayKeywordCommand>(actual);
            Assert.Equal(sut.InnerCommand, relayKeywordCommand.InnerCommand);
            this.AssertEquivalent(expected, relayKeywordCommand.Value);
        }

        private void AssertEquivalent<T>(IEnumerable<T> expected, IEnumerable<T> actual)
        {
            Assert.Equal(expected.Count(), actual.Count());
            Assert.Empty(expected.Except(actual));
        }
    }
}