namespace ArticleHarbor.DomainModel.Models
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Threading.Tasks;
    using Commands;
    using Ploeh.AutoFixture;
    using Queries;
    using Xunit;

    public class BookmarkTest : IdiomaticTest<Bookmark>
    {
        [Test]
        public void SutIsModel(Bookmark sut)
        {
            Assert.IsAssignableFrom<IModel>(sut);
        }

        [Test]
        public void InitializeWithEmptyStringValuesThrows(
            IFixture fixture)
        {
            fixture.Inject(string.Empty);
            var e = Assert.Throws<TargetInvocationException>(() => fixture.Create<Bookmark>());
            Assert.IsType<ArgumentException>(e.InnerException);
        }

        [Test]
        public void ExecuteAsyncReturnsCorrectResult(
            Bookmark sut,
            IModelCommand<object> command,
            IEnumerable<object> expected)
        {
            command.Of(x => x.ExecuteAsync(sut) == Task.FromResult(expected));
            var actual = sut.ExecuteAsync(command).Result;
            Assert.Equal(expected, actual);
        }

        [Test]
        public void GetKeysReturnsCorrectKeys(Bookmark sut)
        {
            var expected = new Keys<string, int>(sut.UserId, sut.ArticleId);
            var actual = sut.GetKeys();
            Assert.Equal(expected, actual);
        }

        [Test]
        public void EqualsEqualsOtherWithDifferentValues(Bookmark sut, Bookmark other)
        {
            var actual = sut.Equals(other);
            Assert.False(actual);
        }

        [Test]
        public void EqualsEqualsOtherWithSameValues(Bookmark sut)
        {
            var other = new Bookmark(
                sut.UserId, 
                sut.ArticleId);
            var actual = sut.Equals(other);
            Assert.True(actual);
        }

        [Test]
        public void EqualsIgnoresCase(Bookmark sut)
        {
            var other = new Bookmark(
                 sut.UserId.ToUpper(),
                 sut.ArticleId);
            var actual = sut.Equals(other);
            Assert.True(actual);
        }
        
        [Test]
        public void GetHashCodeReturnsCorrectResult(Bookmark sut)
        {
            var other = new Bookmark(
                   sut.UserId.ToUpper(),
                   sut.ArticleId);
            var actual = sut.GetHashCode();
            Assert.Equal(other.GetHashCode(), actual);
        }
    }
}