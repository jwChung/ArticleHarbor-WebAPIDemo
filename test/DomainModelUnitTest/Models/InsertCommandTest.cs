namespace ArticleHarbor.DomainModel.Models
{
    using System.Collections.Generic;
    using Xunit;

    public class InsertCommandTest : IdiomaticTest<InsertCommand>
    {
        [Test]
        public void SutIsModelCommand(InsertCommand sut)
        {
            Assert.IsAssignableFrom<ModelCommand<IEnumerable<IModel>>>(sut);
        }
    }
}