namespace ArticleHarbor.DomainModel.Models
{
    using System.Linq;
    using System.Threading.Tasks;
    using Ploeh.AutoFixture;
    using Xunit;

    public class CompositeModelTest : IdiomaticTest<CompositeModel>
    {
        [Test]
        public void SutIsModel(CompositeModel sut)
        {
            Assert.IsAssignableFrom<IModel>(sut);
        }

        [Test]
        public void ExecuteCallsExecuteRespectively(
            CompositeModel sut,
            IModelCommand<object> command,
            IModelCommand<object>[] commands)
        {
            var models = sut.Models.ToArray();
            models[0].Of(x => x.Execute(command) == commands[0]);
            models[1].Of(x => x.Execute(commands[0]) == commands[1]);
            models[2].Of(x => x.Execute(commands[1]) == commands[2]);

            var actual = sut.Execute(command);

            Assert.Equal(commands[2], actual);
        }

        [Test]
        public void ExecuteAsyncRespectivelyCallsExecuteAsync(
            CompositeModel sut,
            IModelCommand<object> command,
            IModelCommand<object>[] commands)
        {
            var models = sut.Models.ToArray();
            models[0].Of(x => x.ExecuteAsync(command) == Task.FromResult(commands[0]));
            models[1].Of(x => x.ExecuteAsync(commands[0]) == Task.FromResult(commands[1]));
            models[2].Of(x => x.ExecuteAsync(commands[1]) == Task.FromResult(commands[2]));

            var actual = sut.ExecuteAsync(command).Result;

            Assert.Equal(commands[2], actual);
        }

        [Test]
        public void GetKeysReturnsCorrectKeys(CompositeModel sut, Generator<Keys> generator)
        {
            foreach (var model in sut.Models)
                model.Of(x => x.GetKeys() == generator.Take(1).Single());
            var expected = new Keys(sut.Models.SelectMany(x => x.GetKeys()));
            
            var actual = sut.GetKeys();
            
            Assert.Equal(expected, actual);
        }
    }
}