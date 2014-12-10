namespace ArticleHarbor.DomainModel.Models
{
    using System.Collections.Generic;
    using System.Reflection;
    using Moq;
    using Ploeh.SemanticComparison.Fluent;
    using Xunit;

    public class DeleteBookmarksCommandTest : IdiomaticTest<DeleteBookmarksCommand>
    {
        [Test]
        public void SutIsModelCommand(DeleteBookmarksCommand sut)
        {
            Assert.IsAssignableFrom<ModelCommand<IEnumerable<IModel>>>(sut);
        }

        [Test]
        public void ValueIsEmpty(DeleteBookmarksCommand sut)
        {
            var actual = sut.Value;
            Assert.Empty(actual);
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

            Assert.Equal(sut, actual);
            sut.Repositories.Bookmarks.ToMock().Verify(
                x => x.ExecuteDeleteCommandAsync(It.Is<IPredicate>(p => likeness.Equals(p))));
        }

        protected override IEnumerable<MemberInfo> ExceptToVerifyInitialization()
        {
            yield return this.Properties.Select(x => x.Value);
        }
    }
}