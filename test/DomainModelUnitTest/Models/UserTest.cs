namespace ArticleHarbor.DomainModel.Models
{
    using Xunit;

    public class UserTest : IdiomaticTest<User>
    {
        [Test]
        public void SutIsModel(User sut)
        {
            Assert.IsAssignableFrom<IModel>(sut);
        }

        [Test]
        public void ExecuteCommandReturnsCorrectResult(
            User sut,
            IModelCommand<object> command,
            IModelCommand<object> expected)
        {
            command.Of(x => x.Execute(sut) == expected);
            var actual = sut.ExecuteCommand(command);
            Assert.Equal(expected, actual);
        }
    }
}