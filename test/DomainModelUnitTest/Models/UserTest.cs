﻿namespace ArticleHarbor.DomainModel.Models
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Xunit;

    public class UserTest : IdiomaticTest<User>
    {
        [Test]
        public void SutIsModel(User sut)
        {
            Assert.IsAssignableFrom<IModel>(sut);
        }

        [Test]
        public void ExecuteAsyncReturnsCorrectResult(
            User sut,
            IModelCommand<object> command,
            IEnumerable<object> expected)
        {
            command.Of(x => x.ExecuteAsync(sut) == Task.FromResult(expected));
            var actual = sut.ExecuteAsync(command).Result;
            Assert.Equal(expected, actual);
        }

        [Test]
        public void GetKeysReturnsCorrectKeys(User sut)
        {
            var expected = new Keys<string>(sut.Id);
            var actual = sut.GetKeys();
            Assert.Equal(expected, actual);
        }
    }
}