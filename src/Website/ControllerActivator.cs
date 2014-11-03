namespace Website
{
    using System;
    using System.Net.Http;
    using System.Web.Http.Controllers;
    using System.Web.Http.Dispatcher;
    using WebApiPresentationModel;

    public class ControllerActivator : IHttpControllerActivator
    {
        public IHttpController Create(
            HttpRequestMessage request,
            HttpControllerDescriptor controllerDescriptor,
            Type controllerType)
        {
            if (controllerType == typeof(ArticlesController))
            {
                return new ArticlesController();
            }

            return null;
        }
    }
}