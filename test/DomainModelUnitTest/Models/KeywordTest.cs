namespace ArticleHarbor.DomainModel.Models
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Xunit;

    public class KeywordTest : IdiomaticTest<Keyword>
    {
        [Test]
        public void SutIsModel(Keyword sut)
        {
            Assert.IsAssignableFrom<IModel>(sut);
        }

        [Test]
        public void ExecuteAsyncReturnsCorrectResult(
            Keyword sut,
            IModelCommand<object> command,
            IEnumerable<object> expected)
        {
            command.Of(x => x.ExecuteAsync(sut) == Task.FromResult(expected));
            var actual = sut.ExecuteAsync(command).Result;
            Assert.Equal(expected, actual);
        }

        [Test]
        public void GetKeysReturnsCorrectKeys(Keyword sut)
        {
            var expected = new Keys<int, string>(sut.ArticleId, sut.Word);
            var actual = sut.GetKeys();
            Assert.Equal(expected, actual);
        }
    }
}