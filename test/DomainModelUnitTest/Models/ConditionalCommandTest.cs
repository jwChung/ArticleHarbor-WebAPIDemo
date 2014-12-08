namespace ArticleHarbor.DomainModel.Models
{
    using System.Collections.Generic;
    using System.Reflection;
    using Xunit;

    public class ConditionalCommandTest : IdiomaticTest<ConditionalCommand<object>>
    {
        [Test]
        public void SutIsModelCommand(ConditionalCommand<int> sut)
        {
            Assert.IsAssignableFrom<ModelCommand<int>>(sut);
        }

        protected override IEnumerable<MemberInfo> ExceptToVerifyInitialization()
        {
            yield return this.Properties.Select(x => x.Value);
        }
    }
}