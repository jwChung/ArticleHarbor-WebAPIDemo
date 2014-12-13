namespace ArticleHarbor.DomainModel.Queries
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using Ploeh.AutoFixture;
    using Xunit;

    public class OperablePredicateTest : IdiomaticTest<OperablePredicate>
    {
        [Test]
        public void SutIsPredicate(OperablePredicate sut)
        {
            Assert.IsAssignableFrom<IPredicate>(sut);
        }

        [Test]
        public void InitializeWithEmptyColumnNameThrows(string operatorName, object value)
        {
            Assert.Throws<ArgumentException>(() => new OperablePredicate(string.Empty, operatorName, value));
        }
        
        [Test]
        public void InitializeWithEmptyOperatorNameThrows(string columnName, object value)
        {
            Assert.Throws<ArgumentException>(() => new OperablePredicate(columnName, string.Empty, value));
        }

        protected override IEnumerable<MemberInfo> ExceptToVerifyInitialization()
        {
            yield return this.Properties.Select(x => x.SqlText);
            yield return this.Properties.Select(x => x.Parameters);
        }
    }
}