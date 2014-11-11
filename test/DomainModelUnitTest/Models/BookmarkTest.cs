namespace ArticleHarbor.DomainModel.Models
{
    using System;
    using System.Reflection;
    using Ploeh.AutoFixture;
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