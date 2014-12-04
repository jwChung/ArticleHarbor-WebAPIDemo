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

    public class RelayKeywordCommandTest : IdiomaticTest<RelayKeywordCommand>
    {
        [Test]
        public void SutIsModelCommand(RelayKeywordCommand sut)
        {
            Assert.IsAssignableFrom<ModelCommand<IEnumerable<IModel>>>(sut);
        }

        [Test]
        public void ValueIsCorrect(RelayKeywordCommand sut, IEnumerable<IModel> models)
        {
            sut.InnerCommand.Of(x => x.Value == models);
            var actual = sut.Value;
            Assert.Equal(models, actual);
        }

        [Test]
        public void ExecuteAsyncArticleCorrectlyRelaysKeywords(
            Article article,
            IEnumerable<string> words,
            IModelCommand<IEnumerable<IModel>> newInnerCommand,
            IFixture fixture)
        {
            fixture.Inject<Func<string, IEnumerable<string>>>(x =>
            {
                Assert.Equal(article.Subject, x);
                return words;
            });
            var sut = fixture.Create<RelayKeywordCommand>();
            var keywords = words.Select(
                w => new Keyword(article.Id, w).AsSource().OfLikeness<Keyword>().CreateProxy());
            sut.InnerCommand.Of(x => x.ExecuteAsync(keywords) == Task.FromResult(newInnerCommand));

            var actual = sut.ExecuteAsync(article).Result;

            var relayKeywordCommand = Assert.IsAssignableFrom<RelayKeywordCommand>(actual);
            Assert.Equal(newInnerCommand, relayKeywordCommand.InnerCommand);
        }

        protected override IEnumerable<MemberInfo> ExceptToVerifyInitialization()
        {
            yield return this.Properties.Select(x => x.Value);
        }
    }
}