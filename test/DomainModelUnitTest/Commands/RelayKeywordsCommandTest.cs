namespace ArticleHarbor.DomainModel.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Models;
    using Ploeh.AutoFixture;
    using Ploeh.SemanticComparison.Fluent;
    using Xunit;

    public class RelayKeywordsCommandTest : IdiomaticTest<RelayKeywordsCommand>
    {
        [Test]
        public void SutIsModelCommand(RelayKeywordsCommand sut)
        {
            Assert.IsAssignableFrom<ModelCommand<IModel>>(sut);
        }

        [Test]
        public void ExecuteAsyncArticleCorrectlyRelaysKeywords(
            Article article,
            string[] words,
            IEnumerable<IModel> values,
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

            sut.InnerCommand.Of(x => x.ExecuteAsync(keywords) == Task.FromResult(values));

            // Exercise system
            var actual = sut.ExecuteAsync(article).Result;

            // Verify outcome
            Assert.Equal(values, actual);
        }
    }
}