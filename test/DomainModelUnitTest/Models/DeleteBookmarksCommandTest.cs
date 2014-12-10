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

        protected override IEnumerable<MemberInfo> ExceptToVerifyInitialization()
        {
            yield return this.Properties.Select(x => x.Value);
        }
    }
}