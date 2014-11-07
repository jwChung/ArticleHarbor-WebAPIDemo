namespace WebApiPresentationModelUnitTest
{
    using System;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web.Http.Dependencies;
    using System.Web.Http.Filters;
    using System.Web.Http.Hosting;
    using Moq;
    using Ploeh.AutoFixture;
    using Ploeh.AutoFixture.Xunit;
    using WebApiPresentationModel;
    using Xunit;

    public class SaveUnitOfWorkActionFilterTest : IdiomaticTest<SaveUnitOfWorkActionFilter>
    {
        [Test]
        public void SutIsActionFilterAttribute(
            SaveUnitOfWorkActionFilter sut)
        {
            Assert.IsAssignableFrom<ActionFilterAttribute>(sut);
        }

        [Test]
        public async Task OnActionExecutedAsyncSavesUnitOfWorkWhenUnitOfWorkWasConstructedInActionMethod(
            SaveUnitOfWorkActionFilter sut,
            HttpActionExecutedContext actionExecutedContext,
            IDependencyScope depedencyScope,
            LazyUnitOfWork layUnitOfWork)
        {
            var unitOfWork = layUnitOfWork.Value;
            depedencyScope.Of(x => x.GetService(typeof(LazyUnitOfWork)) == layUnitOfWork);
            actionExecutedContext.Request.Properties[HttpPropertyKeys.DependencyScope]
                = depedencyScope;

            await sut.OnActionExecutedAsync(actionExecutedContext, CancellationToken.None);

            unitOfWork.ToMock().Verify(x => x.SaveAsync());
        }

        [Test]
        public async Task OnActionExecutedAsyncDoesNotSaveUnitOfWorkWhenUnitOfWorkWasNotConstructedInActionMethod(
            SaveUnitOfWorkActionFilter sut,
            HttpActionExecutedContext actionExecutedContext,
            IDependencyScope depedencyScope,
            LazyUnitOfWork layUnitOfWork)
        {
            depedencyScope.Of(x => x.GetService(typeof(LazyUnitOfWork)) == layUnitOfWork);
            actionExecutedContext.Request.Properties[HttpPropertyKeys.DependencyScope]
                = depedencyScope;

            await sut.OnActionExecutedAsync(actionExecutedContext, CancellationToken.None);

            layUnitOfWork.UnitOfWorkFactory().ToMock().Verify(x => x.SaveAsync(), Times.Never());
        }
    }
}