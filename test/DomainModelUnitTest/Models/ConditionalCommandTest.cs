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

        [Test]
        public void ValueIsFromInnerCommand(ConditionalCommand<string> sut)
        {
            var expected = sut.InnerCommand.Value;
            var actual = sut.Value;
            Assert.Equal(expected, actual);
        }

        protected override IEnumerable<MemberInfo> ExceptToVerifyInitialization()
        {
            yield return this.Properties.Select(x => x.Value);
        }
    }
}