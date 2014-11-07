namespace WebApiPresentationModelUnitTest
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Reflection;
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
            LazyUnitOfWork layUnitOfWork)
        {
            var unitOfWork = layUnitOfWork.Value;
            dependencyScope.Of(x => x.GetService(typeof(LazyUnitOfWork)) == layUnitOfWork);
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
            LazyUnitOfWork layUnitOfWork)
        {
            dependencyScope.Of(x => x.GetService(typeof(LazyUnitOfWork)) == layUnitOfWork);
            actionExecutedContext.Request.Properties[HttpPropertyKeys.DependencyScope]
                = dependencyScope;

            await sut.OnActionExecutedAsync(actionExecutedContext, CancellationToken.None);

            layUnitOfWork.UnitOfWorkFactory().ToMock().Verify(x => x.SaveAsync(), Times.Never());
        }

        [Test]
        public void OnActionExecutedAsyncWithNullContextThrows(
            SaveUnitOfWorkActionFilterAttribute sut)
        {
            var e = Assert.Throws<AggregateException>(
                () => sut.OnActionExecutedAsync(null, CancellationToken.None).Wait());
            Assert.IsAssignableFrom<ArgumentNullException>(e.InnerException);
        }

        protected override IEnumerable<MemberInfo> ExceptToVerifyGuardClause()
        {
            yield return this.Methods.Select(
                x => x.OnActionExecutedAsync(null, CancellationToken.None));
        }
    }
}