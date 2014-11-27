namespace ArticleHarbor.DomainModel.Models
{
    using System;
    using System.Reflection;
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
        public void ExecuteReturnsCorrectResult(
            Bookmark sut,
            IModelCommand<object> command,
            IModelCommand<object> expected)
        {
            command.Of(x => x.Execute(sut) == expected);
            var actual = sut.Execute(command);
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