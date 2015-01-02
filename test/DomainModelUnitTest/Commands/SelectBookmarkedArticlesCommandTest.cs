namespace ArticleHarbor.DomainModel.Commands
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Models;
    using Moq;
    using Ploeh.SemanticComparison.Fluent;
    using Queries;
    using Xunit;

    public class SelectBookmarkedArticlesCommandTest
        : IdiomaticTest<SelectBookmarkedArticlesCommand>
    {
        [Test]
        public void SutIsModelCommand(SelectBookmarkedArticlesCommand sut)
        {
            Assert.IsAssignableFrom<EmptyCommand<IModel>>(sut);
        }

        [Test]
        public void ExecuteAsyncUserReturnsCorrectResult(
            SelectBookmarkedArticlesCommand sut,
            User user,
            IEnumerable<Bookmark> bookmarks,
            IEnumerable<Article> articles)
        {
            // Fixture setup
            sut.Repositories.Bookmarks.Of(x => x.SelectAsync(
                Predicate.Equal("UserId", user.Id)) == Task.FromResult(bookmarks));

            var inClausePredicateLikeness = new InClausePredicate(
                "Id", bookmarks.Select(b => b.ArticleId).Cast<object>())
                .AsSource()
                .OfLikeness<InClausePredicate>()
                .With(x => x.Values)
                .EqualsWhen((a, b) => a.Values.SequenceEqual(b.Values))
                .Without(x => x.SqlText)
                .Without(x => x.Parameters);

            sut.Repositories.Articles.Of(x => x.SelectAsync(
                It.Is<IPredicate>(p => inClausePredicateLikeness.Equals(p)))
                == Task.FromResult(articles));

            // Exercise system
            var actual = sut.ExecuteAsync(user).Result;

            // Verify outcome
            Assert.Equal(articles, actual);
        }
    }
}