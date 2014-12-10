namespace ArticleHarbor.DomainModel.Models
{
    using System.Collections.Generic;
    using System.Reflection;
    using Xunit;

    public class NullCommandTest : IdiomaticTest<NullCommand>
    {
        [Test]
        public void SutIsModelCommand(NullCommand sut)
        {
            Assert.IsAssignableFrom<ModelCommand<IEnumerable<IModel>>>(sut);
        }
    }
}