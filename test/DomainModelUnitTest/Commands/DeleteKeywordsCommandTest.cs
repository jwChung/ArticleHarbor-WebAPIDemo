namespace ArticleHarbor.DomainModel.Commands
{
    using Moq;
    using Ploeh.SemanticComparison.Fluent;
    using Queries;
    using Xunit;

    public class DeleteKeywordsCommandTest : IdiomaticTest<DeleteKeywordsCommand>
    {
        [Test]
        public void SutIsModelCommand(DeleteKeywordsCommand sut)
        {
            Assert.IsAssignableFrom<ModelCommand<IModel>>(sut);
        }

        [Test]
        public void ExecuteAsyncArticleDeletesKeywordsRelatedWithArticle(
            DeleteKeywordsCommand sut,
            Article article)
        {
            var likeness = new EqualPredicate("ArticleId", article.Id).AsSource()
                .OfLikeness<EqualPredicate>()
                .Without(x => x.SqlText)
                .Without(x => x.Parameters);
            
            var actual = sut.ExecuteAsync(article).Result;

            Assert.Empty(actual);
            sut.Repositories.Keywords.ToMock().Verify(
                x => x.ExecuteDeleteCommandAsync(It.Is<IPredicate>(p => likeness.Equals(p))));
        }
    }
}