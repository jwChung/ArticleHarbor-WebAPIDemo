namespace ArticleHarbor.DomainModel.Models
{
    using System.Collections.Generic;
    using System.Reflection;
    using System.Threading.Tasks;
    using Xunit;

    public class CanCreateConfirmableCommandTest
        : IdiomaticTest<CanCreateConfirmableCommand>
    {
        [Test]
        public void SutIsModelCommand(CanCreateConfirmableCommand sut)
        {
            Assert.IsAssignableFrom<ModelCommand<IEnumerable<Task>>>(sut);
        }

        protected override IEnumerable<MemberInfo> ExceptToVerifyInitialization()
        {
            yield return this.Properties.Select(x => x.Result);
        }
    }
}