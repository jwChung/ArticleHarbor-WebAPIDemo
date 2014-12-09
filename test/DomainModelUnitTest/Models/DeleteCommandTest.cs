namespace ArticleHarbor.DomainModel.Models
{
    using System.Collections.Generic;
    using System.Reflection;
    using Xunit;

    public class DeleteCommandTest : IdiomaticTest<DeleteCommand>
    {
        [Test]
        public void SutIsModelCommand(DeleteCommand sut)
        {
            Assert.IsAssignableFrom<ModelCommand<IEnumerable<IModel>>>(sut);
        }

        [Test]
        public void ValueIsEmpty(DeleteCommand sut)
        {
            var actual = sut.Value;
            Assert.Empty(actual);
        }

        protected override IEnumerable<MemberInfo> ExceptToVerifyInitialization()
        {
            yield return this.Properties.Select(x => x.Value);
        }
    }
}