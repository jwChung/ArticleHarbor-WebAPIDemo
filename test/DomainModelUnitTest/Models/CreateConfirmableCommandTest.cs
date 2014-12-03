namespace ArticleHarbor.DomainModel.Models
{
    using System.Collections.Generic;
    using Jwc.Experiment.Xunit;
    using Xunit;

    public class CreateConfirmableCommandTest : IdiomaticTest<CreateConfirmableCommand>
    {
        [Test]
        public void SutIsModelCommand(CreateConfirmableCommand sut)
        {
            Assert.IsAssignableFrom<ModelCommand<object>>(sut);
        }
    }
}