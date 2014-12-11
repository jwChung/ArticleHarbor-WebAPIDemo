namespace ArticleHarbor.DomainModel
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Threading.Tasks;
    using Models;
    using Ploeh.AutoFixture;
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
    }
}