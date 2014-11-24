namespace ArticleHarbor.DomainModel.Models
{
    using System.Collections.Generic;
    using System.Reflection;
    using System.Threading.Tasks;
    using Xunit;

    public class InsertCommandTest : IdiomaticTest<InsertCommand>
    {
        [Test]
        public void SutIsModelCommand(InsertCommand sut)
        {
            Assert.IsAssignableFrom<ModelCommand<Task<IModel>>>(sut);
        }

        [Test]
        public void ResultIsEmpty(InsertCommand sut)
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