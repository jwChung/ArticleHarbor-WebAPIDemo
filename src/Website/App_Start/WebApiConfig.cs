namespace Website
{
    using System;
    using System.Web.Http;
    using System.Web.Http.Dispatcher;

    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            if (config == null)
                throw new ArgumentNullException("config");

            // Web API configuration and services
            config.Services.Replace(
                typeof(IAssembliesResolver),
                new ArticleHarborAssembliesResolver());

            config.Services.Replace(
                typeof(IHttpControllerActivator),
                new CompositeRoot());

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional });
        }
    }
}
