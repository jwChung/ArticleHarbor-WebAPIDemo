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
        public void ExecuteAsyncUserReturnsEmptyWhenCanExecuteAsyncUserReturnsFalse(
            ConditionalCommand<string> sut,
            User user)
        {
            sut.Condition.Of(x => x.CanExecuteAsync(user) == Task.FromResult(false));
            var actual = sut.ExecuteAsync(user).Result;
            Assert.Empty(actual);
        }

        [Test]
        public void ExecuteAsyncUserReturnsCorrectResultWhenCanExecuteAsyncUserReturnsTrue(
            ConditionalCommand<object> sut,
            User user,
            IEnumerable<object> value)
        {
            sut.Condition.Of(x => x.CanExecuteAsync(user) == Task.FromResult(true));
            sut.InnerCommand.Of(x => x.ExecuteAsync(user) == Task.FromResult(value));

            var actual = sut.ExecuteAsync(user).Result;

            Assert.Equal(value, actual);
        }

        [Test]
        public void ExecuteAsyncArticleReturnsEmptyWhenCanExecuteAsyncArticleReturnsFalse(
            ConditionalCommand<string> sut,
            Article article)
        {
            sut.Condition.Of(x => x.CanExecuteAsync(article) == Task.FromResult(false));
            var actual = sut.ExecuteAsync(article).Result;
            Assert.Empty(actual);
        }

        [Test]
        public void ExecuteAsyncArticleReturnsCorrectResultWhenCanExecuteAsyncArticleReturnsTrue(
            ConditionalCommand<object> sut,
            Article article,
            IEnumerable<object> value)
        {
            sut.Condition.Of(x => x.CanExecuteAsync(article) == Task.FromResult(true));
            sut.InnerCommand.Of(x => x.ExecuteAsync(article) == Task.FromResult(value));

            var actual = sut.ExecuteAsync(article).Result;

            Assert.Equal(value, actual);
        }

        [Test]
        public void ExecuteAsyncKeywordReturnsEmptyWhenCanExecuteAsyncKeywordReturnsFalse(
            ConditionalCommand<int> sut,
            Keyword keyword)
        {
            sut.Condition.Of(x => x.CanExecuteAsync(keyword) == Task.FromResult(false));
            var actual = sut.ExecuteAsync(keyword).Result;
            Assert.Empty(actual);
        }

        [Test]
        public void ExecuteAsyncKeywordReturnsCorrectResultWhenCanExecuteAsyncKeywordReturnsTrue(
            ConditionalCommand<string> sut,
            Keyword keyword,
            IEnumerable<string> value)
        {
            sut.Condition.Of(x => x.CanExecuteAsync(keyword) == Task.FromResult(true));
            sut.InnerCommand.Of(x => x.ExecuteAsync(keyword) == Task.FromResult(value));

            var actual = sut.ExecuteAsync(keyword).Result;

            Assert.Equal(value, actual);
        }

        [Test]
        public void ExecuteAsyncBookmarkReturnsEmptyWhenCanExecuteAsyncBookmarkReturnsFalse(
            ConditionalCommand<string> sut,
            Bookmark bookmark)
        {
            sut.Condition.Of(x => x.CanExecuteAsync(bookmark) == Task.FromResult(false));
            var actual = sut.ExecuteAsync(bookmark).Result;
            Assert.Empty(actual);
        }

        [Test]
        public void ExecuteAsyncBookmarkReturnsCorrectResultWhenCanExecuteAsyncBookmarkReturnsTrue(
            ConditionalCommand<int> sut,
            Bookmark bookmark,
            IEnumerable<int> value)
        {
            sut.Condition.Of(x => x.CanExecuteAsync(bookmark) == Task.FromResult(true));
            sut.InnerCommand.Of(x => x.ExecuteAsync(bookmark) == Task.FromResult(value));

            var actual = sut.ExecuteAsync(bookmark).Result;

            Assert.Equal(value, actual);
        }
    }
}