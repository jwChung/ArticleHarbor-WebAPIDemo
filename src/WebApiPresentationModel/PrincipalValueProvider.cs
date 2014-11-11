namespace ArticleHarbor.WebApiPresentationModel
{
    using System;
    using System.Globalization;
    using System.Security.Principal;
    using System.Web.Http.ValueProviders;

    public class PrincipalValueProvider : IValueProvider
    {
        private readonly IPrincipal principal;

        public PrincipalValueProvider(IPrincipal principal)
        {
            if (principal == null)
                throw new ArgumentNullException("principal");

            this.principal = principal;
        }

        public IPrincipal Principal
        {
            get { return this.principal; }
        }

        public bool ContainsPrefix(string prefix)
        {
            return false;
        }

        public ValueProviderResult GetValue(string key)
        {
            if (key == null)
                throw new ArgumentNullException("key");

            if (!key.Equals("USERID", StringComparison.CurrentCultureIgnoreCase))
                return null;

            string value = this.principal.Identity.Name;
            return new ValueProviderResult(value, value, CultureInfo.CurrentCulture);
        }
    }
}