namespace ArticleHarbor.DomainModel.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;
    using Ploeh.AutoFixture;
    using Ploeh.SemanticComparison.Fluent;
    using Xunit;

    public class RelayKeywordsCommandTest : IdiomaticTest<RelayKeywordsCommand>
    {
        [Test]
        public void SutIsModelCommand(RelayKeywordsCommand sut)
        {
            Assert.IsAssignableFrom<ModelCommand<IEnumerable<IModel>>>(sut);
        }

        [Test]
        public void ValueIsFromInnerCommand(RelayKeywordsCommand sut)
        {
            var expected = sut.InnerCommand.Value;
            var actual = sut.Value;
            Assert.Equal(expected, actual);
        }

        [Test]
        public void ExecuteAsyncArticleCorrectlyRelaysKeywords(
            Article article,
            IEnumerable<string> words,
            IModelCommand<IEnumerable<IModel>> newInnerCommand,
            IFixture fixture)
        {
            // Fixture setup
            fixture.Inject<Func<string, IEnumerable<string>>>(x =>
            {
                Assert.Equal(article.Subject, x);
                return words;
            });

            var sut = fixture.Create<RelayKeywordsCommand>();

            var keywords = words.Select(
                w => new Keyword(article.Id, w).AsSource().OfLikeness<Keyword>().CreateProxy());

            sut.InnerCommand.Of(x => x.ExecuteAsync(keywords) == Task.FromResult(newInnerCommand));

            // Exercise system
            var actual = sut.ExecuteAsync(article).Result;

            // Verify outcome
            var relayKeywordCommand = Assert.IsAssignableFrom<RelayKeywordsCommand>(actual);
            Assert.Equal(newInnerCommand, relayKeywordCommand.InnerCommand);
        }

        protected override IEnumerable<MemberInfo> ExceptToVerifyInitialization()
        {
            yield return this.Properties.Select(x => x.Value);
        }
    }
}