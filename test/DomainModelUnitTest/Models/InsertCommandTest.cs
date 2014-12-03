namespace ArticleHarbor.DomainModel.Models
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Xunit;

    public class InsertCommandTest : IdiomaticTest<InsertCommand>
    {
        [Test]
        public void SutIsModelCommand(InsertCommand sut)
        {
            Assert.IsAssignableFrom<ModelCommand<IEnumerable<IModel>>>(sut);
        }

        [Test]
        public void ExecuteAsyncUserCorrectlyInsertsUser(
            InsertCommand sut,
            User user,
            User newUser)
        {
            sut.Repositories.Users.Of(x => x.InsertAsync(user) == Task.FromResult(newUser));

            var actual = sut.ExecuteAsync(user).Result;

            var command = Assert.IsAssignableFrom<InsertCommand>(actual);
            Assert.Equal(newUser, command.Value.Last());
        }
    }
}