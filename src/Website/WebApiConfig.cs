namespace Website
{
    using System;
    using System.Web.Http;
    using System.Web.Http.Dispatcher;
    using Jwc.Funz;

    public static class WebApiConfig
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = "Dispose하지 않아야 DedepencyResolver역할을 할 수 있음.")]
        public static void Register(HttpConfiguration config)
        {
            if (config == null)
                throw new ArgumentNullException("config");

            // Web API configuration and services
            var container = new Container();
            container.Accept(new DependencyRegistrations());
            config.DependencyResolver = new DependencyResolver(container);

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional });
        }
    }
}
