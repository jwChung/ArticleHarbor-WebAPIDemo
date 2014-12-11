﻿namespace ArticleHarbor.DomainModel.Models
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Commands;
    using Ploeh.AutoFixture;
    using Queries;
    using Xunit;

    public class CompositeModelTest : IdiomaticTest<CompositeModel>
    {
        [Test]
        public void SutIsModel(CompositeModel sut)
        {
            Assert.IsAssignableFrom<IModel>(sut);
        }

        [Test]
        public void ExecuteAsyncRespectivelyCallsExecuteAsync(
            CompositeModel sut,
            IModelCommand<object> command,
            IEnumerable<object>[] values)
        {
            var models = sut.Models.ToArray();
            models[0].Of(x => x.ExecuteAsync(command) == Task.FromResult(values[0]));
            models[1].Of(x => x.ExecuteAsync(command) == Task.FromResult(values[1]));
            models[2].Of(x => x.ExecuteAsync(command) == Task.FromResult(values[2]));

            var actual = sut.ExecuteAsync(command).Result;

            Assert.Equal(values.SelectMany(x => x), actual);
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