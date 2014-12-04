namespace ArticleHarbor.DomainModel.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
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
        public void ValueIsCorrect(RelayKeywordCommand sut, IEnumerable<IModel> models)
        {
            sut.InnerCommand.Of(x => x.Value == models);
            var actual = sut.Value;
            Assert.Equal(models, actual);
        }

        [Test]
        public void ExecuteAsyncArticleCorrectlyRelaysKeywords(
            Article article,
            Article insertedArticle,
            IEnumerable<string> words,
            IModelCommand<IEnumerable<IModel>> innerCommand1,
            IModelCommand<IEnumerable<IModel>> innerCommand2,
            IFixture fixture)
        {
            // Fixture setup
            fixture.Inject<Func<string, IEnumerable<string>>>(x =>
            {
                Assert.Equal(insertedArticle.Subject, x);
                return words;
            });
            
            var sut = fixture.Create<RelayKeywordCommand>();

            sut.InnerCommand.Of(x => x.ExecuteAsync(article) == Task.FromResult(innerCommand1));
            innerCommand1.Of(x => x.Value == new IModel[] { insertedArticle });

            var keywords = words.Select(
                w => new Keyword(insertedArticle.Id, w).AsSource().OfLikeness<Keyword>().CreateProxy());
            innerCommand1.Of(x => x.ExecuteAsync(keywords) == Task.FromResult(innerCommand2));

            // Exercise system
            var actual = sut.ExecuteAsync(article).Result;

            // Verify outcome
            var relayKeywordCommand = Assert.IsAssignableFrom<RelayKeywordCommand>(actual);
            Assert.Equal(innerCommand2, relayKeywordCommand.InnerCommand);
        }

        [Test]
        public void ExecuteAsyncArticleRelaysKeywordsOnlyOnce(
            Article article,
            IModelCommand<IEnumerable<IModel>> innerCommand,
            RelayKeywordCommand sut)
        {
            sut.InnerCommand.Of(x => x.ExecuteAsync(article) == Task.FromResult(innerCommand));
            innerCommand.Of(x => x.Value == new IModel[] { article }
                && x.ExecuteAsync(It.IsAny<IEnumerable<Keyword>>()) == Task.FromResult(innerCommand));
            
            sut.ExecuteAsync(article).Wait();

            innerCommand.ToMock().Verify(
                x => x.ExecuteAsync(It.Is<IEnumerable<Keyword>>(p => p.GetType().IsArray)));
        }

        protected override IEnumerable<MemberInfo> ExceptToVerifyInitialization()
        {
            yield return this.Properties.Select(x => x.Value);
        }
    }
}