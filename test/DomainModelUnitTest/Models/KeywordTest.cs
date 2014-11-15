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
        public void ExecuteCommandReturnsCorrectResult(
            Keyword sut,
            IModelCommand<object> command,
            IModelCommand<object> expected)
        {
            command.Of(x => x.Execute(sut) == expected);
            var actual = sut.ExecuteCommand(command);
            Assert.Equal(expected, actual);
        }
    }
}