namespace Website
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Web.Http.Controllers;
    using System.Web.Http.Dispatcher;
    using WebApiPresentationModel;

    public class CompositeRoot : IHttpControllerActivator
    {
        private readonly IDictionary<Type, Func<object>> container;

        public CompositeRoot()
        {
            this.container = new Dictionary<Type, Func<object>>();
            new ServiceRegistrations().Register(this.container);
        }

        public IHttpController Create(
            HttpRequestMessage request,
            HttpControllerDescriptor controllerDescriptor,
            Type controllerType)
        {
            Func<object> value;
            if (this.container.TryGetValue(controllerType, out value))
                return (IHttpController)value();

            return null;
        }

        private class ServiceRegistrations
        {
            public void Register(IDictionary<Type, Func<object>> container)
            {
                container[typeof(ArticlesController)] = () => new ArticlesController();
            }
        }
    }
}