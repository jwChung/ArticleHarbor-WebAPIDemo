namespace ArticleHarbor.WebApiPresentationModel
{
    using System;
    using System.Web.Http.ValueProviders;

    public class PrincipleValueProvider : IValueProvider
    {
        public bool ContainsPrefix(string prefix)
        {
            if (prefix == null)
                throw new ArgumentNullException("prefix");

            throw new System.NotImplementedException();
        }

        public ValueProviderResult GetValue(string key)
        {
            if (key == null)
                throw new ArgumentNullException("key");

            throw new System.NotImplementedException();
        }
    }
}