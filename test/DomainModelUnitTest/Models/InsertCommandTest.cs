namespace ArticleHarbor.DomainModel.Models
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Moq;
    using Ploeh.AutoFixture.Xunit;
    using Xunit;

    public class InsertCommandTest : IdiomaticTest<InsertCommand>
    {
        [Test]
        public void SutIsModelCommand(InsertCommand sut)
        {
            Assert.IsAssignableFrom<ModelCommand<IEnumerable<IModel>>>(sut);
        }

        [Test]
        public void BaseValueIsCorrect(
            [Frozen] IEnumerable<IModel> baseValue,
             IEnumerable<IModel> innerCommandValue,
            InsertCommand sut)
        {
            ////sut.InnerCommand.Of(x => x.Value == innerCommandValue);
            ////var actual = sut.BaseValue;
            ////Assert.Equal(baseValue, actual);
        }

        [Test]
        public void ExecuteAsyncUserCorrectlyInsertsUser(
            [Frozen] IEnumerable<IModel> baseValue,
            InsertCommand sut,
            User user,
            User newUser,
            IModelCommand<IEnumerable<IModel>> newInnerCommand)
        {
            sut.Repositories.Users.Of(x => x.InsertAsync(user) == Task.FromResult(newUser));
            ////sut.InnerCommand.Of(x => x.ExecuteAsync(newUser) == Task.FromResult(newInnerCommand));
            var expected = baseValue.Concat(new IModel[] { newUser });

            var actual = sut.ExecuteAsync(user).Result;

            var command = Assert.IsAssignableFrom<InsertCommand>(actual);
            Assert.Equal(newInnerCommand, command.InnerCommand);
            Assert.True(expected.SequenceEqual(command.BaseValue));
        }

        [Test]
        public void ExecuteAsyncArticleCorrectlyInsertsArticle(
            [Frozen] IEnumerable<IModel> baseValue,
            InsertCommand sut,
            Article article,
            Article newArticle,
            IModelCommand<IEnumerable<IModel>> newInnerCommand)
        {
            sut.Repositories.Articles.Of(x => x.InsertAsync(article) == Task.FromResult(newArticle));
            ////sut.InnerCommand.Of(x => x.ExecuteAsync(newArticle) == Task.FromResult(newInnerCommand));
            var expected = baseValue.Concat(new IModel[] { newArticle });

            var actual = sut.ExecuteAsync(article).Result;

            var command = Assert.IsAssignableFrom<InsertCommand>(actual);
            Assert.Equal(newInnerCommand, command.InnerCommand);
            Assert.True(expected.SequenceEqual(command.BaseValue));
        }

        [Test]
        public void ExecuteAsyncKeywordCorrectlyInsertsKeyword(
            [Frozen] IEnumerable<IModel> baseValue,
            InsertCommand sut,
            Keyword keyword,
            Keyword newKeyword,
            IModelCommand<IEnumerable<IModel>> newInnerCommand)
        {
            sut.Repositories.Keywords.Of(x => x.InsertAsync(keyword) == Task.FromResult(newKeyword));
            ////sut.InnerCommand.Of(x => x.ExecuteAsync(newKeyword) == Task.FromResult(newInnerCommand));
            var expected = baseValue.Concat(new IModel[] { newKeyword });

            var actual = sut.ExecuteAsync(keyword).Result;

            var command = Assert.IsAssignableFrom<InsertCommand>(actual);
            Assert.Equal(newInnerCommand, command.InnerCommand);
            Assert.True(expected.SequenceEqual(command.BaseValue));
        }

        [Test]
        public void ExecuteAsyncBookmarkCorrectlyInsertsBookmark(
            [Frozen] IEnumerable<IModel> baseValue,
            InsertCommand sut,
            Bookmark bookmark,
            Bookmark newBookmark,
            IModelCommand<IEnumerable<IModel>> newInnerCommand)
        {
            sut.Repositories.Bookmarks.Of(x => x.InsertAsync(bookmark) == Task.FromResult(newBookmark));
            ////sut.InnerCommand.Of(x => x.ExecuteAsync(newBookmark) == Task.FromResult(newInnerCommand));
            var expected = baseValue.Concat(new IModel[] { newBookmark });

            var actual = sut.ExecuteAsync(bookmark).Result;

            var command = Assert.IsAssignableFrom<InsertCommand>(actual);
            Assert.Equal(newInnerCommand, command.InnerCommand);
            Assert.True(expected.SequenceEqual(command.BaseValue));
        }
    }
}