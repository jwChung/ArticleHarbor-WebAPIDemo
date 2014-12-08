namespace ArticleHarbor.DomainModel.Models
{
    using System.Collections.Generic;
    using System.Reflection;
    using System.Threading.Tasks;
    using Moq;
    using Xunit;

    public class ConditionalCommandTest : IdiomaticTest<ConditionalCommand<object>>
    {
        [Test]
        public void SutIsModelCommand(ConditionalCommand<int> sut)
        {
            Assert.IsAssignableFrom<ModelCommand<int>>(sut);
        }

        [Test]
        public void ValueIsFromInnerCommand(ConditionalCommand<string> sut)
        {
            var expected = sut.InnerCommand.Value;
            var actual = sut.Value;
            Assert.Equal(expected, actual);
        }

        [Test]
        public void ExecuteAsyncUserReturnsSutItselfWhenCanExecuteAsyncUserReturnsFalse(
            ConditionalCommand<string> sut,
            User user)
        {
            sut.Condition.Of(x => x.CanExecuteAsync(user) == Task.FromResult(false));
            var actual = sut.ExecuteAsync(user).Result;
            Assert.Equal(sut, actual);
        }

        [Test]
        public void ExecuteAsyncUserReturnsCorrectCommandWhenCanExecuteAsyncUserReturnsTrue(
            ConditionalCommand<object> sut,
            User user,
            IModelCommand<object> newInnerCommand)
        {
            sut.Condition.Of(x => x.CanExecuteAsync(user) == Task.FromResult(true));
            sut.InnerCommand.Of(x => x.ExecuteAsync(user) == Task.FromResult(newInnerCommand));

            var actual = sut.ExecuteAsync(user).Result;

            var command = Assert.IsAssignableFrom<ConditionalCommand<object>>(actual);
            Assert.Equal(newInnerCommand, command.InnerCommand);
        }

        [Test]
        public void ExecuteAsyncArticleReturnsSutItselfWhenCanExecuteAsyncArticleReturnsFalse(
            ConditionalCommand<string> sut,
            Article article)
        {
            sut.Condition.Of(x => x.CanExecuteAsync(article) == Task.FromResult(false));
            var actual = sut.ExecuteAsync(article).Result;
            Assert.Equal(sut, actual);
        }

        [Test]
        public void ExecuteAsyncArticleReturnsCorrectCommandWhenCanExecuteAsyncArticleReturnsTrue(
            ConditionalCommand<object> sut,
            Article article,
            IModelCommand<object> newInnerCommand)
        {
            sut.Condition.Of(x => x.CanExecuteAsync(article) == Task.FromResult(true));
            sut.InnerCommand.Of(x => x.ExecuteAsync(article) == Task.FromResult(newInnerCommand));

            var actual = sut.ExecuteAsync(article).Result;

            var command = Assert.IsAssignableFrom<ConditionalCommand<object>>(actual);
            Assert.Equal(newInnerCommand, command.InnerCommand);
        }

        [Test]
        public void ExecuteAsyncKeywordReturnsSutItselfWhenCanExecuteAsyncKeywordReturnsFalse(
            ConditionalCommand<string> sut,
            Keyword keyword)
        {
            sut.Condition.Of(x => x.CanExecuteAsync(keyword) == Task.FromResult(false));
            var actual = sut.ExecuteAsync(keyword).Result;
            Assert.Equal(sut, actual);
        }

        [Test]
        public void ExecuteAsyncKeywordReturnsCorrectCommandWhenCanExecuteAsyncKeywordReturnsTrue(
            ConditionalCommand<object> sut,
            Keyword keyword,
            IModelCommand<object> newInnerCommand)
        {
            sut.Condition.Of(x => x.CanExecuteAsync(keyword) == Task.FromResult(true));
            sut.InnerCommand.Of(x => x.ExecuteAsync(keyword) == Task.FromResult(newInnerCommand));

            var actual = sut.ExecuteAsync(keyword).Result;

            var command = Assert.IsAssignableFrom<ConditionalCommand<object>>(actual);
            Assert.Equal(newInnerCommand, command.InnerCommand);
        }

        [Test]
        public void ExecuteAsyncBookmarkReturnsSutItselfWhenCanExecuteAsyncBookmarkReturnsFalse(
            ConditionalCommand<string> sut,
            Bookmark bookmark)
        {
            sut.Condition.Of(x => x.CanExecuteAsync(bookmark) == Task.FromResult(false));
            var actual = sut.ExecuteAsync(bookmark).Result;
            Assert.Equal(sut, actual);
        }

        [Test]
        public void ExecuteAsyncBookmarkReturnsCorrectCommandWhenCanExecuteAsyncBookmarkReturnsTrue(
            ConditionalCommand<object> sut,
            Bookmark bookmark,
            IModelCommand<object> newInnerCommand)
        {
            sut.Condition.Of(x => x.CanExecuteAsync(bookmark) == Task.FromResult(true));
            sut.InnerCommand.Of(x => x.ExecuteAsync(bookmark) == Task.FromResult(newInnerCommand));

            var actual = sut.ExecuteAsync(bookmark).Result;

            var command = Assert.IsAssignableFrom<ConditionalCommand<object>>(actual);
            Assert.Equal(newInnerCommand, command.InnerCommand);
        }

        protected override IEnumerable<MemberInfo> ExceptToVerifyInitialization()
        {
            yield return this.Properties.Select(x => x.Value);
        }
    }
}