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
            foreach (var binder in this.Binders)
            {
                var binding = binder.GetBinding(actionDescriptor);
                if (binding != null)
                    return binding;
            }

            throw new InvalidOperationException("Cannot find action binding...");
        }
    }
}