namespace ArticleHarbor.WebApiPresentationModel
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Web.Http;
    using System.Web.Http.Controllers;
    using Jwc.Experiment.Xunit;
    using Ploeh.AutoFixture.Xunit;
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
        public void AsIsReadWritable(ReadWritablePropertyAssertion assertion)
        {
            var asProperty = this.Properties.Select(x => x.As);
            assertion.Verify(asProperty);
        }

        [Test]
        public void GetBindingReturnsCorrectBindingWhenAsIsNull(
            [NoAutoProperties] FromDependencyResolverAttribute sut,
            HttpParameterDescriptor descriptor,
            Type parameterType)
        {
            descriptor.Of(x => x.ParameterType == parameterType);

            var actual = sut.GetBinding(descriptor);

            var binding = Assert.IsAssignableFrom<DependencyParameterBinding>(actual);
            Assert.Equal(parameterType, binding.ParameterType);
            Assert.Equal(descriptor, binding.Descriptor);
        }

        [Test]
        public void GetBindingReturnsCorrectBindingWhenAsIsNotNull(
            FromDependencyResolverAttribute sut,
            HttpParameterDescriptor descriptor,
            Type parameterType)
        {
            var type = typeof(string);
            sut.As = type;
            descriptor.Of(x => x.ParameterType == parameterType);

            var actual = sut.GetBinding(descriptor);

            var binding = Assert.IsAssignableFrom<DependencyParameterBinding>(actual);
            Assert.Equal(type, binding.ParameterType);
        }

        [Test]
        public IEnumerable<ITestCase> GetBindingWithIncorrectAsThrows()
        {
            var testData = new[]
            {
                new { As = typeof(object), ParameterType = typeof(int) },
                new { As = typeof(object), ParameterType = typeof(string) },
            };

            return TestCases.WithArgs(testData)
                .WithAuto<FromDependencyResolverAttribute, HttpParameterDescriptor>()
                .Create((data, sut, descriptor) =>
                {
                    sut.As = data.As;
                    descriptor.Of(x => x.ParameterType == data.ParameterType);
                    Assert.Throws<InvalidCastException>(() => sut.GetBinding(descriptor));
                });
        }

        protected override IEnumerable<MemberInfo> ExceptToVerifyInitialization()
        {
            yield return this.Properties.Select(x => x.As);
        }
    }
}