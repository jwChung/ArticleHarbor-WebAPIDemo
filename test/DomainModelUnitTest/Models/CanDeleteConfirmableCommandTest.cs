namespace ArticleHarbor.DomainModel.Models
{
    using System.Collections.Generic;
    using System.Reflection;
    using System.Threading.Tasks;
    using Xunit;

    public class CanDeleteConfirmableCommandTest : IdiomaticTest<CanDeleteConfirmableCommand>
    {
        [Test]
        public void SutIsModelCommand(CanDeleteConfirmableCommand sut)
        {
            Assert.IsAssignableFrom<ModelCommand<Task>>(sut);
        }

        [Test]
        public void ResultIsEmpty(CanDeleteConfirmableCommand sut)
        {
            var actual = sut.Result;
            Assert.Empty(actual);
        }

        protected override IEnumerable<MemberInfo> ExceptToVerifyInitialization()
        {
            yield return this.Properties.Select(x => x.Result);
        }
    }
}