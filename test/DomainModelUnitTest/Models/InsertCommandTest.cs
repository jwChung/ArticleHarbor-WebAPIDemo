namespace ArticleHarbor.DomainModel.Models
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Moq;
    using Xunit;

    public class InsertCommandTest : IdiomaticTest<InsertCommand>
    {
        [Test]
        public void SutIsModelCommand(InsertCommand sut)
        {
            Assert.IsAssignableFrom<ModelCommand<IEnumerable<IModel>>>(sut);
        }

        [Test]
        public void ExecuteAsyncUserCorrectlyInsertsUser(
            InsertCommand sut,
            User user,
            User newUser,
            IEnumerable<IModel> newInnerCommandValue)
        {
            sut.Repositories.Users.Of(x => x.InsertAsync(user) == Task.FromResult(newUser));
            sut.InnerCommand.Of(x => x.ExecuteAsync(newUser) == Task.FromResult(
                Mock.Of<IModelCommand<IEnumerable<IModel>>>(c => c.Value == newInnerCommandValue)));
            var expected = sut.Value.Concat(new IModel[] { newUser }).Concat(newInnerCommandValue);

            var actual = sut.ExecuteAsync(user).Result;

            var newCommand = Assert.IsAssignableFrom<InsertCommand>(actual);
            Assert.Equal(sut.InnerCommand, newCommand.InnerCommand);
            this.AssertEquivalent(expected, newCommand.Value);
        }

        [Test]
        public void ExecuteAsyncArticleCorrectlyInsertsArticle(
            InsertCommand sut,
            Article article,
            Article newArticle,
            IEnumerable<IModel> newInnerCommandValue)
        {
            sut.Repositories.Articles.Of(x => x.InsertAsync(article) == Task.FromResult(newArticle));
            sut.InnerCommand.Of(x => x.ExecuteAsync(newArticle) == Task.FromResult(
                Mock.Of<IModelCommand<IEnumerable<IModel>>>(c => c.Value == newInnerCommandValue)));
            var expected = sut.Value.Concat(new IModel[] { newArticle }).Concat(newInnerCommandValue);

            var actual = sut.ExecuteAsync(article).Result;

            var newCommand = Assert.IsAssignableFrom<InsertCommand>(actual);
            Assert.Equal(sut.InnerCommand, newCommand.InnerCommand);
            this.AssertEquivalent(expected, newCommand.Value);
        }

        [Test]
        public void ExecuteAsyncKeywordCorrectlyInsertsKeyword(
            InsertCommand sut,
            Keyword keyword,
            Keyword newKeyword,
            IEnumerable<IModel> newInnerCommandValue)
        {
            sut.Repositories.Keywords.Of(x => x.InsertAsync(keyword) == Task.FromResult(newKeyword));
            sut.InnerCommand.Of(x => x.ExecuteAsync(newKeyword) == Task.FromResult(
                Mock.Of<IModelCommand<IEnumerable<IModel>>>(c => c.Value == newInnerCommandValue)));
            var expected = sut.Value.Concat(new IModel[] { newKeyword }).Concat(newInnerCommandValue);

            var actual = sut.ExecuteAsync(keyword).Result;

            var newCommand = Assert.IsAssignableFrom<InsertCommand>(actual);
            Assert.Equal(sut.InnerCommand, newCommand.InnerCommand);
            this.AssertEquivalent(expected, newCommand.Value);
        }

        [Test]
        public void ExecuteAsyncBookmarkCorrectlyInsertsBookmark(
            InsertCommand sut,
            Bookmark bookmark,
            Bookmark newBookmark,
            IEnumerable<IModel> newInnerCommandValue)
        {
            sut.Repositories.Bookmarks.Of(x => x.InsertAsync(bookmark) == Task.FromResult(newBookmark));
            sut.InnerCommand.Of(x => x.ExecuteAsync(newBookmark) == Task.FromResult(
                Mock.Of<IModelCommand<IEnumerable<IModel>>>(c => c.Value == newInnerCommandValue)));
            var expected = sut.Value.Concat(new IModel[] { newBookmark }).Concat(newInnerCommandValue);

            var actual = sut.ExecuteAsync(bookmark).Result;

            var newCommand = Assert.IsAssignableFrom<InsertCommand>(actual);
            Assert.Equal(sut.InnerCommand, newCommand.InnerCommand);
            this.AssertEquivalent(expected, newCommand.Value);
        }

        private void AssertEquivalent<T>(IEnumerable<T> expected, IEnumerable<T> actual)
        {
            Assert.Equal(expected.Count(), actual.Count());
            Assert.Empty(expected.Except(actual));
        }
    }
}