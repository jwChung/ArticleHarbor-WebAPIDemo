namespace Website
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Web.Http;
    using System.Web.Http.Controllers;
    using System.Web.Http.Dispatcher;
    using WebApiPresentationModel;

    public class CompositeRoot : IHttpControllerActivator
    {
        private readonly IDictionary<Type, Func<ApiController>> controllers;

        public CompositeRoot()
        {
            this.controllers = ControllerRegistrations.GetControllers();
        }

        public IHttpController Create(
            HttpRequestMessage request,
            HttpControllerDescriptor controllerDescriptor,
            Type controllerType)
        {
            Func<ApiController> value;
            if (!this.controllers.TryGetValue(controllerType, out value))
                return null;

            var controller = value();
            request.RegisterForDispose(controller);
            return controller;
        }

        private static class ControllerRegistrations
        {
            public static IDictionary<Type, Func<ApiController>> GetControllers()
            {
                return new Dictionary<Type, Func<ApiController>>
                {
                    {
                        typeof(ArticlesController), () => new ArticlesController()
                    }
                };
            }
        }
    }
}