using Ploeh.AutoFixture;
namespace ArticleHarbor.DomainModel.Models
{
    using System;
    using System.Reflection;
    using Xunit;

    public class BookmarkTest : IdiomaticTest<Bookmark>
    {
        [Test]
        public void InitializeWithEmptyStringValuesThrows(
            IFixture fixture)
        {
            fixture.Inject(string.Empty);
            var e = Assert.Throws<TargetInvocationException>(() => fixture.Create<Bookmark>());
            Assert.IsType<ArgumentException>(e.InnerException);
        }
    }
}