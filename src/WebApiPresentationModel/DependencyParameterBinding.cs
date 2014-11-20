namespace ArticleHarbor.WebApiPresentationModel
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web.Http.Controllers;
    using System.Web.Http.Metadata;

    public class DependencyParameterBinding : HttpParameterBinding
    {
        public DependencyParameterBinding(HttpParameterDescriptor descriptor) : base(descriptor)
        {
        }

        public override Task ExecuteBindingAsync(
            ModelMetadataProvider metadataProvider,
            HttpActionContext actionContext,
            CancellationToken cancellationToken)
        {
            if (metadataProvider == null)
                throw new ArgumentNullException("metadataProvider");

            if (actionContext == null)
                throw new ArgumentNullException("actionContext");

            throw new NotImplementedException();
        }
    }
}