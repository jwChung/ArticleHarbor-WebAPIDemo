namespace ArticleHarbor.DomainModel.Models
{
    using System.Collections.Generic;
    using Xunit;

    public class RelayKeywordsCommandTest : IdiomaticTest<RelayKeywordsCommand>
    {
        [Test]
        public void SutIsModelCommand(RelayKeywordsCommand sut)
        {
            Assert.IsAssignableFrom<ModelCommand<IEnumerable<IModel>>>(sut);
        }
    }
}