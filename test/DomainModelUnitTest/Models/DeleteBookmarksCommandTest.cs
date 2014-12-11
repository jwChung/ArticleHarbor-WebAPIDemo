namespace ArticleHarbor.DomainModel.Models
{
    using System.Collections.Generic;
    using System.Reflection;
    using Moq;
    using Ploeh.SemanticComparison.Fluent;
    using Queries;
    using Xunit;

    public class DeleteBookmarksCommandTest : IdiomaticTest<DeleteBookmarksCommand>
    {
        [Test]
        public void SutIsModelCommand(DeleteBookmarksCommand sut)
        {
            Assert.IsAssignableFrom<ModelCommand<IModel>>(sut);
        }

        [Test]
        public void ExecuteAsyncUserDeletesBookmarksRelatedWithUser(
            DeleteBookmarksCommand sut,
            User user)
        {
            var likeness = new EqualPredicate("UserId", user.Id).AsSource()
                .OfLikeness<EqualPredicate>()
                .Without(x => x.SqlText)
                .Without(x => x.Parameters);

            var actual = sut.ExecuteAsync(user).Result;

            Assert.Empty(actual);
            sut.Repositories.Bookmarks.ToMock().Verify(
                x => x.ExecuteDeleteCommandAsync(It.Is<IPredicate>(p => likeness.Equals(p))));
        }

        [Test]
        public void ExecuteAsyncArticleDeletesBookmarksRelatedWithArticle(
            DeleteBookmarksCommand sut,
            Article article)
        {
            var likeness = new EqualPredicate("ArticleId", article.Id).AsSource()
                .OfLikeness<EqualPredicate>()
                .Without(x => x.SqlText)
                .Without(x => x.Parameters);

            var actual = sut.ExecuteAsync(article).Result;

            Assert.Empty(actual);
            sut.Repositories.Bookmarks.ToMock().Verify(
                x => x.ExecuteDeleteCommandAsync(It.Is<IPredicate>(p => likeness.Equals(p))));
        }
    }
}