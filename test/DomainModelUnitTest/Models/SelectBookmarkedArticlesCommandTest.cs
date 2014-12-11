namespace ArticleHarbor.DomainModel.Models
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
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
            Assert.IsAssignableFrom<ModelCommand<IModel>>(sut);
        }

        [Test]
        public void ExecuteAsyncUserReturnsCorrectResult(
            SelectBookmarkedArticlesCommand sut,
            User user,
            IEnumerable<Bookmark> bookmarks,
            IEnumerable<Article> articles)
        {
            // Fixture setup
            var equalPredicateLikeness = new EqualPredicate("UserId", user.Id).AsSource()
                .OfLikeness<EqualPredicate>()
                .Without(x => x.SqlText)
                .Without(x => x.Parameters);

            sut.Repositories.Bookmarks.Of(x => x.ExecuteSelectCommandAsync(
                It.Is<IPredicate>(p => equalPredicateLikeness.Equals(p)))
                == Task.FromResult(bookmarks));

            var inClausePredicateLikeness = new InClausePredicate(
                "Id", bookmarks.Select(b => b.ArticleId).Cast<object>())
                .AsSource()
                .OfLikeness<InClausePredicate>()
                .With(x => x.ParameterValues)
                .EqualsWhen((a, b) => a.ParameterValues.SequenceEqual(b.ParameterValues))
                .Without(x => x.SqlText)
                .Without(x => x.Parameters);

            sut.Repositories.Articles.Of(x => x.ExecuteSelectCommandAsync(
                It.Is<IPredicate>(p => inClausePredicateLikeness.Equals(p)))
                == Task.FromResult(articles));

            // Exercise system
            var actual = sut.ExecuteAsync(user).Result;

            // Verify outcome
            Assert.Equal(articles, actual);
        }
    }
}