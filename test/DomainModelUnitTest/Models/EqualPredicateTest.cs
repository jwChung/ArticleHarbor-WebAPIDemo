namespace ArticleHarbor.DomainModel.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Ploeh.AutoFixture;
    using Xunit;

    public class EqualPredicateTest : IdiomaticTest<EqualPredicate>
    {
        [Test]
        public void SutIsPredicate(EqualPredicate sut)
        {
            Assert.IsAssignableFrom<IPredicate>(sut);
        }

        [Test]
        public void InitializeWithEmptyNameThrows(IFixture fixture, object value)
        {
            Assert.Throws<ArgumentException>(() => new EqualPredicate(string.Empty, value));
        }

        [Test]
        public void ConditionIsCorrect(EqualPredicate sut)
        {
            var expected = sut.Name + " = @" + sut.Name;
            var actual = sut.Condition;
            Assert.Equal(expected, actual);
        }

        [Test]
        public void ParametersIsCorrect(EqualPredicate sut)
        {
            var actual = sut.Parameters.Single();
            Assert.Equal(new Parameter(sut.Name, sut.Value), actual);
        }

        [Test]
        public void ParametersAlwaysReturnsSameInstance(EqualPredicate sut)
        {
            var actual = sut.Parameters;
            Assert.Same(sut.Parameters, actual);
        }

        protected override IEnumerable<MemberInfo> ExceptToVerifyInitialization()
        {
            yield return this.Properties.Select(x => x.Condition);
            yield return this.Properties.Select(x => x.Parameters);
        }
    }
}