namespace ArticleHarbor.WebApiPresentationModel
{
    using System;
    using System.Net.Http;
    using System.Threading;
    using System.Web.Http.Controllers;
    using System.Web.Http.Dependencies;
    using System.Web.Http.Hosting;
    using System.Web.Http.Metadata;
    using Xunit;

    public class DependencyParameterBindingTest : IdiomaticTest<DependencyParameterBinding>
    {
        [Test]
        public void SutIsHttpParameterBinding(DependencyParameterBinding sut)
        {
            Assert.IsAssignableFrom<HttpParameterBinding>(sut);
        }

        [Test]
        public void ExecuteBindingAsyncCorrectlyBinds(
            DependencyParameterBinding sut,
            ModelMetadataProvider metadataProvider,
            HttpActionContext actionContext,
            IDependencyScope dependencyScope,
            string parameterName,
            object expected)
        {
            sut.Descriptor.Of(x => x.ParameterName == parameterName);
            dependencyScope.Of(x => x.GetService(sut.ParameterType) == expected);
            actionContext.Request.Properties[HttpPropertyKeys.DependencyScope] = dependencyScope;

            sut.ExecuteBindingAsync(metadataProvider, actionContext, CancellationToken.None).Wait();

            var actual = actionContext.ActionArguments[parameterName];
            Assert.Equal(expected, actual);
        }

        [Test]
        public void ExecuteBindingAsyncThrowsWhenDependencyResolverCannotResolveParameterType(
            DependencyParameterBinding sut,
            ModelMetadataProvider metadataProvider,
            HttpActionContext actionContext,
            IDependencyScope dependencyScope)
        {
            dependencyScope.ToMock().Setup(x => x.GetService(sut.ParameterType)).Returns(() => null);
            actionContext.Request.Properties[HttpPropertyKeys.DependencyScope] = dependencyScope;

            Assert.Throws<ArgumentException>(() => sut.ExecuteBindingAsync(
                metadataProvider, actionContext, CancellationToken.None).Wait());
        }
    }
}