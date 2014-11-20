namespace ArticleHarbor.WebApiPresentationModel
{
    using System;
    using System.Globalization;
    using System.Net.Http;
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
            
            var value = actionContext.Request.GetDependencyScope().GetService(this.parameterType);
            if (value == null)
                throw new ArgumentException(string.Format(
                    CultureInfo.CurrentCulture,
                    "The parameter type '{0}' cannot be serviced by the depedency resolver.",
                    this.parameterType));

            this.SetValue(actionContext, value);
            return Task.FromResult<object>(null);
        }
    }
}