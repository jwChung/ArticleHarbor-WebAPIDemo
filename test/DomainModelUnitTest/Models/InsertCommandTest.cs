namespace ArticleHarbor.DomainModel.Models
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;
    using Ploeh.AutoFixture.Xunit;
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

        [Test]
        public void ExecuteUserAddsUserToRepository(InsertCommand sut, User user, User newUser)
        {
            sut.UnitOfWork.Users.Of(x => x.InsertAsync(user) == Task.FromResult(newUser));
            
            var actual = sut.Execute(user);

            var command = Assert.IsAssignableFrom<InsertCommand>(actual);
            var model = command.Result.Single().Result;
            Assert.Equal(newUser, model);
        }

        [Test]
        public void ExecuteUserReturnsCorrectResult(
            [Frozen] IEnumerable<Task<IModel>> result,
            [Greedy] InsertCommand sut,
            User user)
        {
            var actual = sut.Execute(user);

            var command = Assert.IsAssignableFrom<InsertCommand>(actual);
            Assert.Equal(sut.UnitOfWork, command.UnitOfWork);
            Assert.True(result.All(x => command.Result.Contains(x)));
        }
    }
}