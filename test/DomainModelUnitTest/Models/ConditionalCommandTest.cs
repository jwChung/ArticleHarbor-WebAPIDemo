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

        protected override IEnumerable<MemberInfo> ExceptToVerifyInitialization()
        {
            yield return this.Properties.Select(x => x.Value);
        }
    }
}