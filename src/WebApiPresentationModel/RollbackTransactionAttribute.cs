namespace ArticleHarbor.WebApiPresentationModel
{
    using System;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web.Http.Filters;
    using DomainModel.Repositories;

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public sealed class RollbackTransactionAttribute : ExceptionFilterAttribute
    {
        public override Task OnExceptionAsync(
            HttpActionExecutedContext actionExecutedContext,
            CancellationToken cancellationToken)
        {
            if (actionExecutedContext == null)
                throw new ArgumentNullException("actionExecutedContext");

            return this.OnExceptionAsyncWith(actionExecutedContext, cancellationToken);
        }

        private async Task OnExceptionAsyncWith(
            HttpActionExecutedContext actionExecutedContext,
            CancellationToken cancellationToken)
        {
            var dependencyScope = actionExecutedContext.Request.GetDependencyScope();
            var lazyUnitOfWork = (Lazy<IUnitOfWork>)dependencyScope
                .GetService(typeof(Lazy<IUnitOfWork>));

            if (lazyUnitOfWork.IsValueCreated)
                await lazyUnitOfWork.Value.RollbackTransactionAsync();

            await base.OnExceptionAsync(actionExecutedContext, cancellationToken);
        }
    }
}