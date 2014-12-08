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
        public void ConditionIsCorrect(object value)
        {
            var sut = new EqualPredicate("@foo", value);
            var expected = "foo = @foo";
            var actual = sut.SqlText;
            Assert.Equal(expected, actual);
        }

        [Test]
        public void ParametersIsCorrect(object value)
        {
            var sut = new EqualPredicate("@foo", value);
            var actual = sut.Parameters.Single();
            Assert.Equal(new Parameter(sut.Name, sut.Value), actual);
        }

        protected override IEnumerable<MemberInfo> ExceptToVerifyInitialization()
        {
            yield return this.Properties.Select(x => x.SqlText);
            yield return this.Properties.Select(x => x.Parameters);
        }
    }
}