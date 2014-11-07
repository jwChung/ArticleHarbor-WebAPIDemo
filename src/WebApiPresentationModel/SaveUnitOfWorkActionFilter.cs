namespace WebApiPresentationModel
{
    using System;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web.Http.Filters;

    public class SaveUnitOfWorkActionFilter : ActionFilterAttribute
    {
        public override Task OnActionExecutedAsync(
            HttpActionExecutedContext actionExecutedContext,
            CancellationToken cancellationToken)
        {
            if (actionExecutedContext == null)
                throw new ArgumentNullException("actionExecutedContext");

            var dependencyScope = actionExecutedContext.Request.GetDependencyScope();
            var lazyUnitOfWork = (LazyUnitOfWork)dependencyScope
                .GetService(typeof(LazyUnitOfWork));

            if (lazyUnitOfWork.Optional != null)
                lazyUnitOfWork.Optional.SaveAsync();

            return base.OnActionExecutedAsync(actionExecutedContext, cancellationToken);
        }
    }
}