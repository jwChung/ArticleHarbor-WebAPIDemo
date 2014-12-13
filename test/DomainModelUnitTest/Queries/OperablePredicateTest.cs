namespace ArticleHarbor.DomainModel.Queries
{
    using System.Collections.Generic;
    using System.Reflection;
    using Xunit;

    public class OperablePredicateTest : IdiomaticTest<OperablePredicate>
    {
        [Test]
        public void SutIsPredicate(OperablePredicate sut)
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