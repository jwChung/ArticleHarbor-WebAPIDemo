namespace ArticleHarbor.DomainModel.Queries
{
    using System.Collections.Generic;
    using System.Reflection;
    using Xunit;

    public class NoTopTest : IdiomaticTest<NoTop>
    {
        [Test]
        public void SutIsTop(NoTop sut)
        {
            Assert.IsAssignableFrom<ITop>(sut);
        }

        protected override IEnumerable<MemberInfo> ExceptToVerifyInitialization()
        {
            yield return this.Properties.Select(x => x.Count);
        }
    }
}