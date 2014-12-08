namespace ArticleHarbor.DomainModel.Models
{
    using System.Collections.Generic;
    using System.Reflection;
    using Xunit;

    public class ConditionalCommandTest : IdiomaticTest<ConditionalCommand>
    {
        [Test]
        public void SutIsModelCommand(ConditionalCommand sut)
        {
            Assert.IsAssignableFrom<ModelCommand<IEnumerable<IModel>>>(sut);
        }

        protected override IEnumerable<MemberInfo> ExceptToVerifyInitialization()
        {
            yield return this.Properties.Select(x => x.Value);
        }
    }
}