namespace ArticleHarbor.DomainModel.Models
{
    using System.Collections.Generic;
    using System.Reflection;
    using Xunit;

    public class DeleteBookmarksCommandTest : IdiomaticTest<DeleteBookmarksCommand>
    {
        [Test]
        public void SutIsModelCommand(DeleteBookmarksCommand sut)
        {
            Assert.IsAssignableFrom<ModelCommand<IEnumerable<IModel>>>(sut);
        }

        [Test]
        public void ValueIsEmpty(DeleteBookmarksCommand sut)
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