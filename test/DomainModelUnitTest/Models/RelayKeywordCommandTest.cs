namespace ArticleHarbor.DomainModel.Models
{
    using System.Collections.Generic;
    using Xunit;

    public class RelayKeywordCommandTest : IdiomaticTest<RelayKeywordCommand>
    {
        [Test]
        public void SutIsModelCommand(RelayKeywordCommand sut)
        {
            Assert.IsAssignableFrom<ModelCommand<IEnumerable<IModel>>>(sut);
        }
    }
}