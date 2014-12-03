namespace ArticleHarbor.DomainModel.Models
{
    using System.Collections.Generic;
    using Xunit;

    public class CreateCommandTest : IdiomaticTest<CreateCommand>
    {
        [Test]
        public void SutIsModelCommand(CreateCommand sut)
        {
            Assert.IsAssignableFrom<ModelCommand<IEnumerable<IModel>>>(sut);
        }
    }
}