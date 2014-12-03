namespace ArticleHarbor.WebApiPresentationModel
{
    using System;
    using System.Globalization;
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

            this.EnsureAsIsValidType(parameter);
            return new DependencyParameterBinding(this.@as ?? parameter.ParameterType, parameter);
        }

        private void EnsureAsIsValidType(HttpParameterDescriptor parameter)
        {
            if (this.@as != null && !parameter.ParameterType.IsAssignableFrom(this.@as))
                throw new InvalidCastException(string.Format(
                    CultureInfo.CurrentCulture,
                    "The as type '{0}' cannot be assignable to the parameter type '{1}'.",
                    this.@as,
                    parameter.ParameterType));
        }
    }
}