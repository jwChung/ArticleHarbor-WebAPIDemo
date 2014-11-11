namespace ArticleHarbor.WebApiPresentationModel
{
    using System.Collections.Generic;
    using System.Globalization;
    using System.Reflection;
    using System.Web.Http.ValueProviders;
    using Jwc.Experiment.Xunit;
    using Xunit;

    public class PrincipalValueProviderTest : IdiomaticTest<PrincipalValueProvider>
    {
        [Test]
        public void SutIsValueProvider(PrincipalValueProvider sut)
        {
            Assert.IsAssignableFrom<IValueProvider>(sut);
        }

        [Test]
        public void ContainsPrefixReturnsFalse(PrincipalValueProvider sut)
        {
            bool actual = sut.ContainsPrefix(null);
            Assert.False(actual);
        }

        [Test]
        public void GetValueReturnsNullWhenKeyIsNotUserId(
            PrincipalValueProvider sut,
            string key)
        {
            var actual = sut.GetValue(key);
            Assert.Null(actual);
        }

        [Test]
        public IEnumerable<ITestCase> GetValueReturnsCorrectValueWhenKeyIsUserId()
        {
            var testData = new[]
            {
                "userid",
                "USERID",
                "UserId",
            };
            return TestCases.WithArgs(testData).WithAuto<PrincipalValueProvider, string>().Create(
                (d, sut, value) =>
                {
                    sut.Principal.Of(x => x.Identity.Name == value);

                    var actual = sut.GetValue(d);

                    Assert.Equal(value, actual.RawValue);
                    Assert.Equal(value, actual.AttemptedValue);
                    Assert.Equal(CultureInfo.CurrentCulture, actual.Culture);
                });
        }

        protected override IEnumerable<MemberInfo> ExceptToVerifyGuardClause()
        {
            yield return this.Methods.Select(x => x.ContainsPrefix(null));
        }
    }
}