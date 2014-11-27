namespace ArticleHarbor.DomainModel.Models
{
    using Xunit;

    public class KeywordTest : IdiomaticTest<Keyword>
    {
        [Test]
        public void SutIsModel(Keyword sut)
        {
            Assert.IsAssignableFrom<IModel>(sut);
        }

        [Test]
        public void ExecuteReturnsCorrectResult(
            Keyword sut,
            IModelCommand<object> command,
            IModelCommand<object> expected)
        {
            command.Of(x => x.Execute(sut) == expected);
            var actual = sut.Execute(command);
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