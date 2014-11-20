namespace ArticleHarbor.WebApiPresentationModel
{
    using System;
    using System.Web.Http;
    using Xunit;

    public class FromDependencyResolverAttributeTest
        : IdiomaticTest<FromDependencyResolverAttribute>
    {
        [Test]
        public void SutIsParameterBindingAttribute(FromDependencyResolverAttribute sut)
        {
            Assert.IsAssignableFrom<ParameterBindingAttribute>(sut);
        }
    }
}