namespace ArticleHarbor.DomainModel.Models
{
    using System.Collections.Generic;
    using Xunit;

    public class NewInsertCommandTest : IdiomaticTest<NewInsertCommand>
    {
        [Test]
        public void SutIsModelCommand(NewInsertCommand sut)
        {
            Assert.IsAssignableFrom<ModelCommand<IEnumerable<IModel>>>(sut);
        }
    }
}