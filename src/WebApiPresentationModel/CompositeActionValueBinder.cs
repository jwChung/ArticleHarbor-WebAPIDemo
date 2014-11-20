namespace ArticleHarbor.WebApiPresentationModel
{
    using System;
    using System.Collections.Generic;
    using System.Web.Http.Controllers;

    public class CompositeActionValueBinder : IActionValueBinder
    {
        private readonly IActionValueBinder[] binders;

        public CompositeActionValueBinder(params IActionValueBinder[] binders)
        {
            if (binders == null)
                throw new ArgumentNullException("binders");

            this.binders = binders;
        }

        public IEnumerable<IActionValueBinder> Binders
        {
            get { return this.binders; }
        }

        public HttpActionBinding GetBinding(HttpActionDescriptor actionDescriptor)
        {
            if (actionDescriptor == null)
                throw new ArgumentNullException("actionDescriptor");

            throw new NotImplementedException();
        }
    }
}