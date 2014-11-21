namespace ArticleHarbor.DomainModel.Models
{
    using Xunit;

    public class CanModifyConfirmableCommandTest
    {
        [Test]
        public void SutIsModelCommand(CanModifyConfirmableCommand sut)
        {
            Assert.IsAssignableFrom<ModelCommand<object>>(sut);
        }
    }
}