namespace WebApiPresentationModel
{
    using System;
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

            return base.OnActionExecutedAsync(actionExecutedContext, cancellationToken);
        }
    }
}