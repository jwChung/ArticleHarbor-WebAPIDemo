namespace ArticleHarbor.DomainModel.Models
{
    using System.Linq;
    using Xunit;

    public class CompositeModelTest : IdiomaticTest<CompositeModel>
    {
        [Test]
        public void SutIsModel(CompositeModel sut)
        {
            Assert.IsAssignableFrom<IModel>(sut);
        }

        [Test]
        public void ExecuteCommandCallsExecuteCommandRespectively(
            CompositeModel sut,
            IModelCommand<object> command,
            IModelCommand<object>[] commands)
        {
            var models = sut.Models.ToArray();
            models[0].Of(x => x.ExecuteCommand(command) == commands[0]);
            models[1].Of(x => x.ExecuteCommand(commands[0]) == commands[1]);
            models[2].Of(x => x.ExecuteCommand(commands[1]) == commands[2]);

            var actual = sut.ExecuteCommand(command);

            Assert.Equal(commands[2], actual);
        }
    }
}