namespace ArticleHarbor.DomainModel.Commands
{
    using Models;
    using Moq;
    using Ploeh.SemanticComparison.Fluent;
    using Queries;
    using Xunit;

    public class DeleteBookmarksCommandTest : IdiomaticTest<DeleteBookmarksCommand>
    {
        [Test]
        public void SutIsModelCommand(DeleteBookmarksCommand sut)
        {
            Assert.IsAssignableFrom<EmptyCommand<IModel>>(sut);
        }

        [Test]
        public void ExecuteAsyncUserDeletesBookmarksRelatedWithUser(
            DeleteBookmarksCommand sut,
            User user)
        {
            var actual = sut.ExecuteAsync(user).Result;

            Assert.Empty(actual);
            sut.Repositories.Bookmarks.ToMock().Verify(
                x => x.DeleteAsync(Predicate.Equal("UserId", user.Id)));
        }

        [Test]
        public void ExecuteAsyncArticleDeletesBookmarksRelatedWithArticle(
            DeleteBookmarksCommand sut,
            Article article)
        {
            var actual = sut.ExecuteAsync(article).Result;

            Assert.Empty(actual);
            sut.Repositories.Bookmarks.ToMock().Verify(
                x => x.DeleteAsync(Predicate.Equal("ArticleId", article.Id)));
        }
    }
}