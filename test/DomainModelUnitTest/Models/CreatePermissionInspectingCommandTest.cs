namespace ArticleHarbor.DomainModel.Models
{
    using System.Collections.Generic;
    using System.Reflection;
    using System.Threading.Tasks;
    using Xunit;

    public class CreatePermissionInspectingCommandTest
        : IdiomaticTest<CreatePermissionInspectingCommand>
    {
        [Test]
        public void SutIsModelCommand(CreatePermissionInspectingCommand sut)
        {
            Assert.IsAssignableFrom<ModelCommand<IEnumerable<Task>>>(sut);
        }

        protected override IEnumerable<MemberInfo> ExceptToVerifyInitialization()
        {
            yield return this.Properties.Select(x => x.Result);
        }
    }
}