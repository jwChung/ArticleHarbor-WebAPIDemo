namespace WebApiPresentationModel
{
    using System;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web.Http.Filters;

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public sealed class SaveUnitOfWorkActionFilterAttribute : ActionFilterAttribute
    {
        public override Task OnActionExecutedAsync(
            HttpActionExecutedContext actionExecutedContext,
            CancellationToken cancellationToken)
        {
            if (actionExecutedContext == null)
                throw new ArgumentNullException("actionExecutedContext");

            return this.OnActionExecutedAsyncImpl(actionExecutedContext, cancellationToken);
        }

        private async Task OnActionExecutedAsyncImpl(
            HttpActionExecutedContext actionExecutedContext, CancellationToken cancellationToken)
        {
            var dependencyScope = actionExecutedContext.Request.GetDependencyScope();
            var lazyUnitOfWork = (LazyUnitOfWork)dependencyScope
                .GetService(typeof(LazyUnitOfWork));

            if (lazyUnitOfWork.Optional != null)
                await lazyUnitOfWork.Optional.SaveAsync();

            await base.OnActionExecutedAsync(actionExecutedContext, cancellationToken);
        }
    }
}