namespace ArticleHarbor.WebApiPresentationModel
{
    using System;
    using System.Web.Http.Controllers;
    using System.Web.Http.ValueProviders;

    public class PrincipleValueProviderFactory : ValueProviderFactory, IUriValueProviderFactory
    {
        public override IValueProvider GetValueProvider(HttpActionContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            return new PrincipleValueProvider(context.RequestContext.Principal);
        }
    }
}