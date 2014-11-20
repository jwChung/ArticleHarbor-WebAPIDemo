namespace ArticleHarbor.WebApiPresentationModel
{
    using System;
    using System.Web.Http;
    using System.Web.Http.Controllers;

    [AttributeUsage(AttributeTargets.Parameter)]
    public class FromDependencyResolverAttribute : ParameterBindingAttribute
    {
        public override HttpParameterBinding GetBinding(HttpParameterDescriptor parameter)
        {
            if (parameter == null)
                throw new ArgumentNullException("parameter");

            throw new NotImplementedException();
        }
    }
}