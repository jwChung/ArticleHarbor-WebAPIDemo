namespace ArticleHarbor.WebApiPresentationModel
{
    using System;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web.Http.Filters;
    using DomainModel.Repositories;

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public sealed class CommitTransactionAttribute : ActionFilterAttribute
    {
        public override Task OnActionExecutedAsync(
            HttpActionExecutedContext actionExecutedContext,
            CancellationToken cancellationToken)
        {
            if (actionExecutedContext == null)
                throw new ArgumentNullException("actionExecutedContext");

            return this.OnActionExecutedAsyncWith(actionExecutedContext, cancellationToken);
        }

        private async Task OnActionExecutedAsyncWith(
            HttpActionExecutedContext actionExecutedContext,
            CancellationToken cancellationToken)
        {
            var dependencyScope = actionExecutedContext.Request.GetDependencyScope();
            var lazyUnitOfWork = (Lazy<IUnitOfWork>)dependencyScope
                .GetService(typeof(Lazy<IUnitOfWork>));

            if (lazyUnitOfWork.IsValueCreated && actionExecutedContext.Exception == null)
                await lazyUnitOfWork.Value.CommitTransactionAsync();

            await base.OnActionExecutedAsync(actionExecutedContext, cancellationToken);
        }
    }
}