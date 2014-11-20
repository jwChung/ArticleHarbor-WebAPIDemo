namespace ArticleHarbor.WebApiPresentationModel
{
    using System;
    using System.Web.Http;
    using System.Web.Http.Controllers;
    using Xunit;

    public class FromDependencyResolverAttributeTest
        : IdiomaticTest<FromDependencyResolverAttribute>
    {
        [Test]
        public void SutIsParameterBindingAttribute(FromDependencyResolverAttribute sut)
        {
            Assert.IsAssignableFrom<ParameterBindingAttribute>(sut);
        }

        [Test]
        public void GetBindingReturnsCorrectBinding(
            FromDependencyResolverAttribute sut,
            HttpParameterDescriptor descriptor,
            Type parameterType)
        {
            descriptor.Of(x => x.ParameterType == parameterType);

            var actual = sut.GetBinding(descriptor);

            var binding = Assert.IsAssignableFrom<DependencyParameterBinding>(actual);
            Assert.Equal(parameterType, binding.ParameterType);
            Assert.Equal(descriptor, binding.Descriptor);
        }
    }
}