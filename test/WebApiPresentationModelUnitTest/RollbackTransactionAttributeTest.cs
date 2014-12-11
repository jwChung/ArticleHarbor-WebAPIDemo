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

    public class RollbackTransactionAttributeTest : IdiomaticTest<RollbackTransactionAttribute>
    {
        [Test]
        public void SutIsExceptionFilterAttribute(RollbackTransactionAttribute sut)
        {
            Assert.IsAssignableFrom<ExceptionFilterAttribute>(sut);
        }

        [Test]
        public void OnExceptionAsyncRollbacksTransactionWhenUnitOfWorkWasConstructed(
            RollbackTransactionAttribute sut,
            HttpActionExecutedContext actionExecutedContext,
            IDependencyScope dependencyScope,
            Lazy<IUnitOfWork> lazyUnitOfWork)
        {
            var unitOfWork = lazyUnitOfWork.Value;
            dependencyScope.Of(x => x.GetService(typeof(Lazy<IUnitOfWork>)) == lazyUnitOfWork);
            actionExecutedContext.Request.Properties[HttpPropertyKeys.DependencyScope]
                = dependencyScope;

            sut.OnExceptionAsync(actionExecutedContext, CancellationToken.None).Wait();

            unitOfWork.ToMock().Verify(x => x.RollbackTransactionAsync());
        }

        [Test]
        public async Task OnExceptionAsyncDoesNotCommitTransactionWhenUnitOfWorkWasNotConstructed(
            RollbackTransactionAttribute sut,
            HttpActionExecutedContext actionExecutedContext,
            IDependencyScope dependencyScope,
            Lazy<IUnitOfWork> layUnitOfWork)
        {
            dependencyScope.Of(x => x.GetService(typeof(Lazy<IUnitOfWork>)) == layUnitOfWork);
            actionExecutedContext.Request.Properties[HttpPropertyKeys.DependencyScope]
                = dependencyScope;

            await sut.OnExceptionAsync(actionExecutedContext, CancellationToken.None);

            layUnitOfWork.Value.ToMock().Verify(x => x.RollbackTransactionAsync(), Times.Never());
        }
    }
}