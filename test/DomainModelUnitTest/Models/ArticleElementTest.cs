namespace ArticleHarbor.DomainModel.Models
{
    using Xunit;

    public class ArticleElementTest : IdiomaticTest<ArticleElement>
    {
        [Test]
        public void SutIsModelElement(ArticleElement sut)
        {
            Assert.IsAssignableFrom<IModelElement<Article>>(sut);
        }

        [Test]
        public void ExecuteCallsCorrectCommandMethod(
            ArticleElement sut,
            IModelElementCommand<object> command,
            IModelElementCommand<object> expected)
        {
            command.Of(x => x.Execute(sut) == expected);
            var actual = sut.Execute(command);
            Assert.Equal(expected, actual);
        }
    }
}