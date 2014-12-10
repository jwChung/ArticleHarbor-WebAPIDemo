namespace ArticleHarbor.DomainModel.Models
{
    using System.Collections.Generic;
    using System.Reflection;
    using Xunit;

    public class AndPredicateTest : IdiomaticTest<AndPredicate>
    {
        [Test]
        public void SutIsPredicate(AndPredicate sut)
        {
            Assert.IsAssignableFrom<IPredicate>(sut);
        }

        protected override IEnumerable<MemberInfo> ExceptToVerifyInitialization()
        {
            yield return this.Properties.Select(x => x.SqlText);
            yield return this.Properties.Select(x => x.Parameters);
        }
    }
}