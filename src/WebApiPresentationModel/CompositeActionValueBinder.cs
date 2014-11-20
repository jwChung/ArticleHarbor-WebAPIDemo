namespace ArticleHarbor.WebApiPresentationModel
{
    using System;
    using System.Web.Http.Controllers;

    public class CompositeActionValueBinder : IActionValueBinder
    {
        public HttpActionBinding GetBinding(HttpActionDescriptor actionDescriptor)
        {
            if (actionDescriptor == null)
                throw new ArgumentNullException("actionDescriptor");

            throw new NotImplementedException();
        }
    }
}