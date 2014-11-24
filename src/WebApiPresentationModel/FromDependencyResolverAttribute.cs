namespace ArticleHarbor.WebApiPresentationModel
{
    using System;
    using System.Web.Http;
    using System.Web.Http.Controllers;

    [AttributeUsage(AttributeTargets.Parameter)]
    public sealed class FromDependencyResolverAttribute : ParameterBindingAttribute
    {
        private Type @as;

        public Type As
        {
            get
            {
                return this.@as;
            }

            set
            {
                if (value == null)
                    throw new ArgumentNullException("value");

                this.@as = value;
            }
        }

        public override HttpParameterBinding GetBinding(HttpParameterDescriptor parameter)
        {
            if (parameter == null)
                throw new ArgumentNullException("parameter");

            return new DependencyParameterBinding(parameter.ParameterType, parameter);
        }
    }
}