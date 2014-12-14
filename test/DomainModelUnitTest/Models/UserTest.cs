namespace ArticleHarbor.DomainModel.Models
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Commands;
    using Queries;
    using Xunit;

    public class UserTest : IdiomaticTest<User>
    {
        [Test]
        public void SutIsModel(User sut)
        {
            Assert.IsAssignableFrom<IModel>(sut);
        }

        [Test]
        public void ExecuteAsyncReturnsCorrectResult(
            User sut,
            IModelCommand<object> command,
            IEnumerable<object> expected)
        {
            command.Of(x => x.ExecuteAsync(sut) == Task.FromResult(expected));
            var actual = sut.ExecuteAsync(command).Result;
            Assert.Equal(expected, actual);
        }

        [Test]
        public void GetKeysReturnsCorrectKeys(User sut)
        {
            var expected = new Keys<string>(sut.Id);
            var actual = sut.GetKeys();
            Assert.Equal(expected, actual);
        }

        [Test]
        public void EqualsEqualsOtherWithDifferentValues(User sut, User other)
        {
            var actual = sut.Equals(other);
            Assert.False(actual);
        }

        [Test]
        public void EqualsEqualsOtherWithSameValues(User sut)
        {
            var other = new User(
                sut.Id,
                sut.Role,
                sut.ApiKey);
            var actual = sut.Equals(other);
            Assert.True(actual);
        }

        [Test]
        public void EqualsIgnoresCase(User sut)
        {
            var other = new User(
                sut.Id.ToUpper(),
                sut.Role,
                sut.ApiKey);
            var actual = sut.Equals(other);
            Assert.True(actual);
        }

        [Test]
        public void GetHashCodeReturnsCorrectResult(User sut)
        {
            var other = new User(
                 sut.Id.ToUpper(),
                 sut.Role,
                 sut.ApiKey);
            var actual = sut.GetHashCode();
            Assert.Equal(other.GetHashCode(), actual);
        }
    }
}