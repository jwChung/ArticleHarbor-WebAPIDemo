namespace ArticleHarbor.WebApiPresentationModel
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web.Http.Dependencies;
    using System.Web.Http.Filters;
    using System.Web.Http.Hosting;
    using DomainModel.Repositories;
    using Moq;
    using Xunit;

    public class SaveUnitOfWorkActionFilterAttributeTest
        : IdiomaticTest<SaveUnitOfWorkActionFilterAttribute>
    {
        [Test]
        public void SutIsActionFilterAttribute(
            SaveUnitOfWorkActionFilterAttribute sut)
        {
            Assert.IsAssignableFrom<ActionFilterAttribute>(sut);
        }

        [Test]
        public async Task OnActionExecutedAsyncSavesUnitOfWorkWhenUnitOfWorkWasConstructedInActionMethod(
            SaveUnitOfWorkActionFilterAttribute sut,
            HttpActionExecutedContext actionExecutedContext,
            IDependencyScope dependencyScope,
            Lazy<IUnitOfWork> lazyUnitOfWork)
        {
            var unitOfWork = lazyUnitOfWork.Value;
            dependencyScope.Of(x => x.GetService(typeof(Lazy<IUnitOfWork>)) == lazyUnitOfWork);
            actionExecutedContext.Request.Properties[HttpPropertyKeys.DependencyScope]
                = dependencyScope;

            await sut.OnActionExecutedAsync(actionExecutedContext, CancellationToken.None);

            unitOfWork.ToMock().Verify(x => x.SaveAsync());
        }

        [Test]
        public async Task OnActionExecutedAsyncDoesNotSaveUnitOfWorkWhenUnitOfWorkWasNotConstructedInActionMethod(
            SaveUnitOfWorkActionFilterAttribute sut,
            HttpActionExecutedContext actionExecutedContext,
            IDependencyScope dependencyScope,
            Lazy<IUnitOfWork> layUnitOfWork)
        {
            dependencyScope.Of(x => x.GetService(typeof(Lazy<IUnitOfWork>)) == layUnitOfWork);
            actionExecutedContext.Request.Properties[HttpPropertyKeys.DependencyScope]
                = dependencyScope;

            await sut.OnActionExecutedAsync(actionExecutedContext, CancellationToken.None);

            layUnitOfWork.Value.ToMock().Verify(x => x.SaveAsync(), Times.Never());
        }
    }
}