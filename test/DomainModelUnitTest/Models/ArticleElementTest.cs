namespace ArticleHarbor.DomainModel.Models
{
    using System.Collections.Generic;
    using System.Reflection;
    using Xunit;

    public class ArticleElementTest : IdiomaticTest<ArticleElement>
    {
        [Test]
        public void SutIsModelElement(ArticleElement sut)
        {
            Assert.IsAssignableFrom<IModelElement>(sut);
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