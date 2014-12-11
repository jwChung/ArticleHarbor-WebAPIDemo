namespace ArticleHarbor.WebApiPresentationModel
{
    using System;
    using System.Web.Http.Filters;

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class RollbackTransactionAttribute : ExceptionFilterAttribute
    {
    }
}