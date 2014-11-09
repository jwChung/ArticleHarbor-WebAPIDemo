namespace ArticleHarbor.WebApiPresentationModel
{
    using System.Collections.Generic;
    using System.Reflection;
    using System.Web.Http.ValueProviders;
    using Xunit;

    public class PrincipleValueProviderTest : IdiomaticTest<PrincipleValueProvider>
    {
        [Test]
        public void SutIsValueProvider(PrincipleValueProvider sut)
        {
            Assert.IsAssignableFrom<IValueProvider>(sut);
        }

        [Test]
        public void ContainsPrefixReturnsFalse(PrincipleValueProvider sut)
        {
            bool actual = sut.ContainsPrefix(null);
            Assert.False(actual);
        }

        protected override IEnumerable<MemberInfo> ExceptToVerifyGuardClause()
        {
            yield return this.Methods.Select(x => x.ContainsPrefix(null));
        }
    }
}