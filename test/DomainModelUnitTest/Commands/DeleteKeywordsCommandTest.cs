namespace ArticleHarbor.DomainModel.Commands
{
    using Models;
    using Moq;
    using Ploeh.SemanticComparison.Fluent;
    using Queries;
    using Xunit;

    public class DeleteKeywordsCommandTest : IdiomaticTest<DeleteKeywordsCommand>
    {
        [Test]
        public void SutIsModelCommand(DeleteKeywordsCommand sut)
        {
            Assert.IsAssignableFrom<EmptyCommand<IModel>>(sut);
        }

        [Test]
        public void ExecuteAsyncArticleDeletesKeywordsRelatedWithArticle(
            DeleteKeywordsCommand sut,
            Article article)
        {
            var actual = sut.ExecuteAsync(article).Result;

            Assert.Empty(actual);
            sut.Repositories.Keywords.ToMock().Verify(
                x => x.DeleteAsync(Predicate.Equal("ArticleId", article.Id)));
        }
    }
}