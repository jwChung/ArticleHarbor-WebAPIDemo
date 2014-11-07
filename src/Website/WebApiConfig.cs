namespace Website
{
    using System;
    using System.Web.Hosting;
    using System.Web.Http;
    using Jwc.Funz;
    using WebApiPresentationModel;

    public static class WebApiConfig
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = "Dispose하지 않아야 DedepencyResolver역할을 할 수 있음.")]
        public static void Register(HttpConfiguration config)
        {
            if (config == null)
                throw new ArgumentNullException("config");

            var serverRoot = HostingEnvironment.MapPath("~");
            if (serverRoot != null)
                Environment.CurrentDirectory = HostingEnvironment.MapPath("~");

            // DependencyResolver
            var container = new Container();
            container.Accept(new DependencyRegistrations());
            config.DependencyResolver = new DependencyResolver(container);

            // Web API configuration and services
            config.Formatters.Remove(config.Formatters.XmlFormatter);
            config.Formatters.Insert(
                0, new JsonCustomMediaTypeFormatter(JsonConstructorDeserializer.Deserialize));

            config.Filters.Add(new SaveUnitOfWorkActionFilter());

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional });
        }
    }
}
