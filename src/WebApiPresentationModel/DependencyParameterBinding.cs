namespace ArticleHarbor.WebApiPresentationModel
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web.Http.Controllers;
    using System.Web.Http.Metadata;

    public class DependencyParameterBinding : HttpParameterBinding
    {
        private readonly Type parameterType;

        public DependencyParameterBinding(Type parameterType, HttpParameterDescriptor descriptor) : base(descriptor)
        {
            if (parameterType == null)
                throw new ArgumentNullException("parameterType");

            this.parameterType = parameterType;
        }

        public Type ParameterType
        {
            get { return this.parameterType; }
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