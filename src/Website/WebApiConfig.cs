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
            
            // Web API configuration and services
            Environment.CurrentDirectory = HostingEnvironment.MapPath("~");

            var container = new Container();
            container.Accept(new DependencyRegistrations());
            config.DependencyResolver = new DependencyResolver(container);

            config.Formatters.Remove(config.Formatters.XmlFormatter);
            config.Formatters.Insert(
                0, new JsonCustomMediaTypeFormatter(JsonConstructorDeserializer.Deserialize));

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional });
        }
    }
}
